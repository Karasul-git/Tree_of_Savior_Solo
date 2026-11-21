#!/usr/bin/env python3
"""Gera o pacote "original 2016" para o servidor Melia.

Uso: python3 scripts/build-server-pack-2016.py CAMINHO/DO/MELIA SAIDA
  CAMINHO/DO/MELIA: pasta do Melia (com system/db e system/scripts)
  SAIDA: pasta onde escrever (normalmente server/original-2016)

A saída é uma pasta `user/` para copiar sobre a pasta `user/` do Melia. O Melia carrega
`system/` primeiro e `user/` por cima, então nada em `system/` é alterado.
Fontes dos dados: data/research/tosbase-2016-*.json (ToSBase, março a junho de 2016).
Gera também REPORT.md com o que foi trocado e o que ficou de fora.
"""
import json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
melia = Path(sys.argv[1])
out = Path(sys.argv[2])
user = out / 'user'


def read_json(name):
    return json.loads((ROOT / 'data/research' / name).read_text(encoding='utf-8'))


# ----------------------------------------------------------------- utilidades de texto
def split_db(path, id_key):
    """Lê um db do Melia: devolve (cabeçalho, {id: linha}); as linhas de entrada começam com `{ id_key:`."""
    entries, header = {}, []
    for line in path.read_text(encoding='utf-8-sig').split('\n'):
        m = re.match(r'^\{ %s: (\d+),' % id_key, line)
        if m:
            entries[int(m.group(1))] = line
        elif not entries:
            header.append(line)
    return header, entries


def num_fmt(v):
    if isinstance(v, float) and not v.is_integer():
        return f'{v:.4f}'.rstrip('0').rstrip('.')
    return str(int(v))


def set_field(line, key, value, before_script=True):
    """Troca `key: valor` na linha (só a primeira ocorrência, antes de `script:` quando houver)."""
    cut = line.find(', script:') if before_script else -1
    head, tail = (line[:cut], line[cut:]) if cut > 0 else (line, '')
    pattern = re.compile(r'(\b%s: )("[^"]*"|-?\d+(?:\.\d+)?|[A-Za-z_]\w*)' % re.escape(key))
    if not pattern.search(head):
        return line, False
    new = value if isinstance(value, str) and value.startswith('"') else num_fmt(value) if not isinstance(value, str) else value
    return pattern.sub(lambda m: m.group(1) + new, head, count=1) + tail, True


def replace_array(line, key, items_text):
    i = line.find(f'{key}: [')
    if i < 0:
        return line
    depth, j = 0, i + len(key) + 2
    start = j - 1
    for j in range(start, len(line)):
        if line[j] == '[':
            depth += 1
        elif line[j] == ']':
            depth -= 1
            if depth == 0:
                break
    return line[:start] + '[' + items_text + ']' + line[j + 1:]


def write_db(path, header, entries, lines_by_id):
    path.parent.mkdir(parents=True, exist_ok=True)
    body = [lines_by_id[i] for i in sorted(lines_by_id)]
    path.write_text('\n'.join(header_user(header) + body + [']']) + '\n', encoding='utf-8')


def header_user(header):
    keep = [h for h in header if h.startswith('//') or not h.strip()]
    return ['// Melia', '// Database file (pacote original 2016: sobrescreve entradas do system)', '//'] + [h for h in keep if 'Values:' in h or 'Drop Values' in h] + ['//---------------------------------------------------------------------------', '', '[']


report = {}

# ----------------------------------------------------------------- monstros
RACE = {'Insect': 'Klaida', 'Mutant': 'Paramune', 'Plant': 'Forester', 'Demon': 'Velnias', 'Beast': 'Widling'}
ELEMENT = {'Fire': 'Fire', 'Ice': 'Ice', 'Lightning': 'Lightning', 'Earth': 'Earth', 'Poison': 'Poison', 'Holy': 'Holy', 'Dark': 'Dark', 'Melee': 'Melee'}
ARMOR = {'Cloth': 'Cloth', 'Leather': 'Leather', 'Plate': 'Iron', 'Iron': 'Iron', 'Ghost': 'Ghost'}
RANK = {'Normal': 'Normal', 'Elite': 'Elite', 'Boss': 'Boss', 'Material': 'Material', 'NPC': 'NPC'}

m_header, m_lines = split_db(melia / 'system/db/monsters.txt', 'monsterId')
_, item_lines = split_db(melia / 'system/db/items.txt', 'itemId')
monsters = read_json('tosbase-2016-monsters.json')['monsters']
new_monsters, unmatched = {}, []
drop_unknown = drop_kept = drop_missing = 0
for mid, mon in monsters.items():
    mid_i = int(mid)
    line = m_lines.get(mid_i)
    if not line:
        unmatched.append(mid_i)
        continue
    c = mon['combat']
    atk = c.get('ATK') or [0, 0]
    matk = c.get('MATK') or [0, 0]
    pairs = [('level', mon['level']), ('exp', mon.get('base_exp') or 0), ('jobExp', mon.get('class_exp') or 0), ('hp', mon['hp']), ('sp', c.get('SP', 0)),
             ('pAttackMin', atk[0]), ('pAttackMax', atk[-1]), ('mAttackMin', matk[0]), ('mAttackMax', matk[-1]),
             ('pDefense', c.get('DEF', 0)), ('mDefense', c.get('MDEF', 0)), ('hitRate', c.get('Accuracy', 0)), ('dodgeRate', c.get('Evasion', 0)),
             ('blockRate', 0), ('blockBreakRate', 0), ('critHitRate', c.get('Crit Rate', 0)), ('critDodgeRate', c.get('Crit Resistance', 0)), ('critAttack', c.get('Crit Damage', 0))]
    for key, val in pairs:
        line, _ = set_field(line, key, val)
    for key, mapping, src in (('race', RACE, mon['race']), ('element', ELEMENT, mon['element']), ('armor', ARMOR, mon['armor']), ('rank', RANK, mon['type'])):
        if src in mapping:
            line, _ = set_field(line, key, f'"{mapping[src]}"')
    if mon.get('size') in ('S', 'M', 'L', 'XL'):
        line, _ = set_field(line, 'size', f'"{mon["size"]}"')
    # drops de 2016: itens que existem no Melia; chance desconhecida usa a do Melia ou 1%
    old_drops = {int(i): float(ch) for i, ch in re.findall(r'itemId: (\d+), chance: ([\d.]+)', line[line.find('drops:'):])} if 'drops:' in line else {}
    silver = re.findall(r'\{ itemId: 900011[^}]*\}', line[line.find('drops:'):]) if 'drops:' in line else []
    items = list(silver)
    for d in mon['drops']:
        iid = d['item_id']
        if iid not in item_lines:
            drop_missing += 1
            continue
        if d['chance_pct'] is None:
            chance = old_drops.get(iid, 1.0)
            drop_unknown += 1
        else:
            chance = d['chance_pct']
            drop_kept += 1
        items.append('{ itemId: %d, chance: %s }' % (iid, num_fmt(chance)))
    if 'drops:' in line:
        line = replace_array(line, 'drops', ','.join(items))
    elif items:
        line = re.sub(r'\s*\},?\s*$', ', drops: [' + ','.join(items) + '] },', line)
    new_monsters[mid_i] = line
write_db(user / 'db/monsters.txt', m_header, m_lines, new_monsters)
report['monsters'] = {'tosbase': len(monsters), 'trocados': len(new_monsters), 'sem_equivalente_no_melia': len(unmatched),
                      'drops_com_chance_de_2016': drop_kept, 'drops_com_chance_desconhecida': drop_unknown, 'drops_de_itens_ausentes_no_melia': drop_missing}

# ----------------------------------------------------------------- itens
i_header, i_lines = split_db(melia / 'system/db/items.txt', 'itemId')
items2016 = read_json('tosbase-2016-items.json')['items']
new_items, items_unmatched, items_changed = {}, 0, 0
for iid, it in items2016.items():
    if not iid.isdigit():
        continue
    line = i_lines.get(int(iid))
    if not line:
        items_unmatched += 1
        continue
    st, original = it.get('stats', {}), line
    pa = re.match(r'^\s*(\d+)\s*~\s*(\d+)\s*$', st.get('Physical Attack', ''))
    if pa:
        line, _ = set_field(line, 'minAtk', int(pa.group(1)))
        line, _ = set_field(line, 'maxAtk', int(pa.group(2)))
    for label, key in (('Magic Attack', 'mAtk'), ('Physical Defense', 'def'), ('Magic Defense', 'mDef')):
        v = re.match(r'^\s*(\d+)\s*$', st.get(label, ''))
        if v:
            line, _ = set_field(line, key, int(v.group(1)))
    if it.get('required_level') is not None:
        line, _ = set_field(line, 'minLevel', it['required_level'])
    if it.get('weight') is not None:
        line, _ = set_field(line, 'weight', int(it['weight']))
    # grau (estrelas), potencial e durabilidade máximos de 2016. O Melia ainda não lê esses campos (ignora chaves
    # desconhecidas); ficam no banco para o pacote e para o patch futuro. Formato do ToSBase: "atual / máximo".
    extra = []
    if it.get('grade') is not None:
        extra.append(f"grade: {int(it['grade'])}")
    for key, field in (('potential', 'potential'), ('durability', 'durability')):
        m2 = re.match(r'^\s*(\d+)\s*/\s*(\d+)\s*$', it.get(key) or '')
        if m2:
            extra.append(f'{field}: {int(m2.group(2))}')
    if extra and re.search(r'\},?\s*$', line):
        line = re.sub(r'\s*\},?\s*$', ', ' + ', '.join(extra) + ' },', line)
    if line != original:
        new_items[int(iid)] = line
        items_changed += 1
write_db(user / 'db/items.txt', i_header, i_lines, new_items)
report['items'] = {'tosbase': len(items2016), 'trocados': items_changed, 'sem_equivalente_no_melia': items_unmatched}

# ----------------------------------------------------------------- habilidades
s_header, s_lines = split_db(melia / 'system/db/skills.txt', 'skillId')
pages = read_json('tosbase-2016-skills.json')['skills']
sim = read_json('tosbase-2016-skillsim.json')['skills']
new_skills, sp_table, skill_sources = {}, {}, {'paginas': 0, 'simulador': 0}
no_melia = 0
for sid in sorted(set(pages) | set(sim), key=int):
    src = pages.get(sid) or sim.get(sid)
    line = s_lines.get(int(sid))
    if not line:
        no_melia += 1
        continue
    skill_sources['paginas' if sid in pages else 'simulador'] += 1
    levels = src['levels']
    if not levels:
        continue
    original = line
    attack = [float(l['effects']['Attack'].replace(',', '')) for l in levels if 'Attack' in l['effects'] and re.fullmatch(r'-?[\d.,]+', l['effects']['Attack'])]
    if len(attack) == len(levels) and attack:
        line, _ = set_field(line, 'factor', 100)
        line, _ = set_field(line, 'factorByLevel', 0)
        line, _ = set_field(line, 'atkAdd', attack[0])
        line, _ = set_field(line, 'atkAddByLevel', round((attack[-1] - attack[0]) / max(1, len(levels) - 1), 2))
    sps = [l['sp'] for l in levels if l['sp'] is not None]
    if sps and any(sps):
        sp_table[int(sid)] = sps
        line, _ = set_field(line, 'basicSp', sps[0])
    cd = levels[0]['cooldown']
    if cd is not None:
        line, ok = set_field(line, 'cooldownTime', int(cd * 1000))
        if not ok and cd > 0:
            line = re.sub(r'\s*\},?\s*$', f', cooldownTime: {int(cd * 1000)} }},', line)
    if line != original:
        new_skills[int(sid)] = line
write_db(user / 'db/skills.txt', s_header, s_lines, new_skills)
report['skills'] = {'trocadas': len(new_skills), 'com_tabela_de_sp': len(sp_table), 'fontes': skill_sources, 'sem_equivalente_no_melia': no_melia}

# ----------------------------------------------------------------- classes base
j_header, j_lines = split_db(melia / 'system/db/jobs.txt', 'jobId')
BASES = {1001: dict(str=7, con=8, int=2, spr=3, dex=5, hpRate=3.3, spRate=0.8), 2001: dict(str=3, con=3, int=8, spr=7, dex=4, hpRate=1.1, spRate=1.2),
         3001: dict(str=6, con=4, int=3, spr=4, dex=8, hpRate=1.4, spRate=0.9), 4001: dict(str=5, con=6, int=4, spr=7, dex=3, hpRate=1.5, spRate=1.2)}
new_jobs = {}
for jid, vals in BASES.items():
    line = j_lines[jid]
    for k, v in vals.items():
        line, ok = set_field(line, k, v)
        assert ok, (jid, k)
    new_jobs[jid] = line
write_db(user / 'db/jobs.txt', j_header, j_lines, new_jobs)
report['jobs'] = {'classes_base': len(new_jobs)}

# ----------------------------------------------------------------- features e conf
(user / 'db').mkdir(parents=True, exist_ok=True)
(user / 'db/features.txt').write_text('''// Melia
// Database file (pacote original 2016)
//
// Mantém só as eras Base, iCBT2 e Release (primavera de 2016): desliga o Re:Build e tudo o que veio depois.
// A ordem importa: desligar um pai desliga os filhos.
//---------------------------------------------------------------------------

[
{ name: "ReBuild", enabled: false },
{ name: "Latest", enabled: false },
{ name: "Misc", enabled: false },
{ name: "SpawnPropertyOverrides", enabled: true },
]
''', encoding='utf-8')
(user / 'conf/world').mkdir(parents=True, exist_ok=True)
(user / 'conf/world/jobs.conf').write_text('// Melia\n// Pacote original 2016\n\n// Em junho de 2016 o ToSBase listava classes até o rank 6.\njob_max_rank: 6\n', encoding='utf-8')
(user / 'conf/world/exp.conf').write_text('// Melia\n// Pacote original 2016\n\n// Taxas originais (a tabela de experiência base já é a de 2016).\nexp_rate : 100\njob_exp_rate : 100\n', encoding='utf-8')

# ----------------------------------------------------------------- scripts: fórmulas de 2016
core = user / 'scripts/zone/core'
core.mkdir(parents=True, exist_ok=True)


def find_function(src, name):
    """Devolve (início, fim) do método `name`, incluindo o atributo [ScriptableFunction] logo acima."""
    m = re.search(r'\n(\t\[ScriptableFunction[^\n]*\]\n)?\tpublic float %s\(' % re.escape(name), src)
    assert m, f'função {name} não encontrada'
    start = m.start() + 1
    brace = src.index('{', m.end())
    depth, i = 0, brace
    while True:
        if src[i] == '{':
            depth += 1
        elif src[i] == '}':
            depth -= 1
            if depth == 0:
                return start, i + 1
        i += 1


def patch(src, func, replacements):
    a, b = find_function(src, func)
    body = src[a:b]
    for old, new in replacements:
        assert old in body, f'{func}: trecho não encontrado: {old!r}'
        body = body.replace(old, new, 1)
    return src[:a] + body + src[b:]


def replace_function(src, func, new_text):
    a, b = find_function(src, func)
    return src[:a] + new_text.strip('\n') + src[b:]


calc = (melia / 'system/scripts/zone/core/calc_character.cs').read_text(encoding='utf-8-sig')

HELPERS = '''
	// ---- Pacote original 2016: fórmulas da calculadora do ToSBase (captura de 15/03/2016, F142) ----

	/// <summary>
	/// Bônus por investimento: sobre a soma do valor da classe base com os pontos gastos.
	/// </summary>
	private static float StatInvestmentBonus2016(float value)
	{
		float bonus;
		if (value < 51)
			bonus = value / 5f;
		else if (value < 151)
			bonus = 10f + (value - 50f) / 4f;
		else if (value < 301)
			bonus = 35f + (value - 150f) / 3f;
		else if (value < 501)
			bonus = 85f + (value - 300f) / 2f;
		else
			bonus = 185f + (value - 500f);

		return (float)Math.Floor(bonus);
	}

	/// <summary>
	/// Rank do personagem: o número de classes que ele já escolheu.
	/// </summary>
	private static int Rank2016(Character character)
		=> Math.Max(1, character.Jobs.GetList().Length);

	private static float Stat2016(Character character, string job, string stat, string bonus, string add, bool rankBonus)
	{
		var properties = character.Properties;

		var value = properties.GetFloat(job) + properties.GetFloat(stat);
		value += StatInvestmentBonus2016(value);

		// STR e INT ganham 10% do valor por rank acima do primeiro.
		if (rankBonus)
			value += (float)Math.Floor(value * (Rank2016(character) - 1) * 0.1f);

		value += properties.GetFloat(bonus) + properties.GetFloat(add);
		return (float)Math.Floor(Math.Max(1, value));
	}

	private static float Base2016(Character character, int baseValue)
		=> baseValue;
'''

for stat, prop, base_attr, rank in (('STR', 'STR', 'Str', True), ('CON', 'CON', 'Con', False), ('INT', 'INT', 'Int', True), ('MNA', 'MNA', 'Spr', False), ('DEX', 'DEX', 'Dex', False)):
    calc = replace_function(calc, f'SCR_Get_Character_{stat}_JOB', f'''
	[ScriptableFunction]
	public float SCR_Get_Character_{stat}_JOB(Character character)
	{{
		// 2016: valor fixo da classe base, sem crescimento automático por nível.
		return character.Job?.Data.{base_attr} ?? 1;
	}}
''')
    calc = replace_function(calc, f'SCR_Get_Character_{stat}', f'''
	[ScriptableFunction]
	public float SCR_Get_Character_{stat}(Character character)
		=> Stat2016(character, PropertyName.{stat}_JOB, PropertyName.{stat}_STAT, PropertyName.{stat}_Bonus, PropertyName.{stat}_ADD, {str(rank).lower()});
''')

calc = patch(calc, 'SCR_Get_Character_MHP', [
    ('var byJob = Math.Floor(400 * rateByJob);\n', ''),
    ('var byLevel = Math.Floor(byJob + ((level - 1) * 80 * rateByJob));', 'var byLevel = Math.Floor(rateByJob * Math.Floor((level - 1) * 17));'),
    ('var byStat = Math.Floor(((stat * 0.003f) + (Math.Floor(stat / 10.0f) * 0.01f)) * byLevel);', 'var byStat = Math.Floor(stat * 85);'),
])
calc = patch(calc, 'SCR_Get_Character_MSP', [
    ('var byJob = Math.Floor(200 * rateByJob);\n', ''),
    ('var byLevel = Math.Floor(byJob + ((level - 1) * 18 * rateByJob));', 'var byLevel = Math.Floor(rateByJob * Math.Floor((level - 1) * 6.7) + Math.Floor(stat * 13) + (character.JobClass == JobClass.Cleric ? level * 1.675 : 0));'),
    ('var byStat = Math.Floor(((stat * 0.005f) + (Math.Floor(stat / 10.0f) * 0.015f)) * byLevel);', 'var byStat = 0d;'),
])
calc = patch(calc, 'SCR_Get_Character_RHP', [
    ('var jobHpRate = character.Job?.Data.RHpRate ?? 1;', 'var jobHpRate = character.Job?.Data.HpRate ?? 1;\n\t\tvar level = properties.GetFloat(PropertyName.Lv, 1);\n\t\tvar con = properties.GetFloat(PropertyName.CON, 1);'),
    ('var byDefault = Math.Floor(mhp / 100f * jobHpRate);', 'var byDefault = Math.Floor(jobHpRate * level * 0.5f + con);'),
])
calc = patch(calc, 'SCR_Get_Character_RSP', [
    ('var jobSpRate = character.Job?.Data.RSpRate ?? 1;', 'var jobSpRate = character.Job?.Data.SpRate ?? 1;\n\t\tvar level = properties.GetFloat(PropertyName.Lv, 1);\n\t\tvar spr = properties.GetFloat(PropertyName.MNA, 1);'),
    ('var byDefault = Math.Floor(mhp * 0.03f * jobSpRate);', 'var byDefault = Math.Floor(jobSpRate * level * 0.5f + spr + (character.JobClass == JobClass.Cleric ? level / 4f : 0f));'),
])
calc = patch(calc, 'SCR_Get_Character_MaxSta', [('var byStat = Math.Floor(stat / 20f);', 'var byStat = 0d;')])
calc = patch(calc, 'SCR_Get_Character_MaxWeight', [('if (Feature.IsEnabled("StaticInventoryWeight"))', 'if (true)')])
for f in ('SCR_Get_Character_MINPATK', 'SCR_Get_Character_MAXPATK'):
    calc = patch(calc, f, [('var baseValue = 20;', 'var baseValue = 0;'), ('var byStat = (stat * 2f) + ((float)Math.Floor(stat / 10f) * (byLevel * 0.05f));', 'var byStat = stat;')])
for f in ('SCR_Get_Character_MINMATK', 'SCR_Get_Character_MAXMATK'):
    calc = patch(calc, f, [('var baseValue = 20;', 'var baseValue = 0;'), ('var byStat = (stat * 2f) + ((float)Math.Floor(stat / 10f) * (byLevel * 0.05f));', 'var byStat = stat;')])
for f in ('SCR_Get_Character_MINPATK_SUB', 'SCR_Get_Character_MAXPATK_SUB'):
    calc = patch(calc, f, [('var baseValue = 20;', 'var baseValue = 0;'), ('var byLevel = level / 2f;', 'var byLevel = level;'), ('var byStat = (stat * 2f) + ((float)Math.Floor(stat / 10f) * 5f);', 'var byStat = stat;')])
calc = patch(calc, 'SCR_Get_Character_DEF', [('var baseValue = 20;', 'var baseValue = 0;'), ('var byLevel = level;', 'var byLevel = (float)Math.Floor(Math.Floor(level / 2f) + (character.JobClass == JobClass.Swordsman ? level / 4f : 0f));')])
calc = patch(calc, 'SCR_Get_Character_MDEF', [('var baseValue = 20;', 'var baseValue = 0;'), ('var byLevel = level;', 'var byLevel = (float)Math.Floor(level * 0.5f + properties.GetFloat(PropertyName.MNA) / 5f + (character.JobClass == JobClass.Wizard ? level / 4f : 0f));')])
calc = patch(calc, 'SCR_Get_Character_CRTATK', [('var stat = properties.GetFloat(PropertyName.DEX);', 'var stat = properties.GetFloat(PropertyName.STR);'), ('var byStat = (stat * 4f) + ((float)Math.Floor(stat / 10f) * 10f);', 'var byStat = stat;')])
calc = patch(calc, 'SCR_Get_Character_CRTHR', [('var byLevel = level / 2f;', 'var byLevel = properties.GetFloat(PropertyName.DEX) + (character.JobClass == JobClass.Archer ? level / 5f : 0f);')])
calc = patch(calc, 'SCR_Get_Character_CRTDR', [('var byLevel = level / 2f;', 'var byLevel = properties.GetFloat(PropertyName.CON);')])
calc = patch(calc, 'SCR_Get_Character_HR', [('var stat = properties.GetFloat(PropertyName.STR);', 'var stat = properties.GetFloat(PropertyName.DEX);'), ('var byLevel = level / 4f;', 'var byLevel = level + (character.JobClass == JobClass.Archer ? (level + 4f) / 4f : 0f);'),
                                             ('var byStat = (stat / 2f) + ((float)Math.Floor(stat / 15f) * 3f);', 'var byStat = stat;')])
calc = patch(calc, 'SCR_Get_Character_DR', [('var byLevel = level / 4f;', 'var byLevel = level + (character.JobClass == JobClass.Archer ? level / 8f : 0f);'), ('var byStat = (stat / 2f) + ((float)Math.Floor(stat / 15f) * 3f);', 'var byStat = stat;')])
# bônus de refino dos itens equipados (Original2016Refine, em custom/original2016/refine.cs)
def refine_line(anchor_line, stat):
    return (anchor_line, anchor_line + f'\n\t\tbyItem += Original2016Refine.EquipBonus(character, RefineStat.{stat});')


calc = patch(calc, 'SCR_Get_Character_MINPATK', [refine_line('byItem += character.Inventory.GetEquipProperties(PropertyName.ADD_MINATK);', 'Atk')])
calc = patch(calc, 'SCR_Get_Character_MAXPATK', [refine_line('byItem += character.Inventory.GetEquipProperties(PropertyName.ADD_MAXATK);', 'Atk')])
calc = patch(calc, 'SCR_Get_Character_MINMATK', [refine_line('byItem += character.Inventory.GetEquipProperties(PropertyName.ADD_MINATK);', 'MAtk')])
calc = patch(calc, 'SCR_Get_Character_MAXMATK', [refine_line('byItem += character.Inventory.GetEquipProperties(PropertyName.ADD_MAXATK);', 'MAtk')])
calc = patch(calc, 'SCR_Get_Character_DEF', [refine_line('var byItem = character.Inventory.GetEquipProperties(PropertyName.DEF);', 'Def')])
calc = patch(calc, 'SCR_Get_Character_MDEF', [refine_line('var byItem = character.Inventory.GetEquipProperties(PropertyName.MDEF);', 'MDef')])
calc = patch(calc, 'SCR_Get_Character_HR', [refine_line('byItem += character.Inventory.GetEquipProperties(PropertyName.ADD_HR);', 'Accuracy')])
calc = patch(calc, 'SCR_Get_Character_DR', [refine_line('byItem += character.Inventory.GetEquipProperties(PropertyName.ADD_DR);', 'Evasion')])
# bônus de conjunto de equipamento (Original2016Sets, em custom/original2016/sets.cs)
def set_line(anchor_line, stat, var='byItem'):
    return (anchor_line, anchor_line + f'\n\t\t{var} += Original2016Sets.EquipBonus(character, SetStat.{stat});')


for func, anchor_line, stat, var in [
    ('SCR_Get_Character_DEF', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.DEF);', 'Def', 'byItem'),
    ('SCR_Get_Character_MDEF', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.MDEF);', 'MDef', 'byItem'),
    ('SCR_Get_Character_MHP', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.MHP);', 'Mhp', 'byItem'),
    ('SCR_Get_Character_RHP', 'var byItems = character.Inventory.GetEquipProperties("RHP");', 'Rhp', 'byItems'),
    ('SCR_Get_Character_RSP', 'var byItems = character.Inventory.GetEquipProperties("RSP");', 'Rsp', 'byItems'),
    ('SCR_Get_Character_MaxSta', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.MSTA);', 'MaxSta', 'byItem'),
    ('SCR_Get_Character_STR_ADD', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.STR);', 'Str', 'byItem'),
    ('SCR_Get_Character_CON_ADD', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.CON);', 'Con', 'byItem'),
    ('SCR_Get_Character_INT_ADD', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.INT);', 'Int', 'byItem'),
    ('SCR_Get_Character_MNA_ADD', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.MNA);', 'Spr', 'byItem'),
    ('SCR_Get_Character_DEX_ADD', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.DEX);', 'Dex', 'byItem'),
    ('SCR_Get_Character_DR', 'byItem += character.Inventory.GetEquipProperties(PropertyName.DR);', 'Evasion', 'byItem'),
    ('SCR_Get_Character_CRTHR', 'byItem += character.Inventory.GetEquipProperties(PropertyName.CRTHR);', 'CritRate', 'byItem'),
    ('SCR_Get_Character_SR', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.SR);', 'AoeAttackRatio', 'byItem'),
    ('SCR_GET_RES_FIRE', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.RES_FIRE);', 'ResFire', 'byItem'),
    ('SCR_GET_RES_ICE', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.RES_ICE);', 'ResIce', 'byItem'),
    ('SCR_GET_RES_LIGHTNING', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.RES_LIGHTNING);', 'ResLightning', 'byItem'),
    ('SCR_GET_RES_EARTH', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.RES_EARTH);', 'ResEarth', 'byItem'),
    ('SCR_GET_RES_POISON', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.RES_POISON);', 'ResPoison', 'byItem'),
    ('SCR_GET_RES_HOLY', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.RES_HOLY);', 'ResHoly', 'byItem'),
    ('SCR_GET_RES_DARK', 'var byItem = character.Inventory.GetEquipProperties(PropertyName.RES_DARK);', 'ResDark', 'byItem'),
]:
    calc = patch(calc, func, [set_line(anchor_line, stat, var)])
# velocidade de movimento: não tem variável de itens, entra na soma
calc = patch(calc, 'SCR_Get_Character_MSPD', [('var value = (baseValue + byBuff + byBonus);', 'var value = (baseValue + byBuff + byBonus + Original2016Sets.EquipBonus(character, SetStat.MoveSpeed));')])
calc = patch(calc, 'SCR_Get_Character_BLK_BREAK', [('var stat = properties.GetFloat(PropertyName.DEX);', 'var stat = properties.GetFloat(PropertyName.MNA);'), ('var byLevel = level / 4f;', 'var byLevel = level * 0.5f;'),
                                                 ('var byStat = (stat / 2f) + ((float)Math.Floor(stat / 15f) * 3f);', 'var byStat = stat;')])
# helpers logo depois da abertura da classe
cls = re.search(r'public class CharacterCalculationsScript : GeneralScript\s*\{', calc)
assert cls
calc = calc[:cls.end()] + '\n' + HELPERS + calc[cls.end():]
(core / 'calc_character.cs').write_text('﻿' + calc, encoding='utf-8')

skill_src = (melia / 'system/scripts/zone/core/calc_skill.cs').read_text(encoding='utf-8-sig')
skill_src = patch(skill_src, 'SCR_Get_SpendSP', [
    ('var baseValue = skill.Data.BasicSp;\n\t\tif (baseValue == 0)\n\t\t\treturn 0;\n',
     'var baseValue = skill.Data.BasicSp;\n\t\tif (baseValue == 0)\n\t\t\treturn 0;\n\n\t\t// 2016: o custo vem de uma tabela por nível da habilidade, sem correção pelo nível do personagem.\n\t\tif (Original2016SkillTable.SpByLevel.TryGetValue((int)skill.Id, out var table) && table.Length > 0)\n\t\t\treturn (int)Math.Max(0, table[Math.Clamp(skill.Level, 1, table.Length) - 1]);\n'),
])
rows = ',\n'.join(f"\t\t{{ {k}, new float[] {{ {', '.join(num_fmt(float(x)) + 'f' for x in v)} }} }}" for k, v in sorted(sp_table.items()))
skill_src += f'''

/// <summary>
/// Custo de SP por nível de habilidade em 2016 (ToSBase). Gerado por scripts/build-server-pack-2016.py.
/// </summary>
public static class Original2016SkillTable
{{
	public static readonly System.Collections.Generic.Dictionary<int, float[]> SpByLevel = new()
	{{
{rows}
	}};
}}
'''
(core / 'calc_skill.cs').write_text('﻿' + skill_src, encoding='utf-8')
report['scripts'] = {'calc_character.cs': 'fórmulas de 2016', 'calc_skill.cs': f'tabela de SP de {len(sp_table)} habilidades'}
(out / 'report.json').write_text(json.dumps(report, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')
print(json.dumps(report, ensure_ascii=False, indent=1))
