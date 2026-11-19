#!/usr/bin/env python3
"""Gera docs/original-2016/experiencia.md a partir de data/research/tosbase-2016-exp.json."""
import json
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
d = json.loads((ROOT / 'data/research/tosbase-2016-exp.json').read_text(encoding='utf-8'))
fmt = lambda n: f'{n:,}'.replace(',', '.')


def table(rows):
    return ['| Nível | Experiência necessária | Total acumulado |', '| --- | --- | --- |'] + [f"| {r['level']} | {fmt(r['required'])} | {fmt(r['total'])} |" for r in rows] + ['']


lines = ['# Tabelas de experiência de 2016', '', '[Fontes](../fontes.md#f145)', '',
         'Experiência necessária para subir de nível em 2016, segundo o ToSBase ([F145](../fontes.md#f145)): do **personagem** (níveis 1 a 302), da **classe** (sete ranks de 15 níveis) e do **companheiro**. "Necessária" é a experiência para chegar ao nível indicado a partir do anterior; "total" é a soma desde o início.', '',
         '## Personagem', '', f"Níveis 1 a {d['base'][-1]['level']}. Chegar ao nível 100 exige {fmt(next(r for r in d['base'] if r['level'] == 100)['total'])} de experiência; ao 200, {fmt(next(r for r in d['base'] if r['level'] == 200)['total'])}.", '']
lines += table(d['base'])
lines += ['## Classe', '', 'A experiência de classe sobe os 15 níveis de classe de cada rank. Cada rank tem a sua própria tabela.', '']
for title, rows in d['class_by_rank'].items():
    if title.startswith('Rank'):
        lines += [f'### {title}', ''] + table(rows)
total = d['class_by_rank'].get('Total EXP')
if total:
    lines += ['### Total até cada rank', '', '| Rank | Experiência do rank | Total acumulado |', '| --- | --- | --- |'] + [f"| {r['level']} | {fmt(r['required'])} | {fmt(r['total'])} |" for r in total] + ['']
lines += ['## Companheiro', '']
for title, rows in d['companion'].items():
    lines += [f'### {title}', ''] + table(rows)
c = d['captures']
lines += ['## Fonte', '', f"ToSBase, capturas de {c['base']['captured_at'][:4]}-{c['base']['captured_at'][4:6]}-{c['base']['captured_at'][6:8]} (base), {c['class']['captured_at'][:4]}-{c['class']['captured_at'][4:6]}-{c['class']['captured_at'][6:8]} (classe) e {c['companion']['captured_at'][:4]}-{c['companion']['captured_at'][4:6]}-{c['companion']['captured_at'][6:8]} (companheiro), pelo Internet Archive.", '']
(ROOT / 'docs/original-2016/experiencia.md').write_text('\n'.join(lines), encoding='utf-8')
print('ok')
