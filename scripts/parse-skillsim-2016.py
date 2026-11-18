#!/usr/bin/env python3
"""Lê o simulador de habilidades do ToSBase (js/skillsim-0.0.2.js, captura de 16/06/2016).

O arquivo embute o banco inteiro de habilidades da época (classes, ranks, habilidades
com tabelas por nível e atributos). Uso: python3 scripts/parse-skillsim-2016.py DIRETORIO_DO_BRANCH_DE_DADOS
Grava data/research/tosbase-2016-skillsim.json no mesmo formato de tosbase-2016-skills.json.
"""
import gzip, html, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
src = Path(sys.argv[1])
manifest = json.loads((src / 'manifesto.json').read_text())['captures']
cap = next(v for k, v in manifest.items() if k.startswith('formulas/js_skillsim'))
js = gzip.open(src / Path(cap['file']).relative_to('data/tosbase-2016')).read().decode('utf-8', 'replace')


def var(name, nxt):
    i = js.index(f'var {name} = ')
    j = js.index(f'var {nxt} = ', i) if nxt else len(js)
    return json.loads(js[i + len(f'var {name} = '):j].strip().rstrip(';').strip())


classes = var('classList', 'classCircleList')
ranks = var('rankList', 'skillList')
skills = var('skillList', 'skillAbilityList')
ability_map = var('skillAbilityList', 'abilityList')
abilities = var('abilityList', None)
owner = {ident: cid for cid, c in classes.items() for ident in c['skills']}


def text(fragment):
    fragment = re.sub(r'<br\s*/?>', '\n', fragment)
    return html.unescape(re.sub(r'<[^>]+>', '', fragment)).replace('\xa0', ' ').strip()


def fill(template, level):
    """Troca os marcadores #{...}# pelos valores do nível e separa em 'rótulo: valor'."""
    out = {}
    for line in template.replace('<br/>', '\n').split('\n'):
        line = re.sub(r'<[^>]+>', '', line).strip()
        if not line:
            continue
        def repl(m):
            v = level.get(f'#{{{m.group(1)}}}#')
            return '' if v is None else str(v)
        value = re.sub(r'#\{(\w+)\}#', repl, line)
        k, sep, v = value.partition(':')
        if not sep:
            # Sem dois-pontos: "Duration 15 seconds" vira rótulo "Duration" e valor "15 seconds".
            m = re.match(r'^(.*?)\s*([-+]?\d[\d.,%]*(?:\s.*)?)$', value)
            k, v = (m.group(1), m.group(2)) if m and m.group(1) else (value, '')
        out[html.unescape(k.strip())] = html.unescape(v.strip())
    return out


result = {}
for ident, s in skills.items():
    desc = text(s.get('desc', ''))
    parts = desc.split('\n', 1)
    tags = re.findall(r'\[([^\]]+)\]', parts[0])
    levels = []
    for n in sorted(s['levelList'], key=int):
        lv = s['levelList'][n]
        levels.append({'level': int(n), 'effects': fill(s.get('desc2', ''), lv), 'cooldown': float(s.get('cooldown') or 0), 'sp': float(lv.get('sp', 0)),
                       'skill_factor': lv.get('#{SkillFactor}#')})
    result[s['id']] = {
        'tosbase_id': int(s['id']), 'ident': ident, 'name': s['name'], 'circle': int(s['circle']) if s.get('circle') else None,
        'rank_level': int(s['rankLevel']) if s.get('rankLevel') else None,
        'max_level': int(s['maxLevel']), 'element': s.get('element'), 'type': s.get('type2'), 'required_stance': s.get('reqStance') or 'None',
        'damage_tags': tags, 'description': (parts[1] if len(parts) > 1 else parts[0]).strip(),
        'class_id': owner.get(ident), 'class': classes.get(owner.get(ident), {}).get('name'),
        'ability_ids': ability_map.get(ident, []), 'levels': levels,
        'captured_at': cap['timestamp'], 'archive_url': cap['archive_url'], 'html_sha256': cap['sha256'],
    }

out = {'source': 'ToSBase, simulador de habilidades (js/skillsim-0.0.2.js), capturado em 16/06/2016',
       'note': 'Banco de habilidades embutido no simulador. Valores exibidos pelo site; não são extração do cliente. Gerado por scripts/parse-skillsim-2016.py.',
       'class_count': len(classes), 'skill_count': len(result),
       'classes': {cid: {'name': c['name'], 'base': c['base'], 'skills': c['skills']} for cid, c in classes.items()},
       'ranks': ranks, 'abilities': abilities, 'skills': result}
(ROOT / 'data/research/tosbase-2016-skillsim.json').write_text(json.dumps(out, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')
print(f'{len(classes)} classes, {len(result)} habilidades, {len(abilities)} atributos.')
