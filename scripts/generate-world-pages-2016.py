#!/usr/bin/env python3
"""Gera quests, receitas e conjuntos de 2016 da wiki.

Escreve docs/original-2016/{quests,receitas,conjuntos}/ e data/original-2016/world-index.json.
Ordem de execução: generate-item-pages-2016.py, este, generate-monster-pages-2016.py.
"""
import json, shutil
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
read = lambda f: json.loads((ROOT / f).read_text(encoding='utf-8'))
quests = read('data/research/tosbase-2016-quests.json')['quests']
recipes = read('data/research/tosbase-2016-recipes.json')['recipes']
sets = read('data/research/tosbase-2016-sets.json')['sets']
monsters = read('data/research/tosbase-2016-monsters.json')['monsters']
ITEM_FOLDER = {i['id']: i['folder'] for i in read('data/original-2016/items-index.json')['items']}


def _esc(obj, skip=('type_line', 'archive_url', 'icon', 'html_sha256')):
    """Troca colchetes por parênteses nos textos, para o Markdown não ler nomes como "[x](1)" como link."""
    if isinstance(obj, str):
        return obj.replace('[', '(').replace(']', ')')
    if isinstance(obj, list):
        return [_esc(x, skip) for x in obj]
    if isinstance(obj, dict):
        return {k: (v if k in skip else _esc(v, skip)) for k, v in obj.items()}
    return obj

quests, recipes, sets, monsters = _esc(quests), _esc(recipes), _esc(sets), _esc(monsters)
BAND = 25
MAX_BAND_LEVEL = 300


def fmt(n):
    return '–' if n is None else f'{n:,.0f}'.replace(',', '.')


def item(item_id, name=None, rel='../itens/'):
    iid = str(item_id)
    label = name or iid
    return f'[{label}]({rel}{ITEM_FOLDER[iid]}/{iid}.md)' if iid in ITEM_FOLDER else label


def item_name(item_id):
    return None


for sub in ('quests', 'receitas', 'conjuntos'):
    d = ROOT / 'docs/original-2016' / sub
    if d.exists():
        shutil.rmtree(d)
    d.mkdir(parents=True)

# ---- quests
item_names = {}
for r in recipes.values():
    for m in r['materials']:
        item_names[str(m['item_id'])] = m['name']
    if r['result']:
        item_names[str(r['result']['item_id'])] = r['result']['name']
items_data = _esc(read('data/research/tosbase-2016-items.json')['items'])
for k, v in items_data.items():
    item_names[k] = v['name']


def band(q):
    lv = q.get('level')
    return 'outras' if lv is None or lv > MAX_BAND_LEVEL else str((lv // BAND) * BAND)


def quest_page(qid, q):
    lines = [f"# {q['name']}", '', '[Quests de 2016](README.md)', '', '| Campo | Valor |', '| --- | --- |',
             f"| Nível | {q['level'] if q.get('level') else '–'} |", f"| Nível necessário | {q['required_level'] or '–'} |",
             f"| NPC inicial | {q['start_npc']['name'] if q['start_npc'] else '–'} |", f"| Mapa | {q['map']['name'] if q['map'] else '–'} |", '']
    if q['required_quests']:
        lines += ['## Quests anteriores', ''] + [f"- [{r['name']}]({r['quest_id']}.md) (nível {r['level']})" if str(r['quest_id']) in quests else f"- {r['name']} (nível {r['level']})" for r in q['required_quests']] + ['']
    if q['task']:
        lines += ['## Tarefa', '']
        for line in q['task']:
            lines.append(f'- {line}')
        mons = [m for m in q['task_monsters'] if str(m['monster_id']) in monsters]
        if mons:
            lines += ['', 'Monstros: ' + ', '.join(f"[{m['name']}](../monstros/{m['monster_id']}.md)" for m in mons)]
        lines.append('')
    for key, title in (('start', 'Início'), ('progress', 'Durante'), ('end', 'Fim')):
        d = q['dialogs'].get(key)
        if d and (d['text'] or d['objective']):
            lines += [f'## {title}', ''] + ([f"> {l}" for l in d['text'].split('\n') if l.strip()] if d['text'] else [])
            if d['objective']:
                lines += ['', f"**Objetivo:** {d['objective']}"]
            lines.append('')
    if q['reward_all'] or q['reward_choice']:
        lines += ['## Recompensas', '']
        if q['reward_all']:
            lines += ['Recebe todos:', ''] + [f"- {item(r['item_id'], item_names.get(str(r['item_id'])))} × {r['count']}" for r in q['reward_all']] + ['']
        if q['reward_choice']:
            lines += ['Escolha um:', ''] + [f"- {item(r['item_id'], item_names.get(str(r['item_id'])))} × {r['count']}" for r in q['reward_choice']] + ['']
    lines += ['Textos originais em inglês.', '', '## Fonte', '', f"ToSBase, captura de {q['captured_at'][:4]}-{q['captured_at'][4:6]}-{q['captured_at'][6:8]} (Internet Archive). Valores exibidos pelo banco, sem confirmação no cliente.", '']
    return '\n'.join(lines)


groups = {}
for qid, q in quests.items():
    groups.setdefault(band(q), []).append((qid, q))
qout = ROOT / 'docs/original-2016/quests'
for qid, q in quests.items():
    (qout / f'{qid}.md').write_text(quest_page(qid, q), encoding='utf-8')
order = sorted([b for b in groups if b != 'outras'], key=int) + (['outras'] if 'outras' in groups else [])
index = ['# Quests de 2016', '', f'{len(quests)} quests do banco ToSBase de 2016, com NPC inicial, tarefa, diálogos e recompensas. Escolha uma faixa de nível.', '', '| Faixa de nível | Quests |', '| --- | --- |']
for b in order:
    label = 'Outros níveis' if b == 'outras' else f'{b} a {int(b) + BAND - 1}'
    index.append(f"| [{label}](nivel-{b}.md) | {len(groups[b])} |")
index += ['', 'Os valores e textos são os exibidos pelo ToSBase em março a junho de 2016.', '']
(qout / 'README.md').write_text('\n'.join(index), encoding='utf-8')
for b in order:
    label = 'outros níveis' if b == 'outras' else f'nível {b} a {int(b) + BAND - 1}'
    rows = [f'# Quests de {label}', '', '[Quests de 2016](README.md)', '', f'{len(groups[b])} quests.', '', '| Quest | Nível | NPC inicial | Mapa |', '| --- | --- | --- | --- |']
    for qid, q in sorted(groups[b], key=lambda x: (x[1]['level'] or 0, x[1]['name'])):
        rows.append(f"| [{q['name']}]({qid}.md) | {q['level'] or '–'} | {q['start_npc']['name'] if q['start_npc'] else '–'} | {q['map']['name'] if q['map'] else '–'} |")
    rows.append('')
    (qout / f'nivel-{b}.md').write_text('\n'.join(rows), encoding='utf-8')

# ---- receitas
rout = ROOT / 'docs/original-2016/receitas'
for rid, r in recipes.items():
    lines = [f"# {r['name']}", '', '[Receitas de 2016](README.md)', '']
    if r['result']:
        lines += [f"**Resultado:** {item(r['result']['item_id'], r['result']['name'])}", '']
    lines += ['## Materiais', '', '| Material | Quantidade |', '| --- | --- |'] + [f"| {item(m['item_id'], m['name'])} | {m['count']} |" for m in r['materials']] + ['']
    lines += ['## Fonte', '', f"ToSBase, captura de {r['captured_at'][:4]}-{r['captured_at'][4:6]}-{r['captured_at'][6:8]} (Internet Archive).", '']
    (rout / f'{rid}.md').write_text('\n'.join(lines), encoding='utf-8')
rows = ['# Receitas de 2016', '', f'{len(recipes)} receitas de fabricação do ToSBase de 2016, com o item que produzem e os materiais.', '', '| Receita | Resultado |', '| --- | --- |']
for rid, r in sorted(recipes.items(), key=lambda x: x[1]['name']):
    rows.append(f"| [{r['name']}]({rid}.md) | {r['result']['name'] if r['result'] else '–'} |")
rows.append('')
(rout / 'README.md').write_text('\n'.join(rows), encoding='utf-8')

# ---- conjuntos
sout = ROOT / 'docs/original-2016/conjuntos'
for sid, s in sets.items():
    lines = [f"# {s['name']}", '', '[Conjuntos de 2016](README.md)', '', '## Peças', ''] + [f"- {item(i['item_id'], i['name'])}" for i in s['items']] + ['']
    if s['effects']:
        lines += ['## Efeitos', '', '| Peças | Efeito |', '| --- | --- |'] + [f'| {k} | {v} |' for k, v in s['effects'].items()] + ['']
    lines += ['## Fonte', '', f"ToSBase, captura de {s['captured_at'][:4]}-{s['captured_at'][4:6]}-{s['captured_at'][6:8]} (Internet Archive).", '']
    (sout / f'{sid}.md').write_text('\n'.join(lines), encoding='utf-8')
rows = ['# Conjuntos de 2016', '', f'{len(sets)} conjuntos de equipamento do ToSBase de 2016, com as peças e o bônus de cada um.', '', '| Conjunto | Peças |', '| --- | --- |']
for sid, s in sorted(sets.items(), key=lambda x: x[1]['name']):
    rows.append(f"| [{s['name']}]({sid}.md) | {len(s['items'])} |")
rows.append('')
(sout / 'README.md').write_text('\n'.join(rows), encoding='utf-8')

(ROOT / 'data/original-2016/world-index.json').write_text(json.dumps({
    'quest_bands': [{'band': b, 'count': len(groups[b])} for b in order],
    'quests': [{'id': k, 'name': v['name']} for k, v in quests.items()],
    'recipes': [{'id': k, 'name': v['name']} for k, v in recipes.items()],
    'sets': [{'id': k, 'name': v['name']} for k, v in sets.items()]}, ensure_ascii=False, separators=(',', ':')) + '\n')
print(f'{len(quests)} quests, {len(recipes)} receitas, {len(sets)} conjuntos.')
