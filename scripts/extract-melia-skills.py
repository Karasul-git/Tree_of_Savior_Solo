#!/usr/bin/env python3
"""Extrai de skills.txt do Melia os dados das habilidades usadas pelo projeto.

Uso: python3 scripts/extract-melia-skills.py CAMINHO/skills.txt COMMIT DATA
Grava data/skills/melia-skills.json com todas as habilidades (valores originais,
antes da escala do projeto). Só o resumo de dano e uso; as regras ficam no código do Melia.
"""
import json, re, sys
from pathlib import Path

src, commit, date = sys.argv[1:4]
ROOT = Path(__file__).resolve().parent.parent

def field(line, key):
    m = re.search(r'\b' + key + r': ("[^"]*"|\[[^\]]*\]|[^,}]*)', line)
    return m.group(1).strip().strip('"') if m else None

def num(v):
    try:
        return float(v)
    except (TypeError, ValueError):
        return None

skills = {}
for line in open(src, encoding='utf-8-sig'):
    m = re.match(r'\{ skillId: (\d+), className: "([^"]*)", name: "([^"]*)"', line)
    if not m:
        continue
    tags = re.findall(r'"([^"]+)"', field(line, 'tags') or '')
    cd = num(field(line, 'cooldownTime'))
    sp = num(field(line, 'basicSp'))
    skills[m.group(2)] = {
        'id': int(m.group(1)), 'name': m.group(3),
        'activation': field(line, 'activationType'), 'use': field(line, 'useType'),
        'attack_type': field(line, 'attackType'), 'class_type': field(line, 'classType'),
        'tags': tags,
        'factor_pct': num(field(line, 'factor')), 'factor_by_level_pct': num(field(line, 'factorByLevel')),
        'hits': int(num(field(line, 'hitCount')) or 1) * int(num(field(line, 'multiHitCount')) or 1),
        'sp': sp, 'cooldown_s': (cd / 1000 if cd is not None else None),
        'range': num(field(line, 'maxRange')), 'cast_ms': num(field(line, 'shootTime')),
    }
out = {
    'source_id': 'F124',
    'note': 'Valores originais do Melia (skills.txt), antes da escala do projeto.',
    'repository': 'NoCode-NoLife/melia', 'commit': commit, 'commit_date': date,
    'skill_count': len(skills), 'skills': skills,
}
(ROOT / 'data/skills/melia-skills.json').write_text(json.dumps(out, ensure_ascii=False, separators=(',', ':')) + '\n')
print(f'{len(skills)} habilidades extraídas.')
