#!/usr/bin/env python3
"""Ajusta receitas e coleções do Melia ao que o ToSBase mostrava em 2016.

Uso: python3 scripts/build-crafting-pack-2016.py PASTA_DO_PACOTE PASTA_DO_MELIA
Lê data/research/tosbase-2016-recipes.json e tosbase-2016-collections.json e o system/db do Melia, e escreve
  user/db/recipes.txt      (só as receitas cujos materiais de 2016 diferem dos do Melia)
  user/db/collections.txt  (itens exigidos e bônus de 2016 das coleções que existem nos dois)
  crafting-report.json     (o que mudou e o que ficou de fora)

Receitas: o Melia traz a receita do jogo atual; o ToSBase mostra os materiais de 2016. A casa da receita (o item
"R_...") continua como primeiro ingrediente. Receitas com mais de uma linha no Melia para o mesmo produto, ou
com material que o Melia não conhece, não são trocadas.
Coleções: as exigências vêm da lista do ToSBase (sem o item da própria coleção) e o bônus é lido do texto do
ToSBase ("STR +1", "HP Recovery +5"), com o nome de propriedade que o Melia já usa para esse bônus.
"""
import collections, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
pack, melia = Path(sys.argv[1]), Path(sys.argv[2])
research = lambda n: json.loads((ROOT / 'data/research' / n).read_text(encoding='utf-8'))

items_txt = (melia / 'system/db/items.txt').read_text(encoding='utf-8-sig')
class_by_id = {int(i): c for i, c in re.findall(r'itemId:\s*(\d+),\s*className:\s*"([^"]*)"', items_txt)}
id_by_class = {c: i for i, c in class_by_id.items()}


def split_db(path):
    header, entries = [], {}
    for line in path.read_text(encoding='utf-8-sig').split('\n'):
        m = re.match(r'^\{ (?:recipeId|classId): (\d+),', line)
        if m:
            entries[int(m.group(1))] = line
        elif not entries and not line.startswith('['):
            header.append(line)
    return [h for h in header if (h.startswith('//') and h.strip() not in ('// Melia', '// Database file')) or not h.strip()], entries


def write_db(path, header, lines):
    path.parent.mkdir(parents=True, exist_ok=True)
    head = header
    path.write_text('\n'.join(['// Melia', '// Database file (pacote original 2016: sobrescreve entradas do system)'] + head + ['[']
                              + [lines[i] for i in sorted(lines)] + [']']) + '\n', encoding='utf-8')


# ---------------------------------------------------------------- receitas
r_header, r_lines = split_db(melia / 'system/db/recipes.txt')
by_product = collections.defaultdict(list)
for rid, line in r_lines.items():
    m = re.search(r'productClassName: "([^"]*)"', line)
    by_product[id_by_class.get(m.group(1))].append(rid)

new_recipes, rec = {}, collections.Counter()
for key, v in sorted(research('tosbase-2016-recipes.json')['recipes'].items(), key=lambda kv: int(kv[0])):
    if not v['result']:
        rec['sem produto no ToSBase'] += 1
        continue
    candidates = by_product.get(v['result']['item_id'], [])
    if not candidates:
        rec['produto sem receita no Melia'] += 1
        continue
    if len(candidates) > 1:
        rec['mais de uma receita no Melia (ambíguo)'] += 1
        continue
    rid = candidates[0]
    line = r_lines[rid]
    ing = re.findall(r'\{ className: "([^"]*)", amount: (\d+) \}', line)
    wanted = [(m['item_id'], m['count']) for m in v['materials'] if not m['is_recipe']]
    house = [(c, int(a)) for c, a in ing if c.startswith('R_')]
    current = {id_by_class.get(c): int(a) for c, a in ing if not c.startswith('R_')}
    if current == dict(wanted):
        rec['igual ao Melia'] += 1
        continue
    if any(i not in class_by_id for i, _ in wanted):
        rec['material de 2016 que o Melia não conhece'] += 1
        continue
    new_ing = ', '.join('{ className: "%s", amount: %d }' % (c, a) for c, a in house + [(class_by_id[i], n) for i, n in wanted])
    new_recipes[rid] = re.sub(r'ingredients: \[.*\]', 'ingredients: [' + new_ing + ']', line, count=1)
    rec['trocada'] += 1
write_db(pack / 'user/db/recipes.txt', r_header, new_recipes)

# ---------------------------------------------------------------- coleções
c_header, c_lines = split_db(melia / 'system/db/collections.txt')
PROPS = {'STR': 'STR_BM', 'INT': 'INT_BM', 'CON': 'CON_BM', 'SPR': 'MNA_BM', 'DEX': 'DEX_BM', 'Crit Attack': 'CRTATK_BM', 'MDEF': 'MDEF_BM',
         'DEF': 'DEF_BM', 'Dark Resistance': 'ResDark_BM', 'HP': 'MHP_BM', 'PATK': 'PATK_BM', 'MATK': 'MATK_BM', 'SP': 'MSP_BM',
         'HP Recovery': 'RHP_BM', 'SP Recovery': 'RSP_BM', 'M. Amplification': 'CRTMATK_BM', 'Accuracy': 'HR_BM', 'Evasion': 'DR_CONST',
         'Crit Rate': 'CRTHR_BM', 'Crit Defense': 'CRTDR_BM', 'Stamina': 'MaxSta_BM', 'Max Weight': 'MaxWeight_BM'}
new_cols, col = {}, collections.Counter()
unmapped = collections.Counter()
for key, v in sorted(research('tosbase-2016-collections.json')['entries'].items(), key=lambda kv: int(kv[0])):
    line = c_lines.get(int(key))
    if not line:
        col['coleção só existe no ToSBase'] += 1
        continue
    required = [i for i in v['items'] if not class_by_id.get(i, '').startswith('COLLECT_')]  # tira o item da própria coleção
    if any(i not in class_by_id for i in required):
        col['item de 2016 que o Melia não conhece'] += 1
        continue
    props, bad = [], False
    for bonus_line in v['bonus'].split('\n'):
        m = re.match(r'^(.*?)\s*\+?(-?\d+(?:\.\d+)?)$', bonus_line.strip())
        if not m or m.group(1) not in PROPS:
            unmapped[bonus_line.strip()] += 1
            bad = True
            continue
        props.append((PROPS[m.group(1)], m.group(2)))
    if bad or not props or not required:
        col['bônus sem propriedade equivalente no Melia'] += 1
        continue
    req_txt = ', '.join('"%s": 1' % class_by_id[i] for i in required)
    rew_txt = ', '.join('"%s": %s' % p for p in props)
    new_line = re.sub(r'requiredItems: \{.*?\}, rewardProperties: \{.*?\}', lambda _: 'requiredItems: {%s}, rewardProperties: {%s}' % (req_txt, rew_txt), line, count=1)
    if new_line == line:
        col['igual ao Melia'] += 1
        continue
    new_cols[int(key)] = new_line
    col['trocada'] += 1
write_db(pack / 'user/db/collections.txt', c_header, new_cols)

report = {'receitas_no_tosbase': sum(rec.values()), 'receitas': dict(rec), 'receitas_trocadas_ids': sorted(new_recipes),
          'colecoes_no_tosbase': sum(col.values()), 'colecoes': dict(col), 'colecoes_trocadas_ids': sorted(new_cols),
          'bonus_sem_equivalente': dict(unmapped), 'colecoes_do_melia_que_o_tosbase_nao_tem': len(set(c_lines) - set(map(int, research('tosbase-2016-collections.json')['entries'])))}
(pack / 'crafting-report.json').write_text(json.dumps(report, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')
print('receitas:', dict(rec))
print('coleções:', dict(col), '| bônus sem equivalente:', dict(unmapped))
