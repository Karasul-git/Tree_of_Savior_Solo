#!/usr/bin/env python3
"""Lê as capturas de habilidades do ToSBase de 2016 e grava os dados estruturados.

Uso: python3 scripts/parse-tosbase-skills.py DIRETORIO_DO_BRANCH_DE_DADOS
  (a pasta que contém manifesto.json e raw/, vinda do branch data/tosbase-2016)
Grava data/research/tosbase-2016-skills.json. O HTML bruto não entra no main:
só os dados extraídos, com data, endereço e hash de cada captura.
"""
import gzip, html, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
src = Path(sys.argv[1])
manifest = json.loads((src / 'manifesto.json').read_text())['captures']


def text(fragment):
    fragment = re.sub(r'<br\s*/?>', '\n', fragment)
    fragment = re.sub(r'<[^>]+>', '', fragment)
    return html.unescape(fragment).replace('\xa0', ' ').strip()


def number(value):
    m = re.search(r'-?\d+(?:[.,]\d+)?', value or '')
    return float(m.group(0).replace(',', '.')) if m else None


def parse(raw):
    body = re.sub(r'<script.*?</script>|<style.*?</style>', '', raw, flags=re.S)
    out = {}
    head = re.search(r'<table class="db_table3".*?</table>', body, re.S)
    if not head:
        return None
    h = head.group(0)
    circle = re.search(r'Circle (\d+)', h)
    out['circle'] = int(circle.group(1)) if circle else None
    icon = re.search(r'content/img/icons/skills/([\w.-]+)', h)
    out['icon'] = icon.group(1) if icon else None
    rows = re.findall(r'<tr>(.*?)</tr>', h, re.S)
    cells = [re.findall(r'<t[dh][^>]*>(.*?)</t[dh]>', r, re.S) for r in rows]
    # linha 2: nome, elemento, nível máximo, recarga, SP
    if len(cells) > 1 and len(cells[1]) >= 5:
        out['name'] = text(cells[1][0])
        out['element'] = re.search(r'alt="([^"]*)"', cells[1][1]).group(1) if re.search(r'alt="([^"]*)"', cells[1][1]) else text(cells[1][1])
        out['max_level'] = int(number(cells[1][2]) or 0)
        out['cooldown_header'] = text(cells[1][3])
        out['sp_header'] = text(cells[1][4])
    # linha 4: classe, tipo, postura exigida
    if len(cells) > 3 and len(cells[3]) >= 3:
        out['class'] = text(cells[3][0])
        out['type'] = text(cells[3][1])
        out['required_stance'] = text(cells[3][2])
    # última linha: descrição
    desc = re.findall(r'<td colspan="6">(.*?)</td>', h, re.S)
    if desc:
        parts = text(desc[-1]).split('\n', 1)
        tags = re.findall(r'\[([^\]]+)\]', parts[0]) if parts else []
        out['damage_tags'] = tags
        out['description'] = (parts[1] if len(parts) > 1 else parts[0]).strip()
    # atributos (habilidades de aprimoramento)
    attrs = []
    for aid, tid in re.findall(r'href="database/attributes/(\d+)/"><img[^>]*data-tooltip="([^"]+)"', h):
        block = re.search(r'<div id="%s"[^>]*><b>(.*?)</b><div>(.*?)</div>(?:<div[^>]*>(.*?)</div>)?</div>' % re.escape(tid), body, re.S)
        name = text(block.group(1)) if block else None
        effect = text(block.group(2)) if block else None
        requirement = text(block.group(3)) if block and block.group(3) else None
        attrs.append({'id': int(aid), 'name': name, 'effect': effect, 'requirement': requirement})
    out['attributes'] = attrs
    # alvo
    det = re.search(r'<h2>Details</h2>.*?<tbody>(.*?)</tbody>', body, re.S)
    if det:
        cells2 = [text(c) for c in re.findall(r'<td[^>]*>(.*?)</td>', det.group(1), re.S)]
        keys = ['target', 'on_enemy', 'on_friend', 'on_neutral', 'companion']
        out['details'] = dict(zip(keys, cells2))
    # níveis
    levels = []
    lv = re.search(r'<h2>Skill Levels</h2>.*?<tbody>(.*?)</tbody>', body, re.S)
    if lv:
        for row in re.findall(r'<tr>(.*?)</tr>', lv.group(1), re.S):
            c = re.findall(r'<td[^>]*>(.*?)</td>', row, re.S)
            if len(c) < 5:
                continue
            effects = {}
            lines = [l.strip() for l in text(c[2]).split('\n') if l.strip()]
            for line in lines:
                k, _, v = line.partition(':')
                effects[k.strip()] = v.strip() if _ else ''
            levels.append({'level': int(number(text(c[1])) or 0), 'effects': effects,
                           'cooldown': number(text(c[3])), 'sp': number(text(c[4]))})
    out['levels'] = levels
    return out


skills = {}
problems = []
for mid, cap in sorted(manifest.items()):
    if not mid.startswith('skills/') or cap.get('status') != 'ok':
        continue
    raw = gzip.open(src / Path(cap['file']).relative_to('data/tosbase-2016')).read().decode('utf-8', 'replace')
    data = parse(raw)
    if not data or not data.get('name'):
        problems.append(f'{mid}: página sem tabela de habilidade')
        continue
    if data['max_level'] and len(data['levels']) != data['max_level']:
        problems.append(f"{mid} {data['name']}: {len(data['levels'])} níveis lidos, máximo {data['max_level']}")
    data.update({'tosbase_id': int(mid.split('/')[1]), 'captured_at': cap['timestamp'], 'archive_url': cap['archive_url'], 'html_sha256': cap['sha256']})
    skills[mid.split('/')[1]] = data

out = {'source': 'ToSBase, capturas do Internet Archive (março a junho de 2016)', 'skill_count': len(skills),
       'note': 'Valores exibidos pelo banco naquela janela; não são extração do cliente. Gerado por scripts/parse-tosbase-skills.py.',
       'skills': skills}
(ROOT / 'data/research/tosbase-2016-skills.json').write_text(json.dumps(out, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')
print(f'{len(skills)} habilidades lidas.')
for p in problems:
    print('-', p)
