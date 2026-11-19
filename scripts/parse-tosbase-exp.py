#!/usr/bin/env python3
"""Lê as tabelas de experiência de 2016 do ToSBase (/game/exp-tables).

Uso: python3 scripts/parse-tosbase-exp.py DIRETORIO_DO_BRANCH_DE_DADOS
Grava data/research/tosbase-2016-exp.json: experiência base, de classe (por rank) e de companheiro.
"""
import gzip, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
src = Path(sys.argv[1])
manifest = json.loads((src / 'manifesto.json').read_text())['captures']


def page(key):
    cap = manifest[key]
    raw = gzip.open(src / Path(cap['file']).relative_to('data/tosbase-2016')).read().decode('utf-8', 'replace')
    body = re.sub(r'<script.*?</script>|<style.*?</style>', '', raw, flags=re.S)
    return re.sub(r' style="[^"]*"', '', body), cap


def num(v):
    return int(v.replace(',', '').strip())


def rows(table):
    out = []
    for r in re.findall(r'<tr><td>(.*?)</td></tr>', table, re.S):
        cells = re.findall(r'(?:^|</td><td>)([^<]*)', r)
        cells = [c for c in re.split(r'</td><td>', r)]
        if len(cells) >= 3:
            out.append({'level': num(cells[0]), 'required': num(cells[1]), 'total': num(cells[2])})
    return out


def tables(body):
    result = []
    for m in re.finditer(r'(?:<h2>([^<]*)</h2></div>\s*)?<table class="db_table">(.*?)</table>', body, re.S):
        result.append((m.group(1), rows(m.group(2))))
    return result


base_body, base_cap = page('gamepages/game_exp-tables_base')
class_body, class_cap = page('gamepages/game_exp-tables_class')
comp_body, comp_cap = page('gamepages/game_exp-tables_companion')
base = [t for t in tables(base_body) if t[1]][0][1]
class_tables = [(title or 'Rank 1', r) for title, r in tables(class_body) if r]
comp = [(title or 'Companion', r) for title, r in tables(comp_body) if r]
caps = {'base': base_cap, 'class': class_cap, 'companion': comp_cap}
out = {'source': 'ToSBase, tabelas de experiência (/game/exp-tables), capturas de 02/04/2016 e 01/04/2016',
       'captures': {k: {'captured_at': v['timestamp'], 'archive_url': v['archive_url'], 'html_sha256': v['sha256']} for k, v in caps.items()},
       'base': base, 'class_by_rank': {t: r for t, r in class_tables}, 'companion': {t: r for t, r in comp}}
(ROOT / 'data/research/tosbase-2016-exp.json').write_text(json.dumps(out, ensure_ascii=False, indent=0) + '\n', encoding='utf-8')
print(f"base: {len(base)} níveis (último {base[-1]}); classe: {[(t, len(r)) for t, r in class_tables]}; companheiro: {[(t, len(r)) for t, r in comp]}")
