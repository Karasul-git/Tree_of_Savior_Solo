#!/usr/bin/env python3
"""Lê quests, receitas e conjuntos do ToSBase de 2016.

Uso: python3 scripts/parse-tosbase-world.py DIRETORIO_DO_BRANCH_DE_DADOS
Grava data/research/tosbase-2016-quests.json, -recipes.json e -sets.json (sem o HTML bruto).
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


def num(v):
    m = re.search(r'-?\d[\d,]*(?:\.\d+)?', v or '')
    return int(float(m.group(0).replace(',', ''))) if m else None


def load(mid, cap):
    raw = gzip.open(src / Path(cap['file']).relative_to('data/tosbase-2016')).read().decode('utf-8', 'replace')
    body = re.sub(r'<script.*?</script>|<style.*?</style>', '', raw, flags=re.S)
    body = re.sub(r' style="[^"]*"', '', body)
    # remove os blocos de tooltip, que repetem dados de itens
    body = re.sub(r'<div id="(?:item_reward_tooltip|item_icon_tooltip|item_tooltip|set_tooltip)_\d+".*?(?=<div id="(?:item_reward_tooltip|item_icon_tooltip|item_tooltip|set_tooltip)_|<h2|<div id="comments"|<div id="footer")', '', body, flags=re.S)
    return body


def items_in(fragment):
    # o bloco "Select One" do ToSBase vem sem o link <a>; o id está no atributo data-tooltip da imagem
    out = []
    for iid, cnt in re.findall(r'data-tooltip="item_reward_tooltip_(\d+)"[^>]*/>\s*<span[^>]*>(\d+)</span>', fragment, re.S):
        out.append({'item_id': int(iid), 'count': int(cnt)})
    return out


def parse_quest(body):
    m = re.search(r'<h1>(.*?)</h1>', body, re.S)
    if not m:
        return None
    q = {'name': text(m.group(1))}
    basic = re.search(r'<h3>Basic</h3>(.*?)</div></div>', body, re.S)
    b = basic.group(1) if basic else ''
    npc = re.search(r'<b>Start NPC:</b>\s*<a href="database/npcs/(\d+)/">(.*?)</a>', b, re.S)
    q['start_npc'] = {'id': int(npc.group(1)), 'name': text(npc.group(2))} if npc else None
    mp = re.search(r'<b>Map:</b>\s*<a href="game/world-map/(\d+)/">(.*?)</a>', b, re.S)
    q['map'] = {'id': int(mp.group(1)), 'name': text(mp.group(2))} if mp else None
    lv = re.search(r'<b>Level:</b>\s*(\d+)(?:\s*\(Required Level:\s*(\d+)\))?', b)
    q['level'] = int(lv.group(1)) if lv else None
    q['required_level'] = int(lv.group(2)) if lv and lv.group(2) else None
    task = re.search(r'<h3>Task</h3><div>(.*?)</div></div>', body, re.S)
    t = task.group(1) if task else ''
    q['task'] = [l for l in text(t).split('\n') if l.strip()]
    q['task_monsters'] = [{'monster_id': int(i), 'name': text(n), 'count': int(c)} for i, n, c in re.findall(r'href="database/monsters/(\d+)/">(.*?)</a>\s*\(\d+ / (\d+)\)', t, re.S)]
    q['task_items'] = []
    for line in re.split(r'<br\s*/?>', t):
        im = re.search(r'href="database/items/(\d+)/"[^>]*>(.*?)</a>', line, re.S)
        cm = re.search(r'\((\d+) / (\d+)\)', line)
        if im and cm:
            q['task_items'].append({'item_id': int(im.group(1)), 'name': text(im.group(2)), 'count': int(cm.group(2)), 'verb': text(line).split(' ', 1)[0]})
    req = re.search(r'<h3>Required Quests</h3><div>(.*?)</div></div>', body, re.S)
    q['required_quests'] = []
    if req:
        for kind, i, n, l in re.findall(r'questinfo_1_(\w+)\.png"[^>]*/>\s*<a href="database/quests/(\d+)/">(.*?)</a>\s*\(Level (\d+)\)', req.group(1), re.S):
            q['required_quests'].append({'quest_id': int(i), 'name': text(n), 'level': int(l), 'kind': kind})
    q['dialogs'] = {}
    for key in ('Start', 'Progress', 'End'):
        d = re.search(r'<h3>%s</h3></div><div>(.*?)</div></div></div>' % key, body, re.S)
        if d:
            frag = d.group(1)
            lines = text(frag).split('\n')
            obj = next((l.split(':', 1)[1].strip() for l in lines if l.startswith('Objective:')), None)
            story = '\n'.join(l for l in lines if not l.startswith('Objective:'))
            q['dialogs'][key.lower()] = {'text': story.strip(), 'objective': obj}
    get_all = re.search(r'<h3>Reward \(Get All\)</h3>(.*?)(?=<h2>|<h3>|$)', body, re.S)
    q['reward_all'] = items_in(get_all.group(1)) if get_all else []
    select = re.search(r'<h2>Reward \(Select One\)</h2>(.*?)(?=<h2>|<h3>|$)', body, re.S)
    q['reward_choice'] = items_in(select.group(1)) if select else []
    return q


def parse_recipe(body):
    m = re.search(r'&bull; ([^<&]+)</div>', body)
    name = text(m.group(1)) if m else None
    res = re.search(r'<h2>Item</h2></div><div><a href="database/items/(\d+)/">.*?>([^<]+)</a>', body, re.S)
    mats = re.search(r'<h2>Materials</h2>(.*)', body, re.S)
    out = {'name': name, 'result': {'item_id': int(res.group(1)), 'name': text(res.group(2))} if res else None, 'materials': []}
    if mats:
        for kind, iid, cnt, nm in re.findall(r'<a href="database/items/(recipes/)?(\d+)/"><div><div><img[^>]*/> <span[^>]*>(\d+)</span></div></div></a>\s*<a [^>]*>([^<]+)</a>', mats.group(1), re.S):
            out['materials'].append({'item_id': int(iid), 'count': int(cnt), 'name': text(nm), 'is_recipe': bool(kind)})
    return out if name else None


def parse_set(body):
    m = re.search(r'<h1>(.*?)</h1>', body, re.S)
    if not m:
        return None
    items = [{'item_id': int(i), 'name': text(n)} for i, n in re.findall(r'<a href="database/items/(\d+)/"[^>]*class="tooltip_trigger2"[^>]*>([^<]+)</a>', body)]
    seen, uniq = set(), []
    for it in items:
        if it['item_id'] not in seen:
            seen.add(it['item_id']); uniq.append(it)
    effects = {}
    for k, frag in re.findall(r'<h2>Effect \((\d+)\)</h2></div><div>(.*?)</div>', body, re.S):
        effects[k] = text(frag)
    return {'name': text(m.group(1)), 'items': uniq, 'effects': effects}


PARSERS = {'quests': parse_quest, 'recipes': parse_recipe, 'sets': parse_set}
for kind, fn in PARSERS.items():
    data, problems = {}, []
    for mid, cap in sorted(manifest.items()):
        if not mid.startswith(kind + '/') or mid.endswith('_lista') or cap.get('status') != 'ok':
            continue
        r = fn(load(mid, cap))
        if not r:
            problems.append(mid)
            continue
        r.update({'tosbase_id': mid.split('/')[1], 'captured_at': cap['timestamp'], 'archive_url': cap['archive_url'], 'html_sha256': cap['sha256']})
        data[mid.split('/')[1]] = r
    out = {'source': 'ToSBase, capturas do Internet Archive (março a junho de 2016)', 'count': len(data),
           'note': 'Valores exibidos pelo banco naquela janela. Gerado por scripts/parse-tosbase-world.py.', kind: data}
    (ROOT / f'data/research/tosbase-2016-{kind}.json').write_text(json.dumps(out, ensure_ascii=False, separators=(',', ':')) + '\n', encoding='utf-8')
    print(f'{kind}: {len(data)} lidos; {len(problems)} sem leitura {problems[:6]}')
