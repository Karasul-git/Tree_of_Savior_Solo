#!/usr/bin/env python3
"""Gera mapas, NPCs, atributos, coleções e efeitos de estado de 2016 da wiki.

Escreve docs/original-2016/{mapas,npcs,atributos,colecoes,efeitos}/ e data/original-2016/world2-index.json.
Ordem: generate-item-pages-2016.py, generate-world-pages-2016.py, este, e de novo os três anteriores
(os links entre páginas dependem dos índices uns dos outros).
"""
import json, shutil
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
read = lambda f: json.loads((ROOT / f).read_text(encoding='utf-8'))


def _esc(obj, skip=('archive_url', 'html_sha256')):
    if isinstance(obj, str):
        return obj.replace('[', '(').replace(']', ')')
    if isinstance(obj, list):
        return [_esc(x, skip) for x in obj]
    if isinstance(obj, dict):
        return {k: (v if k in skip else _esc(v, skip)) for k, v in obj.items()}
    return obj


maps = _esc(read('data/research/tosbase-2016-maps.json')['entries'])
npcs = _esc(read('data/research/tosbase-2016-npcs.json')['entries'])
abilities = _esc(read('data/research/tosbase-2016-abilities.json')['entries'])
collections = _esc(read('data/research/tosbase-2016-collections.json')['entries'])
effects = _esc(read('data/research/tosbase-2016-status-effects.json')['entries'])
monsters = read('data/research/tosbase-2016-monsters.json')['monsters']
quests = read('data/research/tosbase-2016-quests.json')['quests']
ITEM = {i['id']: (i['folder'], i['name']) for i in read('data/original-2016/items-index.json')['items']}
TYPE = {'City': 'Cidade', 'Field': 'Campo', 'Dungeon': 'Masmorra', 'Instance': 'Instância'}
FACTION = {'Neutral': 'Neutra', 'Monster': 'Monstro', 'Law': 'Lei', 'Chaos': 'Caos'}


def fmt(n):
    return '–' if n is None else f'{n:,.0f}'.replace(',', '.')


def src(x):
    c = x['captured_at']
    return ['## Fonte', '', f"ToSBase, captura de {c[:4]}-{c[4:6]}-{c[6:8]} (Internet Archive). Valores exibidos pelo banco, sem confirmação no cliente.", '']


def mp(mid, name):
    return f'[{name}](../mapas/{mid}.md)' if str(mid) in maps else name


def np_(nid, name):
    return f'[{name}](../npcs/{nid}.md)' if str(nid) in npcs else name


for sub in ('mapas', 'npcs', 'atributos', 'colecoes', 'efeitos'):
    d = ROOT / 'docs/original-2016' / sub
    if d.exists():
        shutil.rmtree(d)
    d.mkdir(parents=True)
base = ROOT / 'docs/original-2016'

# ---- mapas
for mid, m in maps.items():
    lines = [f"# {m['name']}", '', '[Mapas de 2016](README.md)', '', '| Campo | Valor |', '| --- | --- |',
             f"| Região | {m['area'] or '–'} |", f"| Tipo | {TYPE.get(m['type'], m['type'] or '–')} |", f"| Nível | {fmt(m['level'])} |",
             f"| Orbe de ressurreição | {'Sim' if m['revive_orb'] == 'Yes' else ('Não' if m['revive_orb'] == 'No' else '–')} |", '']
    if m['connected']:
        lines += ['## Mapas conectados', ''] + [f"- {mp(c['map_id'], c['name'])} (nível {fmt(c['level'])}{', ' + TYPE.get(c['type'], c['type']) if c['type'] else ''})" for c in m['connected']] + ['']
    if m['monsters']:
        lines += ['## Monstros', '', '| Monstro | População | Renascimento |', '| --- | --- | --- |']
        lines += [f"| {('[' + x['name'] + '](../monstros/' + str(x['monster_id']) + '.md)') if str(x['monster_id']) in monsters else x['name']} | {x['population']} | {x['respawn_seconds']} s |" for x in m['monsters']] + ['']
    if m['npcs']:
        seen, rows = set(), []
        for n in m['npcs']:
            if (n['npc_id'], n['name']) not in seen:
                seen.add((n['npc_id'], n['name']))
                rows.append(f"- {np_(n['npc_id'], n['name'])}")
        lines += ['## NPCs', ''] + rows + ['']
    (base / 'mapas' / f'{mid}.md').write_text('\n'.join(lines + src(m)), encoding='utf-8')
by_type = {}
for mid, m in maps.items():
    by_type.setdefault(m['type'] or 'Outros', []).append((mid, m))
idx = ['# Mapas de 2016', '', f'{len(maps)} mapas do ToSBase de 2016, com região, nível, mapas conectados, monstros e NPCs.', '']
for t, group in sorted(by_type.items()):
    idx += [f'## {TYPE.get(t, t)}', '', '| Mapa | Região | Nível |', '| --- | --- | --- |']
    idx += [f"| [{m['name']}]({mid}.md) | {m['area'] or '–'} | {fmt(m['level'])} |" for mid, m in sorted(group, key=lambda x: (x[1]['level'] or 0, x[1]['name']))] + ['']
(base / 'mapas' / 'README.md').write_text('\n'.join(idx), encoding='utf-8')

# ---- NPCs
for nid, n in npcs.items():
    lines = [f"# {n['name']}", '', '[NPCs de 2016](README.md)', '', '| Campo | Valor |', '| --- | --- |', f"| Nível | {fmt(n.get('level'))} |",
             f"| Facção | {FACTION.get(n.get('faction'), n.get('faction') or '–')} |", f"| Raça | {n.get('race') or '–'} |", f"| Tamanho | {n.get('size') or '–'} |", '']
    if n['locations']:
        lines += ['## Onde está', '', '| Mapa | Nome no mapa | Quests |', '| --- | --- | --- |']
        for l in n['locations']:
            qs = ', '.join((f"[{q['name']}](../quests/{q['quest_id']}.md)" if str(q['quest_id']) in quests else q['name']) for q in l['quests']) or '–'
            lines.append(f"| {mp(l['map_id'], l['map']) if l['map_id'] else l['map']} | {l['name'] or '–'} | {qs} |")
        lines.append('')
    (base / 'npcs' / f'{nid}.md').write_text('\n'.join(lines + src(n)), encoding='utf-8')
idx = ['# NPCs de 2016', '', f'{len(npcs)} NPCs do ToSBase de 2016, com facção, onde estão e as quests que oferecem.', '', '| NPC | Nível | Mapas |', '| --- | --- | --- |']
for nid, n in sorted(npcs.items(), key=lambda x: x[1]['name'].lower()):
    idx.append(f"| [{n['name']}]({nid}.md) | {fmt(n.get('level'))} | {', '.join(dict.fromkeys(l['map'] for l in n['locations'][:3])) or '–'} |")
(base / 'npcs' / 'README.md').write_text('\n'.join(idx + ['']), encoding='utf-8')

# ---- atributos
for aid, a in abilities.items():
    lines = [f"# {a['name']}", '', '[Atributos de 2016](README.md)', '', '| Campo | Valor |', '| --- | --- |', f"| Classe | {a.get('class') or '–'} |",
             f"| Círculo | {a['circle'] or '–'} |", f"| Habilidade | {a.get('skill') or '–'} |", f"| Nível máximo | {fmt(a.get('max_level'))} |",
             f"| Requisito | {a.get('required') or '–'} |", f"| Tipo | {a.get('type') or '–'} |", '']
    if a.get('description'):
        lines += ['## Descrição', '', f"> {a['description']}", '', 'Texto original em inglês.', '']
    if a.get('effect') and a['effect'] != '-':
        lines += ['## Efeito', '', a['effect'], '']
    (base / 'atributos' / f'{aid}.md').write_text('\n'.join(lines + src(a)), encoding='utf-8')
by_class = {}
for aid, a in abilities.items():
    by_class.setdefault(a.get('class') or 'Outras', []).append((aid, a))
idx = ['# Atributos de habilidade de 2016', '', f'{len(abilities)} atributos (aprimoramentos de habilidade) do ToSBase de 2016, por classe.', '']
for c, group in sorted(by_class.items()):
    idx += [f'## {c}', '', '| Atributo | Habilidade | Círculo |', '| --- | --- | --- |'] + [f"| [{a['name']}]({aid}.md) | {a.get('skill') or '–'} | {a['circle'] or '–'} |" for aid, a in sorted(group, key=lambda x: (x[1]['circle'] or 9, x[1]['name']))] + ['']
(base / 'atributos' / 'README.md').write_text('\n'.join(idx), encoding='utf-8')

# ---- coleções
for cid, c in collections.items():
    lines = [f"# {c['name']}", '', '[Coleções de 2016](README.md)', '', f"**Bônus:** {c['bonus'] or '–'}", '', '## Itens', '']
    lines += [f"- [{ITEM[str(i)][1]}](../itens/{ITEM[str(i)][0]}/{i}.md)" if str(i) in ITEM else f'- item {i}' for i in c['items']] + ['']
    (base / 'colecoes' / f'{cid}.md').write_text('\n'.join(lines + src(c)), encoding='utf-8')
idx = ['# Coleções de 2016', '', f'{len(collections)} coleções do ToSBase de 2016, com os itens que as completam e o bônus.', '', '| Coleção | Itens | Bônus |', '| --- | --- | --- |']
idx += [f"| [{c['name']}]({cid}.md) | {len(c['items'])} | {c['bonus'] or '–'} |" for cid, c in sorted(collections.items(), key=lambda x: x[1]['name'])]
(base / 'colecoes' / 'README.md').write_text('\n'.join(idx + ['']), encoding='utf-8')

# ---- efeitos de estado
for eid, e in effects.items():
    lines = [f"# {e['name']}", '', '[Efeitos de estado de 2016](README.md)', '', '| Nível | Tipo | Descrição |', '| --- | --- | --- |']
    lines += [f"| {fmt(x['level'])} | {x['type']} | {x['description']} |" for x in e['entries']] + ['']
    (base / 'efeitos' / f'{eid}.md').write_text('\n'.join(lines + src(e)), encoding='utf-8')
idx = ['# Efeitos de estado de 2016', '', f'{len(effects)} efeitos de estado do ToSBase de 2016.', '', '| Efeito | Tipo | Descrição |', '| --- | --- | --- |']
idx += [f"| [{e['name']}]({eid}.md) | {e['entries'][0]['type'] if e['entries'] else '–'} | {e['entries'][0]['description'] if e['entries'] else '–'} |" for eid, e in sorted(effects.items(), key=lambda x: x[1]['name'])]
(base / 'efeitos' / 'README.md').write_text('\n'.join(idx + ['']), encoding='utf-8')

(ROOT / 'data/original-2016/world2-index.json').write_text(json.dumps({
    'maps': [{'id': k, 'name': v['name']} for k, v in maps.items()], 'npcs': [{'id': k, 'name': v['name']} for k, v in npcs.items()],
    'abilities': [{'id': k, 'name': v['name']} for k, v in abilities.items()], 'collections': [{'id': k, 'name': v['name']} for k, v in collections.items()],
    'effects': [{'id': k, 'name': v['name']} for k, v in effects.items()]}, ensure_ascii=False, separators=(',', ':')) + '\n')
print(f'{len(maps)} mapas, {len(npcs)} NPCs, {len(abilities)} atributos, {len(collections)} coleções, {len(effects)} efeitos.')
