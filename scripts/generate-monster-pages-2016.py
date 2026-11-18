#!/usr/bin/env python3
"""Gera o bestiário de 2016 da wiki a partir de data/research/tosbase-2016-monsters.json.

Escreve docs/original-2016/monstros/ (índice, páginas por faixa de nível e uma página por monstro)
e data/original-2016/monsters-index.json. Uso: python3 scripts/generate-monster-pages-2016.py
"""
import json, shutil
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
data = json.loads((ROOT / 'data/research/tosbase-2016-monsters.json').read_text(encoding='utf-8'))['monsters']

ITEM_FOLDER = {}
_idx = ROOT / 'data/original-2016/items-index.json'
if _idx.exists():
    ITEM_FOLDER = {i['id']: i['folder'] for i in json.loads(_idx.read_text(encoding='utf-8'))['items']}

TYPE = {'Normal': 'Comum', 'Boss': 'Chefe', 'Elite': 'Elite', 'Material': 'Material', 'NPC': 'NPC'}
RACE = {'Demon': 'Demônio (Velnias)', 'Plant': 'Planta', 'Beast': 'Fera', 'Mutant': 'Mutante (Paramune)', 'Insect': 'Inseto', '?': 'Desconhecida'}
ELEMENT = {'Fire': 'Fogo', 'Ice': 'Gelo', 'Lightning': 'Raio', 'Earth': 'Terra', 'Poison': 'Veneno', 'Holy': 'Sagrado', 'Dark': 'Trevas',
           'Soul': 'Alma', 'Melee': 'Físico', '?': 'Desconhecido'}
ARMOR = {'Leather': 'Couro', 'Cloth': 'Tecido', 'Iron': 'Placa', 'Ghost': 'Espectral', '?': 'Desconhecida'}
MOVE = {'Normal': 'Anda', 'Flying': 'Voa', 'Static': 'Parado', '?': 'Desconhecido'}
BAND = 25


def fmt(n):
    return '–' if n is None else f'{n:,.0f}'.replace(',', '.') if isinstance(n, (int, float)) and float(n).is_integer() else str(n).replace('.', ',')


def pair(r):
    return '–' if not r else (fmt(r[0]) if len(r) == 1 or r[0] == r[-1] else f'{fmt(r[0])} a {fmt(r[-1])}')


def band_of(level):
    lo = (level // BAND) * BAND
    return lo, lo + BAND - 1


mons = sorted(data.values(), key=lambda m: (m['level'], m['name'], int(m['tosbase_id'])))
out = ROOT / 'docs/original-2016/monstros'
if out.exists():
    shutil.rmtree(out)
out.mkdir(parents=True)

for m in mons:
    c, a = m.get('combat', {}), m.get('attributes', {})
    rows = [('Nível', fmt(m['level'])), ('Tipo', TYPE.get(m['type'], m['type'])), ('Raça', RACE.get(m['race'], m['race'])),
            ('Elemento', ELEMENT.get(m['element'], m['element'])), ('Armadura', ARMOR.get(m['armor'], m['armor'])), ('Tamanho', m.get('size') or '–'),
            ('Movimento', MOVE.get(m.get('move'), m.get('move') or '–')), ('Velocidade', pair(m.get('speed'))), ('HP', fmt(m.get('hp'))),
            ('Experiência base', fmt(m.get('base_exp'))), ('Experiência de classe', fmt(m.get('class_exp')))]
    lines = [f"# {m['name']}", '', f"[Bestiário de 2016](README.md) · [Faixa de nível {band_of(m['level'])[0]} a {band_of(m['level'])[1]}](nivel-{band_of(m['level'])[0]}.md)", '',
             '| Campo | Valor |', '| --- | --- |'] + [f'| {k} | {v} |' for k, v in rows] + ['']
    if m.get('description'):
        lines += ['## Descrição', '', f"> {m['description']}", '', 'Texto original em inglês.', '']
    if a or c:
        lines += ['## Atributos e combate', '']
        if a:
            lines += ['| ' + ' | '.join(a) + ' |', '| ' + ' | '.join('---' for _ in a) + ' |', '| ' + ' | '.join(fmt(v) for v in a.values()) + ' |', '']
        if c:
            names = {'Accuracy': 'Precisão', 'Evasion': 'Esquiva', 'DEF': 'Defesa', 'MDEF': 'Defesa mágica', 'ATK': 'Ataque', 'MATK': 'Ataque mágico',
                     'Crit Rate': 'Taxa de crítico', 'Crit Resistance': 'Resistência a crítico', 'Crit Damage': 'Dano crítico', 'M. Amp.': 'Amplificação mágica'}
            lines += ['| Estatística | Valor |', '| --- | --- |'] + [f"| {names.get(k, k)} | {pair(v) if isinstance(v, list) else fmt(v)} |" for k, v in c.items() if k not in ('HP',)] + ['']
    if m.get('drops'):
        lines += ['## Drops', '', '| Item | Chance |', '| --- | --- |'] + [f"| {('[' + d['name'] + '](../itens/' + ITEM_FOLDER[str(d['item_id'])] + '/' + str(d['item_id']) + '.md)') if str(d['item_id']) in ITEM_FOLDER else d['name']} | {'–' if d['chance_pct'] is None else fmt(d['chance_pct']) + '%'} |" for d in m['drops']] + ['', 'Chances marcadas com "–" não eram conhecidas pelo banco naquela data.', '']
    if m.get('locations'):
        lines += ['## Onde aparece', '', '| Mapa | Quantidade | Renascimento |', '| --- | --- | --- |'] + [f"| {l['map']} | {fmt(l['spawn_count'])} | {fmt(l['respawn_seconds']) + ' s' if l['respawn_seconds'] is not None else '–'} |" for l in m['locations']] + ['']
    lines += ['## Fonte', '', f"ToSBase, captura de {m['captured_at'][:4]}-{m['captured_at'][4:6]}-{m['captured_at'][6:8]} (Internet Archive). Valores exibidos pelo banco, sem confirmação no cliente.", '']
    (out / f"{m['tosbase_id']}.md").write_text('\n'.join(lines), encoding='utf-8')

bands = {}
for m in mons:
    bands.setdefault(band_of(m['level'])[0], []).append(m)
index = ['# Bestiário de 2016', '', f'{len(mons)} monstros do banco ToSBase de 2016, com nível, atributos, HP, experiência, drops e onde aparecem. Escolha uma faixa de nível.', '',
         '| Faixa de nível | Monstros |', '| --- | --- |']
for lo in sorted(bands):
    index.append(f"| [{lo} a {lo + BAND - 1}](nivel-{lo}.md) | {len(bands[lo])} |")
index += ['', 'Os valores são os exibidos pelo ToSBase em março a junho de 2016. Chances de drop desconhecidas aparecem como "–".', '']
(out / 'README.md').write_text('\n'.join(index), encoding='utf-8')
for lo, group in bands.items():
    lines = [f'# Monstros de nível {lo} a {lo + BAND - 1}', '', '[Bestiário de 2016](README.md)', '', f'{len(group)} monstros.', '',
             '| Monstro | Nível | Tipo | Raça | Elemento | HP | Experiência base |', '| --- | --- | --- | --- | --- | --- | --- |']
    for m in group:
        lines.append(f"| [{m['name']}]({m['tosbase_id']}.md) | {fmt(m['level'])} | {TYPE.get(m['type'], m['type'])} | {RACE.get(m['race'], m['race'])} | {ELEMENT.get(m['element'], m['element'])} | {fmt(m.get('hp'))} | {fmt(m.get('base_exp'))} |")
    lines.append('')
    (out / f'nivel-{lo}.md').write_text('\n'.join(lines), encoding='utf-8')

(ROOT / 'data/original-2016').mkdir(exist_ok=True)
(ROOT / 'data/original-2016/monsters-index.json').write_text(json.dumps({'bands': [{'low': lo, 'high': lo + BAND - 1, 'count': len(g)} for lo, g in sorted(bands.items())],
    'monsters': [{'id': m['tosbase_id'], 'name': m['name'], 'level': m['level']} for m in mons]}, ensure_ascii=False, separators=(',', ':')) + '\n')
print(f'{len(mons)} monstros, {len(bands)} faixas.')
