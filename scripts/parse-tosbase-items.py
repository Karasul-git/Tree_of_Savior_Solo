#!/usr/bin/env python3
"""Lê as capturas de itens do ToSBase de 2016 e grava os dados estruturados.

Uso: python3 scripts/parse-tosbase-items.py DIRETORIO_DO_BRANCH_DE_DADOS
Grava data/research/tosbase-2016-items.json (sem o HTML bruto).
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


def num(value):
    m = re.search(r'-?\d[\d,]*(?:\.\d+)?', value or '')
    return float(m.group(0).replace(',', '')) if m else None


def section(body, title):
    m = re.search(r'<h2[^>]*>' + re.escape(title) + r'</h2>(.*?)(?=<h2[^>]*>|<div id="comments")', body, re.S)
    return m.group(1) if m else None


def parse(raw):
    body = re.sub(r'<script.*?</script>|<style.*?</style>', '', raw, flags=re.S)
    if 'Item not found' in body[:6000]:
        return None
    m = re.search(r'<h1>Item Database</h1></div>\s*<div><h2>(.*?)</h2></div>', body, re.S) or re.search(r'<h1>(?!Item Database)(.*?)</h1>', body, re.S)
    if not m:
        return None
    out = {'name': text(m.group(1))}
    icon = re.search(r'content/img/icons/items/([\w.-]+)', body[body.find('items/bg_top'):] if 'items/bg_top' in body else body)
    out['icon'] = icon.group(1) if icon else None
    star = re.search(r'items/star(\d)\.png', body)
    out['grade'] = int(star.group(1)) if star else None
    out['no_trade'] = 'no_trade.png' in body
    # tooltip: do nome até o fim do bloco
    t0 = body.find('items/bg_top')
    tip = body[t0:] if t0 >= 0 else ''
    cut = re.search(r'<h2[^>]*>(?:Dropped By|Crafted By|Required For|Part Of|Quest Reward|Sets|Collection|Obtained|Comments|Write Comment|Improved Skill)', tip)
    tip = tip[:cut.start()] if cut else tip
    # a tabela de aprimoramento e a Common Info ficam antes ou ao lado do tooltip; fora do bloco
    lines = [l.strip() for l in text(re.sub(r'<(?:td|th)[^>]*>', '\n', tip)).split('\n') if l.strip()]
    lines = [l for l in lines if not l.startswith('items/') and not l.startswith('bg_')]
    if not lines:
        # Outro leiaute (consumíveis, materiais, gemas): cartão simples depois do caminho de navegação.
        crumb = re.search(r'Item Database</a> &bull;[^<]*</div>', body)
        rest = body[crumb.end():] if crumb else body
        rest = re.sub(r'<h2[^>]*>(?:Common Info|Enhancement \(Anvil\))</h2>\s*<table.*?</table>', '', rest, flags=re.S)
        stop = re.search(r'<h2[^>]*>', rest)
        card = rest[:stop.start()] if stop else rest
        lines = [l.strip() for l in text(re.sub(r'</?div[^>]*>', '\n', card)).split('\n') if l.strip()]
    if lines and lines[0] == out['name']:
        lines = lines[1:]
    out['tooltip_lines'] = lines
    out['tradeable'] = False if 'Not Tradeable' in lines else (True if 'Tradeable' in lines else None)
    for l in lines:
        if re.match(r'^Weight:', l):
            out['weight'] = num(l)
        elif l.startswith('Required Level:'):
            out['required_level'] = int(num(l) or 0)
        elif l.startswith('Required Class:'):
            out['required_classes'] = [c.strip() for c in l.split(':', 1)[1].split(',') if c.strip()]
    for i, l in enumerate(lines):
        if l == 'Potential' and i + 1 < len(lines):
            out['potential'] = lines[i + 1]
        if l == 'Durability' and i + 1 < len(lines):
            out['durability'] = lines[i + 1]
    out['type_line'] = lines[0] if lines and not lines[0].startswith(('Weight', 'Required')) else None
    # pares rótulo/valor logo depois do peso
    stats = {}
    for i, l in enumerate(lines[:-1]):
        nxt = lines[i + 1]
        if re.fullmatch(r'(?:Physical|Magic|Magical)\s+\w+|Defense|Physical Defense|Magic Defense|[A-Z][A-Za-z. ]{2,30}', l) and re.fullmatch(r'[+-]?\d[\d,]*(?:\s*~\s*\d[\d,]*)?%?', nxt):
            stats[l] = nxt
    out['stats'] = stats
    # aprimoramento
    enh = re.search(r'<h2[^>]*>Enhancement \(Anvil\)</h2>\s*<table class="db_table">(.*?)</table>', body, re.S)
    out['enhancement'] = []
    if enh:
        rows = [re.findall(r'<t[dh][^>]*>(.*?)</t[dh]>', r, re.S) for r in re.findall(r'<tr>(.*?)</tr>', enh.group(1), re.S)]
        stat_label = text(rows[0][1]) if rows and len(rows[0]) > 1 else None
        out['enhancement_stat'] = stat_label
        for r in rows[1:]:
            if len(r) >= 4:
                out['enhancement'].append({'level': int(num(text(r[0])) or 0), 'bonus': num(text(r[1])), 'success_pct': num(text(r[2])), 'price': num(text(r[3]))})
    common = re.search(r'<h2[^>]*>Common Info</h2>\s*<table class="db_table2">(.*?)</table>', body, re.S)
    out['common'] = {}
    if common:
        for r in re.findall(r'<tr>(.*?)</tr>', common.group(1), re.S):
            c = re.findall(r'<td[^>]*>(.*?)</td>', r, re.S)
            if len(c) >= 2:
                out['common'][text(c[0])] = text(c[1])
    s = section(body, 'Dropped By')
    out['dropped_by'] = []
    if s:
        for mid, name, chance in re.findall(r'href="database/monsters/(\d+)/">(.*?)</a></span></div><div>Drop Chance: ([^<]*)</div>', s, re.S):
            ch = None if '?' in chance else num(chance)
            out['dropped_by'].append({'monster_id': int(mid), 'name': text(name), 'chance_pct': ch})
    s = section(body, 'Crafted By')
    out['crafted_by'] = [{'recipe_id': int(i), 'name': text(n)} for i, n in re.findall(r'href="database/items/recipes/(\d+)/"[^>]*>([^<]+)</a>', s or '') if n.strip()]
    s = section(body, 'Required For')
    out['required_for'] = []
    if s:
        seen = set()
        for kind, i, n in re.findall(r'href="database/items/(recipes/)?(\d+)/"[^>]*style="font-weight: bold;"[^>]*>([^<]+)</a>', s):
            if (kind, i) not in seen:
                seen.add((kind, i))
                out['required_for'].append({'type': 'recipe' if kind else 'item', 'id': int(i), 'name': text(n)})
    s = section(body, 'Part Of')
    out['part_of'] = [{'collection_id': int(i), 'name': text(n)} for i, n in re.findall(r'href="database/collections/(\d+)/"[^>]*>([^<]+)</a>', s or '')]
    s = section(body, 'Quest Reward')
    out['quest_reward'] = [{'quest_id': int(i), 'name': text(n), 'level': int(l)} for i, n, l in re.findall(r'href="database/quests/(\d+)/"[^>]*>(.*?)</a>\s*\(Level (\d+)\)', s or '', re.S)]
    s = section(body, 'Obtained (Treasure Chest)')
    out['treasure_chest'] = text(s) if s else None
    s = section(body, 'Sets')
    out['sets'] = [{'set_id': int(i), 'name': text(n)} for i, n in re.findall(r'href="database/items/sets/(\d+)/"[^>]*>([^<]+)</a>', s or '')]
    s = section(body, 'Improved Skill')
    out['improved_skills'] = [text(n) for n in re.findall(r'<a [^>]*href="database/skills/\d+/"[^>]*>([^<]+)</a>', s or '')]
    return out


items, problems, notfound = {}, [], []
for mid, cap in sorted(manifest.items()):
    if not mid.startswith('items/') or mid.endswith('_lista') or cap.get('status') != 'ok':
        continue
    raw = gzip.open(src / Path(cap['file']).relative_to('data/tosbase-2016')).read().decode('utf-8', 'replace')
    data = parse(raw)
    iid = mid.split('/')[1]
    if data is None:
        (notfound if 'Item not found' in raw else problems).append(iid)
        continue
    data.update({'tosbase_id': iid, 'captured_at': cap['timestamp'], 'archive_url': cap['archive_url'], 'html_sha256': cap['sha256']})
    items[iid] = data

out = {'source': 'ToSBase, capturas do Internet Archive (março a junho de 2016)', 'item_count': len(items),
       'note': 'Valores exibidos pelo banco naquela janela. Gerado por scripts/parse-tosbase-items.py.', 'items': items}
(ROOT / 'data/research/tosbase-2016-items.json').write_text(json.dumps(out, ensure_ascii=False, separators=(',', ':')) + '\n', encoding='utf-8')
print(f'{len(items)} itens lidos; {len(notfound)} "não encontrado"; {len(problems)} sem leitura: {problems[:10]}')
