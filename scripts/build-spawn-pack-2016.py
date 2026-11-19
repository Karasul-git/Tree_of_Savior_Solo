#!/usr/bin/env python3
"""Ajusta os spawns do Melia ao que o ToSBase mostrava em 2016 e mede o que ainda falta.

Uso: python3 scripts/build-spawn-pack-2016.py PASTA_DO_PACOTE PASTA_DO_MELIA
Lê data/research/tosbase-2016-maps.json (população e tempo de nascimento por mapa) e os scripts de
spawn do Melia (system/scripts/zone/**/mobs/**). Para cada arquivo que precisa de ajuste, escreve uma
cópia em user/scripts/zone/<mesmo caminho> (o Melia usa o arquivo de user/ no lugar do de system/).
Escreve também spawns-report.json.

O que muda (só o que o ToSBase prova):
  1. Spawner sem tempo de nascimento explícito, com monstro e população iguais aos do ToSBase:
     ganha `respawn: Seconds(n)` do ToSBase (o Melia usaria 30 a 60 s). Se o ToSBase tem
     tempos diferentes para a mesma população, nada muda (ambíguo).
  3. AddPropertyOverrides dos mapas de 2016: comentados. São atributos do jogo atual (vida e ataque
     dezenas de vezes maiores) que, na prática, anulariam os monstros de 2016 do pacote nesses mapas.
  2. Spawner dos "Tree Root Crystal" (Rootcrystal_*) em mapa cujo ToSBase não os lista: comentado
     (o ToSBase de 2016 não os lista nesses mapas).
O que não muda, só entra no relatório: monstros do ToSBase sem spawner no Melia, monstros do Melia
que o ToSBase não lista, mapas que o Melia deixou fechados e a população/tempo que já divergem.
"""
import collections, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
pack, melia = Path(sys.argv[1]), Path(sys.argv[2])
zone = melia / 'system/scripts/zone'

maps_txt = (melia / 'system/db/maps.txt').read_text(encoding='utf-8-sig')
map_class = {int(i): c for i, c in re.findall(r'mapId:\s*(\d+),\s*className:\s*"([^"]*)"', maps_txt)}
map_name = {int(i): n for i, n in re.findall(r'mapId:\s*(\d+),\s*className:\s*"[^"]*",\s*name:\s*"([^"]*)"', maps_txt)}
monster_const = {n: int(i) for n, i in re.findall(r'public const int (\w+) = (\d+);', (zone / 'const/MonsterId.cs').read_text(encoding='utf-8-sig'))}
monster_name = {int(i): n for i, n in re.findall(r'monsterId:\s*(\d+),\s*className:\s*"[^"]*",\s*name:\s*"([^"]*)"',
                                                   (melia / 'system/db/monsters.txt').read_text(encoding='utf-8-sig'))}
tosbase = json.loads((ROOT / 'data/research/tosbase-2016-maps.json').read_text(encoding='utf-8'))['entries']

SPAWNER = re.compile(r'^(?P<head>\s*AddSpawner\("(?P<ident>[^"]+)",\s*MonsterId\.(?P<mon>\w+),\s*'
                     r'(?:amount:\s*(?P<amount>\d+)|min:\s*(?P<min>\d+),\s*max:\s*(?P<max>\d+)))(?P<rest>.*)\);(?P<tail>\s*)$')
POINT = re.compile(r'AddSpawnPoint\(\s*"([^"]+)"\s*,\s*"(\w+)"')
RESPAWN = re.compile(r'respawn:\s*(Seconds|Minutes|Hours|Milliseconds)\((\d+)\)')


def seconds_of(rest):
    m = RESPAWN.search(rest)
    return None if not m else int(m.group(2)) * {'Seconds': 1, 'Minutes': 60, 'Hours': 3600, 'Milliseconds': 0.001}[m.group(1)]


# --- lê os scripts de spawn do Melia
files = sorted(p for p in (zone / 'content').rglob('*.cs') if '/mobs/' in p.as_posix() and 'AddSpawner(' in p.read_text(encoding='utf-8-sig'))
parsed = {}  # arquivo -> (linhas, spawners)
melia_spawns = collections.defaultdict(list)  # (mapa, id do monstro) -> [spawner]
for f in files:
    lines = f.read_text(encoding='utf-8-sig').split('\n')
    maps_of = {}
    for line in lines:
        for ident, mp in POINT.findall(line):
            maps_of.setdefault(ident, mp)
    spawners = []
    for n, line in enumerate(lines):
        m = SPAWNER.match(line)
        if not m or m['mon'] not in monster_const or m['ident'] not in maps_of:
            continue
        s = {'line': n, 'ident': m['ident'], 'mon': m['mon'], 'id': monster_const[m['mon']], 'map': maps_of[m['ident']],
             'pop': int(m['amount'] or m['max']), 'respawn': seconds_of(m['rest']), 'm': m}
        spawners.append(s)
        melia_spawns[(s['map'], s['id'])].append(s)
    parsed[f] = (lines, spawners)

# --- confronta com o ToSBase
changes = collections.Counter()
edits = collections.defaultdict(dict)  # arquivo -> {linha: novo texto}
per_map, closed = [], []
for key, entry in sorted(tosbase.items(), key=lambda kv: int(kv[0])):
    if not entry['monsters']:
        continue
    cls = map_class.get(int(key))
    here = {mid: [s for s in v] for (mp, mid), v in melia_spawns.items() if mp == cls}
    if not here:
        closed.append({'map_id': int(key), 'name': entry['name'], 'class': cls, 'melia_name': map_name.get(int(key)), 'level': entry['level'], 'monsters': len(entry['monsters'])})
        continue
    by_id = collections.defaultdict(list)
    for m in entry['monsters']:
        by_id[m['monster_id']].append((m['population'], m['respawn_seconds']))
    expected = set(by_id)
    present = {mid for mid in here}
    crystals = {mid for mid in here if monster_name.get(mid, '') == 'Tree Root Crystal'}
    for mid, spawners in here.items():
        for s in spawners:
            same = [rs for pop, rs in by_id.get(mid, []) if pop == s['pop']]
            if s['respawn'] is None and same and len(set(same)) == 1:
                rest = s['m']['rest']
                new_rest = rest + f', respawn: Seconds({same[0]})' if 'tendency:' not in rest else rest.replace(', tendency:', f', respawn: Seconds({same[0]}), tendency:', 1)
                text = s['m']['head'] + new_rest + ');' + s['m']['tail']
                edits[s['ident'] and next(f for f, (_, sp) in parsed.items() if s in sp)][s['line']] = text
                changes['respawn aplicado'] += 1
            elif s['respawn'] is None and same:
                changes['respawn ambíguo (nada mudou)'] += 1
            elif s['respawn'] is not None and same and s['respawn'] != same[0] and len(set(same)) == 1:
                changes['respawn do Melia difere do ToSBase (nada mudou)'] += 1
        if mid in crystals and mid not in expected:
            for s in spawners:
                f = next(f for f, (_, sp) in parsed.items() if s in sp)
                edits[f][s['line']] = '\t\t// removido (pacote 2016): o ToSBase de 2016 não lista cristal de raiz neste mapa\n\t\t// ' + parsed[f][0][s['line']].strip()
                changes['spawner de cristal de raiz removido'] += 1
    per_map.append({'map_id': int(key), 'name': entry['name'], 'class': cls, 'level': entry['level'],
                    'tosbase': len(expected), 'presentes': len(expected & present),
                    'faltam': sorted(monster_name.get(i, str(i)) for i in expected - present),
                    'a_mais': sorted(monster_name.get(i, str(i)) for i in present - expected - crystals)})

# --- 3. sobrescritas de atributos por mapa (valores do jogo atual: vida e ataque muito maiores que os de 2016)
maps_2016 = {m['class'] for m in per_map}
OVERRIDE = re.compile(r'^\s*AddPropertyOverrides\("(\w+)"')
for f, (lines, _) in parsed.items():
    for n, line in enumerate(lines):
        m = OVERRIDE.match(line)
        if m and m.group(1) in maps_2016:
            edits[f][n] = '\t\t// removido (pacote 2016): atributos do jogo atual; valem os do banco de monstros de 2016\n\t\t// ' + line.strip()
            changes['sobrescrita de atributos por mapa removida'] += 1

# --- escreve as cópias alteradas
for f, ed in edits.items():
    lines = list(parsed[f][0])
    for n, text in ed.items():
        lines[n] = text
    target = pack / 'user/scripts/zone' / f.relative_to(zone)
    target.parent.mkdir(parents=True, exist_ok=True)
    target.write_text('﻿' + '\n'.join(lines), encoding='utf-8')

total = sum(m['tosbase'] for m in per_map)
present = sum(m['presentes'] for m in per_map)
report = {'mapas_do_tosbase_com_monstros': len(per_map) + len(closed), 'mapas_com_spawn_no_melia': len(per_map),
          'mapas_fechados_ou_sem_spawn': closed, 'monstros_do_tosbase_nesses_mapas': total, 'monstros_com_spawner_no_melia': present,
          'alteracoes': dict(changes), 'arquivos_alterados': len(edits), 'mapas': per_map}
(pack / 'spawns-report.json').write_text(json.dumps(report, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')
print(f'{len(per_map)} mapas com spawn; {len(closed)} sem spawn no Melia; {present} de {total} monstros do ToSBase têm spawner.')
print(f'{len(edits)} arquivos alterados:', dict(changes))
