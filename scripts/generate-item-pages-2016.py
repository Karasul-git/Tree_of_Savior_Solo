#!/usr/bin/env python3
"""Gera o banco de itens de 2016 da wiki a partir de data/research/tosbase-2016-items.json.

Escreve docs/original-2016/itens/ e data/original-2016/items-index.json.
Uso: python3 scripts/generate-item-pages-2016.py (rode antes de generate-monster-pages-2016.py)
"""
import json, re, shutil
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
items = json.loads((ROOT / 'data/research/tosbase-2016-items.json').read_text(encoding='utf-8'))['items']
monsters = json.loads((ROOT / 'data/research/tosbase-2016-monsters.json').read_text(encoding='utf-8'))['monsters']

# (pasta, rótulo, seção, padrões do tipo)
CATEGORIES = [
    ('espadas', 'Espadas', 'Armas', r'^One-handed Sword'), ('espadas-de-duas-maos', 'Espadas de duas mãos', 'Armas', r'^Two-handed Sword'),
    ('floretes', 'Floretes', 'Armas', r'^Rapier'), ('lancas', 'Lanças', 'Armas', r'^One-handed Spear'),
    ('lancas-de-duas-maos', 'Lanças de duas mãos', 'Armas', r'^Two-handed Spear'), ('macas', 'Maças', 'Armas', r'^One-handed Blunt|^One-handed Mace'),
    ('macas-de-duas-maos', 'Maças de duas mãos', 'Armas', r'^Two-handed Blunt|^Two-handed Mace'), ('arcos', 'Arcos', 'Armas', r'^Bow'),
    ('bestas', 'Bestas', 'Armas', r'^Crossbow'), ('adagas', 'Adagas', 'Armas', r'^Dagger'), ('pistolas', 'Pistolas', 'Armas', r'^Pistol'),
    ('mosquetes', 'Mosquetes', 'Armas', r'^Musket'), ('canhoes', 'Canhões', 'Armas', r'^Cannon'),
    ('cajados', 'Cajados e bastões', 'Armas', r'^Staff|^Rod'), ('escudos', 'Escudos', 'Escudos', r'^Shield'),
    ('armaduras-de-corpo', 'Armaduras de corpo', 'Armaduras', r'^Top'), ('armaduras-de-pernas', 'Armaduras de pernas', 'Armaduras', r'^Bottom|^Pants'),
    ('luvas', 'Luvas', 'Armaduras', r'^Gloves'), ('botas', 'Botas', 'Armaduras', r'^Boots'),
    ('colares', 'Colares', 'Acessórios', r'^Necklace|^MagicAmulet'), ('pulseiras', 'Pulseiras', 'Acessórios', r'^Bracelet|^Armband'),
    ('trajes', 'Trajes e penteados', 'Aparência', r'^Costume|^Hair'),
    ('consumiveis', 'Consumíveis', 'Outros', r'^Drug'), ('gemas', 'Gemas', 'Outros', r'^Gem'), ('materiais', 'Materiais', 'Outros', r'^Material'),
    ('itens-de-quest', 'Itens de quest', 'Outros', r'^Quest'), ('colecoes', 'Itens de coleção', 'Outros', r'^Collection'),
    ('livros', 'Livros', 'Outros', r'^Book'), ('premium', 'Itens premium', 'Outros', r'^Premium'),
]
OTHER = ('outros', 'Outros itens', 'Outros')
FOLDERS = {c[0]: c for c in CATEGORIES + [OTHER + (None,)]}
LABEL = {'Slash': 'Corte', 'Pierce': 'Perfuração', 'Strike': 'Impacto', 'Missile': 'Projétil', 'Thrust': 'Estocada'}
TYPE_PT = {'One-handed Sword': 'Espada de uma mão', 'Two-handed Sword': 'Espada de duas mãos', 'One-handed Spear': 'Lança de uma mão',
           'Two-handed Spear': 'Lança de duas mãos', 'One-handed Blunt Weapon': 'Maça de uma mão', 'Two-handed Blunt Weapon': 'Maça de duas mãos',
           'Bow': 'Arco', 'Crossbow': 'Besta', 'Dagger': 'Adaga', 'Pistol': 'Pistola', 'Cannon': 'Canhão', 'Musket': 'Mosquete', 'Rapier': 'Florete',
           'Staff': 'Cajado', 'Rod': 'Bastão', 'Shield': 'Escudo', 'Top': 'Corpo', 'Bottom': 'Pernas', 'Gloves': 'Luvas', 'Boots': 'Botas',
           'Necklace': 'Colar', 'Bracelet': 'Pulseira', 'Armband': 'Braçadeira', 'Drug': 'Consumível', 'Gem': 'Gema', 'Material': 'Material',
           'Quest': 'Item de quest', 'Collection': 'Item de coleção', 'Book': 'Livro', 'Premium': 'Premium', 'Costume': 'Traje'}
MATERIAL_PT = {'Leather': 'Couro', 'Plate': 'Placa', 'Cloth': 'Tecido'}


def type_pt(tl):
    if not tl:
        return '–'
    m = re.match(r'^([^\[(]+?)\s*(?:[\[(]([^\])]+)[\])])?$', tl)
    if not m:
        return tl
    base, extra = m.group(1).strip(), m.group(2)
    base_pt = TYPE_PT.get(base, base)
    extra_pt = LABEL.get(extra, MATERIAL_PT.get(extra, extra)) if extra else None
    return f'{base_pt} ({extra_pt})' if extra_pt else base_pt


STAT = {'Physical Attack': 'Ataque físico', 'Magic Attack': 'Ataque mágico', 'Physical Defense': 'Defesa física', 'Magic Defense': 'Defesa mágica',
        'Defense': 'Defesa', 'ATK': 'Ataque', 'DEF': 'Defesa'}


def fmt(n):
    if n is None:
        return '–'
    return f'{n:,.0f}'.replace(',', '.') if float(n).is_integer() else str(n).replace('.', ',')


def category(item):
    tl = item.get('type_line') or ''
    for folder, label, section, pat in CATEGORIES:
        if re.search(pat, tl):
            return folder
    return 'outros'


by_cat = {f: [] for f in FOLDERS}
for iid, it in items.items():
    it['id'] = iid
    by_cat[category(it)].append(it)
for f in by_cat:
    by_cat[f].sort(key=lambda i: (i.get('required_level') or 0, i['name'], int(i['id']) if i['id'].isdigit() else 0))
by_cat = {f: v for f, v in by_cat.items() if v}
folder_of = {it['id']: f for f, v in by_cat.items() for it in v}

out = ROOT / 'docs/original-2016/itens'
if out.exists():
    shutil.rmtree(out)
out.mkdir(parents=True)


def item_link(iid, name, rel=''):
    return f"[{name}]({rel}../{folder_of[str(iid)]}/{iid}.md)" if str(iid) in folder_of else name


def page(it):
    folder = folder_of[it['id']]
    label = FOLDERS[folder][1]
    rows = [('Tipo', type_pt(it.get('type_line'))), ('Peso', fmt(it.get('weight'))), ('Nível necessário', fmt(it.get('required_level'))),
            ('Classes', ', '.join(it['required_classes']) if it.get('required_classes') else '–')]
    if it.get('grade'):
        rows.append(('Grau', str(it['grade'])))
    if it.get('tradeable') is not None or it.get('no_trade'):
        rows.append(('Negociável', 'Não' if (it.get('tradeable') is False or it.get('no_trade')) else 'Sim'))
    if it.get('potential'):
        rows.append(('Potencial', it['potential']))
    if it.get('durability'):
        rows.append(('Durabilidade', it['durability']))
    for k, v in it['stats'].items():
        rows.append((STAT.get(k, k), v))
    lines = [f"# {it['name']}", '', f"[{label}](README.md) · [Itens de 2016](../README.md)", '',
             '| Campo | Valor |', '| --- | --- |'] + [f'| {k} | {v} |' for k, v in rows] + ['']
    skip = ('Weight', 'Required', 'Potential', 'Durability', 'Not Tradeable', 'Tradeable', 'Maximum Sockets')
    desc = [l for l in it['tooltip_lines'][1:] if not l.startswith(skip) and l not in it['stats'] and l not in it['stats'].values()
            and l not in ('Potential', 'Durability') and not re.fullmatch(r'\d+ / \d+', l) and 'fansite' not in l]
    if desc:
        lines += ['## Descrição', ''] + [f'> {l}' for l in desc] + ['', 'Texto original em inglês.', '']
    if it['enhancement']:
        label_stat = STAT.get(it.get('enhancement_stat'), it.get('enhancement_stat') or 'Bônus')
        lines += ['## Aprimoramento', '', f'| Nível | {label_stat} | Chance de sucesso | Preço |', '| --- | --- | --- | --- |']
        lines += [f"| +{e['level']} | +{fmt(e['bonus'])} | {fmt(e['success_pct'])}% | {fmt(e['price'])} |" for e in it['enhancement']] + ['']
    if it['dropped_by']:
        lines += ['## Dropado por', '', '| Monstro | Chance |', '| --- | --- |']
        for d in it['dropped_by']:
            name = f"[{d['name']}](../../monstros/{d['monster_id']}.md)" if str(d['monster_id']) in monsters else d['name']
            lines.append(f"| {name} | {'–' if d['chance_pct'] is None else fmt(d['chance_pct']) + '%'} |")
        lines += ['']
    if it['crafted_by']:
        lines += ['## Fabricado por', ''] + [f"- {c['name']}" for c in it['crafted_by']] + ['']
    if it['required_for']:
        lines += ['## Usado em', ''] + [f"- {item_link(r['id'], r['name']) if r['type'] == 'item' else r['name']}" for r in it['required_for']] + ['']
    if it['quest_reward']:
        lines += ['## Recompensa de quest', ''] + [f"- {q['name']} (nível {q['level']})" for q in it['quest_reward']] + ['']
    if it.get('treasure_chest'):
        lines += ['## Baú do tesouro', '', it['treasure_chest'], '']
    if it['part_of']:
        lines += ['## Parte de', ''] + [f"- {c['name']}" for c in it['part_of']] + ['']
    if it['sets']:
        lines += ['## Conjuntos', ''] + [f"- {s['name']}" for s in it['sets']] + ['']
    if it['improved_skills']:
        lines += ['## Habilidade melhorada', ''] + [f'- {s}' for s in it['improved_skills']] + ['']
    c = it['captured_at']
    lines += ['## Fonte', '', f"ToSBase, captura de {c[:4]}-{c[4:6]}-{c[6:8]} (Internet Archive). Valores exibidos pelo banco, sem confirmação no cliente.", '']
    return '\n'.join(lines)


for folder, group in by_cat.items():
    d = out / folder
    d.mkdir()
    label = FOLDERS[folder][1]
    weapon = any('Physical Attack' in i['stats'] for i in group)
    head = ['Item', 'Nível', 'Grau'] + (['Ataque'] if weapon else (['Defesa'] if any('Physical Defense' in i['stats'] for i in group) else []))
    rows = ['# ' + label, '', '[Itens de 2016](../README.md)', '', f'{len(group)} itens, em ordem de nível necessário.', '',
            '| ' + ' | '.join(head) + ' |', '| ' + ' | '.join('---' for _ in head) + ' |']
    for i in group:
        cells = [f"[{i['name']}]({i['id']}.md)", fmt(i.get('required_level')), str(i.get('grade') or '–')]
        if weapon:
            cells.append(i['stats'].get('Physical Attack', '–'))
        elif 'Defesa' in head:
            cells.append(i['stats'].get('Physical Defense', '–'))
        rows.append('| ' + ' | '.join(cells) + ' |')
    rows.append('')
    (d / 'README.md').write_text('\n'.join(rows), encoding='utf-8')
    for i in group:
        (d / f"{i['id']}.md").write_text(page(i), encoding='utf-8')

sections = {}
for folder in by_cat:
    sections.setdefault(FOLDERS[folder][2], []).append(folder)
index = ['# Itens de 2016', '', f'{len(items)} itens do banco ToSBase de 2016, com atributos, aprimoramento, quem os solta, receitas e coleções. Escolha uma categoria.', '']
for section, folders in sections.items():
    index += [f'## {section}', '', '| Categoria | Itens |', '| --- | --- |'] + [f"| [{FOLDERS[f][1]}]({f}/README.md) | {len(by_cat[f])} |" for f in folders] + ['']
index += ['Os valores são os exibidos pelo ToSBase em março a junho de 2016.', '']
(out / 'README.md').write_text('\n'.join(index), encoding='utf-8')
(ROOT / 'data/original-2016').mkdir(exist_ok=True)
(ROOT / 'data/original-2016/items-index.json').write_text(json.dumps({
    'categories': [{'folder': f, 'label': FOLDERS[f][1], 'count': len(v)} for f, v in by_cat.items()],
    'items': [{'id': i['id'], 'name': i['name'], 'folder': f} for f, v in by_cat.items() for i in v]}, ensure_ascii=False, separators=(',', ':')) + '\n')
print(f'{len(items)} itens, {len(by_cat)} categorias.')
