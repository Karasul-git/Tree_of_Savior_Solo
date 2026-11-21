#!/usr/bin/env python3
"""Gera o sistema de conjuntos de equipamento de 2016 como script do Melia.

Uso: python3 scripts/build-sets-pack-2016.py PASTA_DO_PACOTE PASTA_DO_MELIA   (normalmente server/original-2016)
Lê data/research/tosbase-2016-sets.json e os itens do Melia (system/db/items.txt) e escreve:
  user/scripts/zone/custom/original2016/sets.cs   (Original2016Sets: tabela de conjuntos e Bonus/EquipBonus)
  user/scripts/zone/scripts_custom.txt            (garante a linha do arquivo acima)
  sets-report.json                                (efeitos implementados e pendentes, por conjunto)

O Melia não tem conjuntos. O bônus é somado dentro das funções de cálculo de status (build-server-pack-2016.py
insere as chamadas em calc_character.cs); equipar ou tirar uma peça já recalcula tudo.
Só entram os efeitos que são um número somado a um atributo que o Melia calcula (defesa, HP, status, resistência a
elemento, recuperação, velocidade, esquiva, taxa crítica e taxa de AoE). Os demais (ataque por elemento, defesa por
tipo, bônus contra demônios, chances de ativar e habilidades) ficam em Pending, com o texto original.
Os níveis de um conjunto (2, 3, 4 peças) são somados (Cumulative = true). Isso é uma suposição a confirmar no jogo.
"""
import collections, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
pack, melia = Path(sys.argv[1]), Path(sys.argv[2])
sets = json.loads((ROOT / 'data/research/tosbase-2016-sets.json').read_text(encoding='utf-8'))['sets']
melia_items = set(map(int, re.findall(r'itemId:\s*(\d+)', (melia / 'system/db/items.txt').read_text(encoding='utf-8-sig'))))

ELEMENTS = {'Fire': 'ResFire', 'Ice': 'ResIce', 'Lightning': 'ResLightning', 'Earth': 'ResEarth', 'Poison': 'ResPoison',
            'Holy': 'ResHoly', 'Dark': 'ResDark', 'Darkness': 'ResDark'}
SIMPLE = [  # (padrão, atributos)
    (r'^(?:Physical )?Defense (\d+)$', ['Def']),
    (r'^Magic Defense (\d+)$', ['MDef']),
    (r'^HP (\d+)$', ['Mhp']),
    (r'^HP Recovery (\d+)$', ['Rhp']),
    (r'^SP Recovery (\d+)$', ['Rsp']),
    (r'^Stamina (\d+)$', ['MaxSta']),
    (r'^STR (\d+)$', ['Str']), (r'^CON (\d+)$', ['Con']), (r'^INT (\d+)$', ['Int']),
    (r'^SPR (\d+)$', ['Spr']), (r'^DEX (\d+)$', ['Dex']),
    (r'^All Stats (\d+)$', ['Str', 'Con', 'Int', 'Spr', 'Dex']),
    (r'^Evasion (\d+)$', ['Evasion']),
    (r'^Critical Rate (\d+)$', ['CritRate']),
    (r'^Movement Speed (\d+)$', ['MoveSpeed']),
    (r'^AoE Attack Ratio (\d+)$', ['AoeAttackRatio']),
]
STATS = ['Def', 'MDef', 'Mhp', 'Rhp', 'Rsp', 'MaxSta', 'Str', 'Con', 'Int', 'Spr', 'Dex', 'Evasion', 'CritRate', 'MoveSpeed',
         'AoeAttackRatio'] + sorted(set(ELEMENTS.values()), key=list(ELEMENTS.values()).index)


def parse(line):
    """Devolve a lista de (atributo, valor) da linha, ou None se o efeito não é um número simples."""
    for pattern, stats in SIMPLE:
        m = re.match(pattern, line)
        if m:
            return [(s, int(m.group(1))) for s in stats]
    m = re.match(r'^(\w+) Property Resistance (\d+)$', line)
    if m and m.group(1) in ELEMENTS:
        return [(ELEMENTS[m.group(1)], int(m.group(2)))]
    return None


out_sets, report_sets = [], []
done = pending = 0
missing_items = collections.Counter()
for sid, s in sorted(sets.items(), key=lambda kv: int(kv[0])):
    ids = [i['item_id'] for i in s['items']]
    for i in ids:
        if i not in melia_items:
            missing_items[i] += 1
    effects, pend = [], []
    for pieces, text in sorted(s['effects'].items(), key=lambda kv: int(kv[0])):
        for line in text.split('\n'):
            parsed = parse(line.strip())
            if parsed:
                effects += [(int(pieces), stat, value) for stat, value in parsed]
                done += 1
            else:
                pend.append((int(pieces), line.strip()))
                pending += 1
    note = None
    if ids and max(int(k) for k in s['effects']) > len(ids):
        note = f'o ToSBase lista {len(ids)} peça(s), mas o efeito pede {max(int(k) for k in s["effects"])}: a lista de peças parece incompleta (nos conjuntos de medalha e pulseira, provavelmente a mesma pulseira vale duas vezes, uma em cada mão)'
    out_sets.append((int(sid), s['name'], ids, effects, pend))
    report_sets.append({'id': int(sid), 'nome': s['name'], 'pecas': ids, 'efeitos_implementados': [{'pecas': p, 'atributo': a, 'valor': v} for p, a, v in effects],
                        'efeitos_pendentes': [{'pecas': p, 'texto': t} for p, t in pend], 'aviso': note})

cs_name = lambda t: t.replace('\\', '\\\\').replace('"', '\\"')
set_rows = []
for sid, name, ids, effects, pend in out_sets:
    eff = ', '.join(f'new SetEffect({p}, SetStat.{a}, {v}f)' for p, a, v in effects)
    set_rows.append(f'\t\tnew ItemSet2016({sid}, "{cs_name(name)}", new[] {{ {", ".join(map(str, ids))} }}, new SetEffect[] {{ {eff} }}),')
pend_rows = [f'\t\t({sid}, {p}, "{cs_name(t)}"),' for sid, _, _, _, pend in out_sets for p, t in pend]

cs = '''//--- Melia Script ----------------------------------------------------------
// Conjuntos de equipamento de 2016 (gerado por scripts/build-sets-pack-2016.py)
//--- Description -----------------------------------------------------------
// Bônus por quantidade de peças de um mesmo conjunto. O Melia não tem
// conjuntos: as funções de cálculo de status (calc_character.cs) chamam
// Original2016Sets.EquipBonus, então equipar ou tirar uma peça já recalcula.
// Só há efeitos que são um número somado a um atributo; os de combate
// (ataque por elemento, defesa por tipo, chances de ativar...) estão em
// Pending, com o texto original, e ainda não fazem nada.
// Não editar à mão: gere de novo pelo script.
//---------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Melia.Zone.World.Actors.Characters;

public enum SetStat
{
\t''' + ', '.join(STATS) + '''
}

public class SetEffect
{
\t/// <summary>Quantidade de peças do conjunto necessária para o efeito.</summary>
\tpublic int Pieces { get; }
\tpublic SetStat Stat { get; }
\tpublic float Value { get; }

\tpublic SetEffect(int pieces, SetStat stat, float value)
\t{
\t\tthis.Pieces = pieces; this.Stat = stat; this.Value = value;
\t}
}

public class ItemSet2016
{
\tpublic int Id { get; }
\tpublic string Name { get; }
\tpublic int[] ItemIds { get; }
\tpublic SetEffect[] Effects { get; }

\tpublic ItemSet2016(int id, string name, int[] itemIds, SetEffect[] effects)
\t{
\t\tthis.Id = id; this.Name = name; this.ItemIds = itemIds; this.Effects = effects;
\t}
}

public static class Original2016Sets
{
\t/// <summary>
\t/// true: com 3 peças valem os efeitos de 2 e de 3 peças somados; false: vale só o efeito da maior
\t/// quantidade atingida. O ToSBase lista cada nível separado e não diz qual é a regra: confirmar no jogo.
\t/// </summary>
\tpublic const bool Cumulative = true;

\tpublic static readonly ItemSet2016[] All =
\t{
''' + '\n'.join(set_rows) + '''
\t};

\t/// <summary>Efeitos de conjunto que o ToSBase mostra e que ainda não foram implementados (conjunto, peças, texto).</summary>
\tpublic static readonly (int SetId, int Pieces, string Text)[] Pending =
\t{
''' + '\n'.join(pend_rows) + '''
\t};

\t/// <summary>
\t/// Soma o bônus do atributo para as peças equipadas, dadas pelos ids dos itens (um por espaço de equipamento).
\t/// A mesma peça em dois espaços conta duas vezes: o conjunto Bronza lista só a medalha e a pulseira, mas pede 3
\t/// peças, o que só fecha com as duas pulseiras.
\t/// </summary>
\tpublic static float Bonus(IEnumerable<int> equippedItemIds, SetStat stat)
\t{
\t\tvar equipped = equippedItemIds.ToList();
\t\tvar sum = 0f;

\t\tforeach (var set in All)
\t\t{
\t\t\tvar count = equipped.Count(id => set.ItemIds.Contains(id));
\t\t\tif (count == 0)
\t\t\t\tcontinue;

\t\t\tvar active = set.Effects.Where(e => e.Stat == stat && e.Pieces <= count).ToList();
\t\t\tif (!Cumulative && active.Count > 0)
\t\t\t{
\t\t\t\tvar top = active.Max(e => e.Pieces);
\t\t\t\tactive = active.Where(e => e.Pieces == top).ToList();
\t\t\t}

\t\t\tsum += active.Sum(e => e.Value);
\t\t}

\t\treturn sum;
\t}

\t/// <summary>Bônus de conjunto do atributo para os itens que o personagem tem equipados.</summary>
\tpublic static float EquipBonus(Character character, SetStat stat)
\t\t=> Bonus(character.Inventory.GetEquip().Values.Select(i => i.Id), stat);
}
'''
target = pack / 'user/scripts/zone/custom/original2016/sets.cs'
target.parent.mkdir(parents=True, exist_ok=True)
target.write_text('﻿' + cs, encoding='utf-8')

lst = pack / 'user/scripts/zone/scripts_custom.txt'
header = '// Melia\n// Lista de scripts do pacote original 2016\n//---------------------------------------------------------------------------\n\n'
existing = [l for l in lst.read_text(encoding='utf-8').split('\n') if l.strip() and not l.startswith('//')] if lst.exists() else []
for line in ('custom/original2016/quests.cs', 'custom/original2016/refine.cs', 'custom/original2016/sets.cs'):
    if line not in existing:
        existing.append(line)
lst.write_text(header + '\n'.join(existing) + '\n', encoding='utf-8')

pend_kinds = collections.Counter(re.sub(r'\d+(\.\d+)?', 'N', t) for _, _, _, _, pend in out_sets for _, t in pend)
report = {'conjuntos': len(out_sets), 'efeitos_no_tosbase': done + pending, 'efeitos_implementados': done, 'efeitos_pendentes': pending,
          'cumulativo': True, 'itens_que_o_melia_nao_tem': dict(missing_items),
          'avisos': {r['id']: r['aviso'] for r in report_sets if r['aviso']},
          'conjunto_6': 'não existe em 2016: a captura do ToSBase para sets/6 é a lista genérica de conjuntos',
          'tipos_de_efeito_pendente': dict(pend_kinds.most_common()), 'por_conjunto': report_sets}
(pack / 'sets-report.json').write_text(json.dumps(report, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')
print(f'{len(out_sets)} conjuntos; {done} efeitos implementados, {pending} pendentes; peças fora do Melia: {dict(missing_items)}; avisos: {report["avisos"]}')
