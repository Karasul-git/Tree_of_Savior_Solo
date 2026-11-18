#!/usr/bin/env python3
"""Lê as capturas de monstros do ToSBase de 2016 e grava os dados estruturados.

Uso: python3 scripts/parse-tosbase-monsters.py DIRETORIO_DO_BRANCH_DE_DADOS
Grava data/research/tosbase-2016-monsters.json (sem o HTML bruto).
"""
import gzip, html, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
src = Path(sys.argv[1])
manifest = json.loads((src / 'manifesto.json').read_text())['captures']


def text(fragment):
    fragment = re.sub(r'<br\s*/?>', '\n', fragment)
    return html.unescape(re.sub(r'<[^>]+>', '', fragment)).replace('\xa0', ' ').strip()


def num(value):
    value = (value or '').replace(',', '')
    m = re.search(r'-?\d+(?:\.\d+)?', value)
    return (int(m.group(0)) if re.fullmatch(r'-?\d+', m.group(0)) else float(m.group(0))) if m else None


def rng(value):
    parts = re.findall(r'-?\d[\d,]*(?:\.\d+)?', value or '')
    nums = [num(p) for p in parts]
    return nums if nums else None


def alt(cell):
    m = re.search(r'alt="([^"]*)"', cell)
    return m.group(1) if m else text(cell)


def parse(raw):
    body = re.sub(r'<script.*?</script>|<style.*?</style>', '', raw, flags=re.S)
    head = re.search(r'<table class="db_table3">.*?</table>', body, re.S)
    if not head:
        return None
    h = head.group(0)
    rows = [re.findall(r'<t[dh][^>]*>(.*?)</t[dh]>', r, re.S) for r in re.findall(r'<tr>(.*?)</tr>', h, re.S)]
    out = {}
    icon = re.search(r'content/img/icons/monsters/([\w.-]+)', h)
    out['icon'] = icon.group(1) if icon else None
    if len(rows) > 1 and len(rows[1]) >= 6:
        c = rows[1]  # nome, nível, tipo, raça, elemento, armadura
        out['name'] = text(c[0]); out['level'] = num(text(c[1])); out['type'] = alt(c[2]); out['race'] = alt(c[3])
        out['element'] = alt(c[4]); out['armor'] = alt(c[5])
    elif len(rows) > 1:
        return None
    if len(rows) > 3 and len(rows[3]) >= 6:
        c = rows[3]  # movimento, velocidade, HP, exp base, exp de classe, tamanho
        out['move'] = alt(c[0]); out['speed'] = rng(text(c[1])); out['hp'] = num(text(c[2]))
        out['base_exp'] = num(text(c[3])); out['class_exp'] = num(text(c[4])); out['size'] = text(c[5])
    desc = re.search(r'<th colspan="9">Description</th></tr><tr><td colspan="9">(.*?)</td></tr>', h, re.S)
    out['description'] = text(desc.group(1)) if desc else ''
    drops = re.search(r'<th colspan="9">Drops.*?</th></tr><tr><td colspan="9">(.*?)</td></tr>', h, re.S)
    out['drops'] = []
    if drops:
        for item_id, name, chance in re.findall(r'<a href="database/items/(\d+)/"[^>]*>(.*?)</a>\s*\(([^)]*)\)', drops.group(1), re.S):
            pct = None if '?' in chance else num(chance)
            out['drops'].append({'item_id': int(item_id), 'name': text(name), 'chance_pct': pct})
    loc = re.search(r'<th colspan="9">Locations</th></tr><tr><td colspan="9">(.*?)</td></tr>', h, re.S)
    out['locations'] = []
    if loc:
        for map_id, name, extra in re.findall(r'<a href="game/world-map/(\d+)/">(.*?)</a>\s*(?:\(([^)]*)\))?', loc.group(1), re.S):
            spawn = re.findall(r'\d+', extra or '')
            out['locations'].append({'map_id': int(map_id), 'map': text(name), 'spawn_count': int(spawn[0]) if spawn else None,
                                     'respawn_seconds': int(spawn[1]) if len(spawn) > 1 else None})
    stats = re.search(r'<h2>Stats</h2>(.*)', body, re.S)
    if stats:
        tables = re.findall(r'<table.*?</table>', stats.group(1), re.S)
        def grid(t):
            r = [re.findall(r'<t[dh][^>]*>(.*?)</t[dh]>', x, re.S) for x in re.findall(r'<tr>(.*?)</tr>', t, re.S)]
            pairs = {}
            for i in range(0, len(r) - 1, 2):
                for k, v in zip(r[i], r[i + 1]):
                    pairs[text(k)] = text(v)
            return pairs
        if tables:
            s = grid(tables[0]); out['attributes'] = {k: num(v) for k, v in s.items()}
        if len(tables) > 1:
            s = grid(tables[1])
            out['combat'] = {k: (rng(v) if '~' in v else num(v)) for k, v in s.items()}
    return out


monsters, problems = {}, []
for mid, cap in sorted(manifest.items()):
    if not mid.startswith('monsters/') or mid.endswith('_lista') or cap.get('status') != 'ok':
        continue
    raw = gzip.open(src / Path(cap['file']).relative_to('data/tosbase-2016')).read().decode('utf-8', 'replace')
    data = parse(raw)
    if not data or not data.get('name') or data.get('level') is None:
        problems.append(mid)
        continue
    data.update({'tosbase_id': mid.split('/')[1], 'captured_at': cap['timestamp'], 'archive_url': cap['archive_url'], 'html_sha256': cap['sha256']})
    monsters[mid.split('/')[1]] = data

out = {'source': 'ToSBase, capturas do Internet Archive (março a junho de 2016)', 'monster_count': len(monsters),
       'note': 'Valores exibidos pelo banco naquela janela; chances de drop "?" ficam sem valor. Gerado por scripts/parse-tosbase-monsters.py.',
       'monsters': monsters}
(ROOT / 'data/research/tosbase-2016-monsters.json').write_text(json.dumps(out, ensure_ascii=False, indent=0) + '\n', encoding='utf-8')
print(f'{len(monsters)} monstros lidos; {len(problems)} sem leitura: {problems[:10]}')
