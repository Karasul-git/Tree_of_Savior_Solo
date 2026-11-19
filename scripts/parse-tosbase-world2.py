#!/usr/bin/env python3
"""Lê mapas, NPCs, atributos, coleções e efeitos de estado do ToSBase de 2016.

Uso: python3 scripts/parse-tosbase-world2.py DIRETORIO_DO_BRANCH_DE_DADOS
Grava data/research/tosbase-2016-{maps,npcs,abilities,collections,status-effects}.json.
"""
import gzip, html, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
src = Path(sys.argv[1])
manifest = json.loads((src / 'manifesto.json').read_text())['captures']


def text(fragment):
    fragment = re.sub(r'<br\s*/?>|</div>|</tr>|</p>', '\n', fragment)
    t = html.unescape(re.sub(r'<[^>]+>', '', fragment)).replace('\xa0', ' ')
    return re.sub(r'\n\s*\n+', '\n', re.sub(r'[ \t]+', ' ', t)).strip()


def num(v):
    m = re.search(r'-?\d[\d,]*(?:\.\d+)?', v or '')
    return int(float(m.group(0).replace(',', ''))) if m else None


def alt(cell):
    m = re.search(r'alt="([^"]*)"', cell)
    return m.group(1) if m else text(cell)


def load(cap):
    raw = gzip.open(src / Path(cap['file']).relative_to('data/tosbase-2016')).read().decode('utf-8', 'replace')
    body = re.sub(r'<script.*?</script>|<style.*?</style>', '', raw, flags=re.S)
    body = re.sub(r' style="[^"]*"', '', body)
    return re.sub(r'<div id="(?:item|set)_[a-z_]*tooltip_\d+".*?(?=<div id="(?:item|set)_|<h2|<div id="comments"|<div id="footer")', '', body, flags=re.S)


def title(body):
    m = re.search(r'<h1>(.*?)</h1>', body, re.S)
    return text(m.group(1)) if m else None


def table_rows(fragment):
    return [re.findall(r'<t[dh][^>]*>(.*?)</t[dh]>', r, re.S) for r in re.findall(r'<tr>(.*?)</tr>', fragment, re.S)]


def parse_map(body):
    name = title(body)
    if not name:
        return None
    info = re.search(r'<h2>Map Info</h2><div>(.*?)</div></div>', body, re.S)
    meta = {}
    if info:
        for k, v in re.findall(r'<b>([^<:]+):</b>\s*([^<]*)', info.group(1)):
            meta[k.strip()] = v.strip()
    conn = re.search(r'<h2>Connected Maps</h2><div>(.*?)</div></div>', body, re.S)
    connected = []
    if conn:
        for mid, nm, extra in re.findall(r'href="game/world-map/(\d+)/">(.*?)</a>\s*\(([^)]*)\)', conn.group(1), re.S):
            connected.append({'map_id': int(mid), 'name': text(nm), 'level': num(extra), 'type': extra.split(',')[-1].strip() if ',' in extra else None})
    mons = []
    ms = re.search(r'<h2>Monsters</h2>(.*?)(?=<h2>Normal NPCs|<h2>Other NPCs|<div id="footer"|$)', body, re.S)
    if ms:
        for mid, nm, pop, resp in re.findall(r'href="database/monsters/(\d+)/">(.*?)</a>.*?Population: (\d+).*?Respawn: (\d+)s', ms.group(1), re.S):
            mons.append({'monster_id': int(mid), 'name': text(nm), 'population': int(pop), 'respawn_seconds': int(resp)})
    npcs = []
    for section in ('Normal NPCs', 'Other NPCs'):
        s = re.search(r'<h2>%s</h2>(.*?)(?=<h2>|<div id="footer"|$)' % section, body, re.S)
        if s:
            for nid, nm, lv in re.findall(r'href="database/npcs/(\d+)/">(.*?)</a></div><div>Level: (\d+)', s.group(1), re.S):
                npcs.append({'npc_id': int(nid), 'name': text(nm), 'level': int(lv), 'group': section})
    return {'name': name, 'area': meta.get('Area'), 'type': meta.get('Type'), 'level': num(meta.get('Level')),
            'revive_orb': meta.get('Revive Orb Useable'), 'connected': connected, 'monsters': mons, 'npcs': npcs}


def parse_npc(body):
    name = title(body)
    if not name:
        return None
    rows = table_rows(body.split('<h2>Locations</h2>')[0])
    out = {'name': name}
    if len(rows) > 1 and len(rows[1]) >= 5:
        c = rows[1]
        out.update({'level': num(text(c[0])), 'faction': text(c[1]), 'race': alt(c[2]), 'size': text(c[3]), 'armor': alt(c[4])})
    loc = re.search(r'<h2>Locations</h2>(.*?)(?=<div id="comments"|<div id="footer"|$)', body, re.S)
    out['locations'] = []
    if loc:
        for r in table_rows(loc.group(1))[1:]:
            if len(r) >= 4:
                mm = re.search(r'world-map/(\d+)/">(.*?)</a>', r[0], re.S)
                quests = [{'quest_id': int(i), 'name': text(n)} for i, n in re.findall(r'href="database/quests/(\d+)/">(.*?)</a>', r[3], re.S)]
                out['locations'].append({'map_id': int(mm.group(1)) if mm else None, 'map': text(mm.group(2)) if mm else text(r[0]), 'name': text(r[1]), 'level': num(text(r[2])), 'quests': quests})
    return out


def parse_ability(body):
    m = re.search(r'&bull; ([^<]+)</div>', body)
    name = text(m.group(1)) if m else None
    if not name:
        return None
    t = re.search(r'<table class="db_table3".*?</table>', body, re.S)
    if not t:
        return None
    rows = table_rows(t.group(0))
    circle = re.search(r'Circle (\d+)', t.group(0))
    out = {'name': name, 'circle': int(circle.group(1)) if circle else None}
    if len(rows) > 1 and len(rows[1]) >= 5:
        c = rows[1]
        sk = re.search(r'database/skills/(\d+)/">(.*?)</a>', c[2], re.S)
        out.update({'class': text(c[0]), 'max_level': num(text(c[1])), 'skill_id': int(sk.group(1)) if sk else None,
                    'skill': text(sk.group(2)) if sk else text(c[2]), 'required': text(c[3]), 'type': text(c[4])})
    if len(rows) > 3 and rows[3]:
        out['description'] = text(rows[3][0])
        out['effect'] = text(rows[3][1]) if len(rows[3]) > 1 else None
    return out


def parse_collection(body):
    name = title(body)
    if not name:
        return None
    rows = table_rows(re.search(r'<table class="db_table">.*?</table>', body, re.S).group(0)) if re.search(r'<table class="db_table">', body) else []
    out = {'name': name, 'items': [], 'bonus': None}
    if len(rows) > 1 and len(rows[1]) >= 3:
        out['items'] = [int(i) for i in re.findall(r'href="database/items/(\d+)/"', rows[1][1])]
        out['bonus'] = text(rows[1][2])
    return out


def parse_status(body):
    name = title(body)
    if not name:
        return None
    t = re.search(r'<table class="db_table">.*?</table>', body, re.S)
    out = {'name': name, 'entries': []}
    if t:
        for r in table_rows(t.group(0))[1:]:
            if len(r) >= 5:
                out['entries'].append({'name': text(r[1]), 'level': num(text(r[2])), 'type': ' / '.join(text(r[3]).split('\n')), 'description': text(r[4])})
    return out


JOBS = [('worldmap', 'maps', parse_map), ('npcs', 'npcs', parse_npc), ('attributes', 'abilities', parse_ability),
        ('collections', 'collections', parse_collection), ('statuseffects', 'status-effects', parse_status)]
for kind, outname, fn in JOBS:
    data, problems = {}, []
    for mid, cap in sorted(manifest.items()):
        if not mid.startswith(kind + '/') or mid.endswith('_lista') or cap.get('status') != 'ok':
            continue
        r = fn(load(cap))
        if not r:
            problems.append(mid)
            continue
        r.update({'tosbase_id': mid.split('/')[1], 'captured_at': cap['timestamp'], 'archive_url': cap['archive_url'], 'html_sha256': cap['sha256']})
        data[mid.split('/')[1]] = r
    out = {'source': 'ToSBase, capturas do Internet Archive (março a junho de 2016)', 'count': len(data),
           'note': 'Valores exibidos pelo banco naquela janela. Gerado por scripts/parse-tosbase-world2.py.', 'entries': data}
    (ROOT / f'data/research/tosbase-2016-{outname}.json').write_text(json.dumps(out, ensure_ascii=False, separators=(',', ':')) + '\n', encoding='utf-8')
    print(f'{outname}: {len(data)} lidos; {len(problems)} sem leitura {problems[:6]}')
