#!/usr/bin/env python3
"""Gera o banco de equipamentos da wiki: dados e uma página por item.

Lê data/items/melia-equipment.json (valores do jogo atual, servidor Melia) e escreve
data/items/items.json e docs/itens/. Valores provisórios: a curva de equipamentos do
nosso jogo ainda será calibrada. Uso: python3 scripts/generate-item-pages.py
"""
import json, re, shutil, unicodedata
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
source = json.loads((ROOT / 'data/items/melia-equipment.json').read_text(encoding='utf-8'))

# categoria: (pasta, rótulo da página, rótulo singular, mãos)
CATEGORIES = {
    'Sword': ('espadas', 'Espadas', 'Espada', 'Uma mão'),
    'THSword': ('espadas-de-duas-maos', 'Espadas de duas mãos', 'Espada de duas mãos', 'Duas mãos'),
    'Spear': ('lancas', 'Lanças', 'Lança', 'Uma mão'),
    'THSpear': ('lancas-de-duas-maos', 'Lanças de duas mãos', 'Lança de duas mãos', 'Duas mãos'),
    'Rapier': ('floretes', 'Floretes', 'Florete', 'Uma mão'),
    'Mace': ('macas', 'Maças', 'Maça', 'Uma mão'),
    'THMace': ('macas-de-duas-maos', 'Maças de duas mãos', 'Maça de duas mãos', 'Duas mãos'),
    'Bow': ('bestas', 'Bestas', 'Besta', 'Uma mão'),
    'THBow': ('arcos', 'Arcos', 'Arco', 'Duas mãos'),
    'Dagger': ('adagas', 'Adagas', 'Adaga', 'Uma mão'),
    'Pistol': ('pistolas', 'Pistolas', 'Pistola', 'Uma mão'),
    'Musket': ('mosquetes', 'Mosquetes', 'Mosquete', 'Duas mãos'),
    'Cannon': ('canhoes', 'Canhões', 'Canhão', 'Duas mãos'),
    'Shield': ('escudos', 'Escudos', 'Escudo', None),
    'Shirt': ('armaduras-de-corpo', 'Armaduras de corpo', 'Armadura de corpo', None),
    'Pants': ('armaduras-de-pernas', 'Armaduras de pernas', 'Armadura de pernas', None),
    'Gloves': ('luvas', 'Luvas', 'Luvas', None),
    'Boots': ('botas', 'Botas', 'Botas', None),
    'Neck': ('colares', 'Colares', 'Colar', None),
    'Ring': ('aneis', 'Anéis', 'Anel', None),
}
SECTIONS = [
    ('Armas', ['Sword', 'THSword', 'Spear', 'THSpear', 'Rapier', 'Mace', 'THMace', 'Bow', 'THBow', 'Dagger', 'Pistol', 'Musket', 'Cannon']),
    ('Escudos', ['Shield']),
    ('Armaduras', ['Shirt', 'Pants', 'Gloves', 'Boots']),
    ('Acessórios', ['Neck', 'Ring']),
]
MATERIAL = {'Leather': 'Couro', 'Cloth': 'Tecido', 'Iron': 'Placa', 'Shield': 'Escudo', 'Ghost': 'Espectral'}
ATTACK_TYPE = {'Slash': 'Corte', 'Aries': 'Perfuração', 'Strike': 'Impacto', 'Arrow': 'Flecha', 'Gun': 'Tiro', 'Cannon': 'Canhão',
               'Magic': 'Mágico', 'Missile': 'Projétil'}
RESISTANCES = {'fireRes': 'Fogo', 'iceRes': 'Gelo', 'lightningRes': 'Raio', 'earthRes': 'Terra', 'poisonRes': 'Veneno',
               'holyRes': 'Sagrado', 'darkRes': 'Trevas', 'soulRes': 'Alma'}


def fmt(n):
    return f'{n:,.0f}'.replace(',', '.')


def slug(item):
    return item['className'].lower().replace('_', '-')


def keep(item):
    return item['grade'] != 99 and 'Days)' not in item['name'] and item['equipType1'] in CATEGORIES


items = [i for i in source['items'] if keep(i)]
items.sort(key=lambda i: (i['equipType1'], i['minLevel'], i['grade'], i['number']))

out = ROOT / 'docs/itens'
if out.exists():
    shutil.rmtree(out)
out.mkdir(parents=True)

curated = []
for item in items:
    cat = CATEGORIES[item['equipType1']]
    entry = {'id': item['itemId'], 'class_name': item['className'], 'name': item['name'], 'category': item['equipType1'],
             'folder': cat[0], 'slug': slug(item), 'level': item['minLevel'], 'grade': item['grade'],
             'weight': item['weight'], 'price': item['price'], 'sell_price': item['sellPrice']}
    for key, name in (('minAtk', 'atk_min'), ('maxAtk', 'atk_max'), ('mAtk', 'matk'), ('def', 'def'), ('mDef', 'mdef'), ('material', 'material')):
        if key in item:
            entry[name] = item[key]
    curated.append(entry)
(ROOT / 'data/items/items.json').write_text(json.dumps({
    'note': 'Equipamentos da wiki. Gerado por scripts/generate-item-pages.py; não editar à mão.',
    'source_id': 'F124', 'count': len(curated),
    'categories': [{'folder': v[0], 'label': v[1]} for v in CATEGORIES.values()], 'items': curated}, ensure_ascii=False, separators=(',', ':')) + '\n', encoding='utf-8')


def item_page(item):
    cat = CATEGORIES[item['equipType1']]
    rows = [('Tipo', f"[{cat[2]}]({'README.md'})" if False else cat[2]), ('Grau', str(item['grade'])), ('Nível mínimo', str(item['minLevel']))]
    if cat[3]:
        rows.append(('Empunhadura', cat[3]))
    if 'minAtk' in item:
        avg = (item['minAtk'] + item['maxAtk']) / 2
        rows.append(('Ataque', f"{fmt(item['minAtk'])} a {fmt(item['maxAtk'])} (média {fmt(avg)})"))
    if item.get('mAtk'):
        rows.append(('Ataque mágico', fmt(item['mAtk'])))
    if item.get('attackType'):
        rows.append(('Tipo de ataque', ATTACK_TYPE.get(item['attackType'], item['attackType'])))
    if 'def' in item:
        rows.append(('Defesa', fmt(item['def'])))
    if 'mDef' in item:
        rows.append(('Defesa mágica', fmt(item['mDef'])))
    if item.get('material'):
        rows.append(('Material', MATERIAL.get(item['material'], item['material'])))
    rows.append(('Peso', fmt(item['weight'])))
    if item['price']:
        rows.append(('Preço de compra', fmt(item['price'])))
    if item['sellPrice']:
        rows.append(('Preço de venda', fmt(item['sellPrice'])))
    extras = []
    for key, label in (('addMinAtk', 'Ataque mínimo adicional'), ('addMaxAtk', 'Ataque máximo adicional'), ('addMAtk', 'Ataque mágico adicional'),
                       ('addDef', 'Defesa adicional'), ('addMDef', 'Defesa mágica adicional'), ('pAtk', 'Ataque físico')):
        if item.get(key):
            extras.append((label, fmt(item[key])))
    for key, label in (('smallSizeBonus', 'Contra alvos pequenos'), ('middleSizeBonus', 'Contra alvos médios'), ('largeSizeBonus', 'Contra alvos grandes')):
        if item.get(key):
            extras.append((label, f"+{fmt(item[key])}"))
    for key, label in RESISTANCES.items():
        if item.get(key):
            extras.append((f'Resistência a {label.lower()}', fmt(item[key])))
    lines = [f"# {item['name']}", '',
             f"[{cat[1]}](README.md) · [Equipamentos](../README.md)", '',
             '| Campo | Valor |', '| --- | --- |'] + [f'| {k} | {v} |' for k, v in rows]
    if extras:
        lines += ['', '## Bônus', '', '| Bônus | Valor |', '| --- | --- |'] + [f'| {k} | {v} |' for k, v in extras]
    lines += ['', '## Sobre os valores', '',
              'Valores do jogo atual (Re:Build), ainda **provisórios** no nosso jogo: a curva de equipamentos será calibrada. [F124](../../fontes.md#f124)', '']
    return '\n'.join(lines)


by_cat = {}
for item in items:
    by_cat.setdefault(item['equipType1'], []).append(item)

for key, (folder, label, singular, hands) in CATEGORIES.items():
    group = by_cat.get(key, [])
    d = out / folder
    d.mkdir()
    weapon = group and 'minAtk' in group[0]
    head = ['Item', 'Nível', 'Grau'] + (['Ataque'] if weapon else ['Defesa', 'Material']) + ['Preço']
    lines = [f'# {label}', '', '[Equipamentos](../README.md)', '',
             f'{len(group)} itens, em ordem de nível. Valores provisórios.', '',
             '| ' + ' | '.join(head) + ' |', '| ' + ' | '.join('---' for _ in head) + ' |']
    for item in group:
        if weapon:
            mid = [f"{fmt(item['minAtk'])} a {fmt(item['maxAtk'])}"]
        else:
            mid = [fmt(item.get('def', 0)), MATERIAL.get(item.get('material'), '–')]
        lines.append('| ' + ' | '.join([f"[{item['name']}]({slug(item)}.md)", str(item['minLevel']), str(item['grade'])] + mid + [fmt(item['price']) if item['price'] else '–']) + ' |')
    lines.append('')
    (d / 'README.md').write_text('\n'.join(lines), encoding='utf-8')
    for item in group:
        (d / f'{slug(item)}.md').write_text(item_page(item), encoding='utf-8')

index = ['# Equipamentos', '',
         f'{len(items)} equipamentos: armas, escudos, armaduras e acessórios, do nível 1 ao 280. Cada item tem a sua página. Os valores são provisórios: vêm do jogo atual e ainda serão calibrados para o nosso.', '',
         '**Grau:** o servidor organiza os equipamentos em linhas de grau (1 a 4). O grau 1 é a linha comum, e os maiores graus vêm mais fortes nos mesmos níveis.', '']
for title, keys in SECTIONS:
    index += [f'## {title}', '', '| Categoria | Itens |', '| --- | --- |']
    for key in keys:
        index.append(f'| [{CATEGORIES[key][1]}]({CATEGORIES[key][0]}/README.md) | {len(by_cat.get(key, []))} |')
    index.append('')
(out / 'README.md').write_text('\n'.join(index), encoding='utf-8')
print(f'{len(items)} equipamentos, {len(CATEGORIES)} categorias.')
