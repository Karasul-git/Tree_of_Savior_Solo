#!/usr/bin/env python3
"""Extrai de items.txt do Melia os equipamentos usados pela wiki.

Uso: python3 scripts/extract-melia-items.py CAMINHO/items.txt COMMIT DATA
Grava data/items/melia-equipment.json: armas, escudos, armaduras e acessórios das linhas
padrão de progressão, até o nível 280. Valores do jogo atual, sem alteração.
"""
import json, re, sys
from collections import Counter
from pathlib import Path

src, commit, date = sys.argv[1:4]
ROOT = Path(__file__).resolve().parent.parent
LINES = r'^(SWD|TSW|SPR|TSP|RAP|MAC|TMAC|BOW|TBW|DAG|PST|MUS|CAN|SHD|TOP|LEG|HAND|FOOT|NECK|BRC)(\d+)_(\d+)$'
MAX_LEVEL = 280
PAIR = re.compile(r'(\w+): ("[^"]*"|-?\d+(?:\.\d+)?|[A-Za-z_]\w*)')

items, skipped = [], Counter()
for line in open(src, encoding='utf-8-sig'):
    if not line.startswith('{ itemId'):
        continue
    head = line.split(', script:')[0].split(', leftHandSkill:')[0] if False else line.split(', script:')[0]
    row = {}
    for key, value in PAIR.findall(head):
        row[key] = value.strip('"') if value.startswith('"') else (float(value) if re.fullmatch(r'-?\d+(\.\d+)?', value) else value)
        if isinstance(row[key], float) and row[key].is_integer():
            row[key] = int(row[key])
    m = re.match(LINES, row.get('className', ''))
    if row.get('type') != 'Equip' or not m:
        continue
    if row.get('minLevel', 0) > MAX_LEVEL:
        skipped['acima do nível 280'] += 1
        continue
    row['line'], row['grade'], row['number'] = m.group(1), int(m.group(2)), int(m.group(3))
    items.append(row)

out = {'source_id': 'F124', 'repository': 'NoCode-NoLife/melia', 'commit': commit, 'commit_date': date,
       'note': 'Equipamentos das linhas padrão (armas, escudos, armaduras, colares e anéis) até o nível 280, com os valores do jogo atual.',
       'skipped': dict(skipped), 'count': len(items), 'items': items}
(ROOT / 'data/items/melia-equipment.json').write_text(json.dumps(out, ensure_ascii=False, separators=(',', ':')) + '\n')
print(len(items), 'equipamentos;', dict(skipped))
print(Counter(i['line'] for i in items))
