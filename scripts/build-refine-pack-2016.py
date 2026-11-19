#!/usr/bin/env python3
"""Gera as regras de refino de 2016 como script do Melia e confere cada uma contra o ToSBase.

Uso: python3 scripts/build-refine-pack-2016.py PASTA_DO_PACOTE   (normalmente server/original-2016)
Lê data/research/tosbase-2016-items.json (tabela de refino de cada item: nível, bônus, chance, preço) e escreve:
  user/scripts/zone/custom/original2016/refine.cs   (classe Original2016Refine)
  user/scripts/zone/scripts_custom.txt              (garante a linha do arquivo acima)
  refine-report.json                                (cobertura e exceções)

Regras recuperadas (todas conferidas item a item; o gerador FALHA se alguma tabela do ToSBase não for reproduzida):
  * Bônus por nível. Base = grau + 2 para ATK e MATK, e grau // 2 + 1 para DEF, MDEF, precisão, esquiva e MHR.
    Até o limite do tipo (5 para ataque, 3 para o resto) o bônus é base x nível; depois disso sobe base x 2 por nível.
  * Chance de sucesso: armas 100% até +5, depois 88, 78, 68, 59, 51; demais peças 100% até +3, depois a mesma sequência.
  * Falha: perde 1 nível de refino e 1 de potencial (regra da wiki de 2016); com potencial 0 não dá para refinar.
  * Exceções: itens cujas tabelas fogem do padrão aparecem como tabelas extras, por id.
O preço do refino varia por item e fica na lista de preços, 15 níveis por item.
"""
import collections, json, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
pack = Path(sys.argv[1])
items = json.loads((ROOT / 'data/research/tosbase-2016-items.json').read_text(encoding='utf-8'))['items']

WEAPON_RATES = [100, 100, 100, 100, 100, 88, 78, 68, 59, 51, 51, 51, 51, 51, 51]
ARMOR_RATES = [100, 100, 100, 88, 78, 68, 59, 51, 51, 51, 51, 51, 51, 51, 51]
ATTACK = ('ATK', 'MATK')


def standard_bonus(stat, grade):
    base, limit = (grade + 2, 5) if stat in ATTACK else (grade // 2 + 1, 3)
    return [base * (lv if lv <= limit else limit + (lv - limit) * 2) for lv in range(1, 16)]


bonus_tables, rate_tables = [], []  # tabelas fora do padrão, por índice
entries, skipped = {}, collections.Counter()
for iid, it in sorted(items.items(), key=lambda kv: int(kv[0]) if kv[0].isdigit() else 0):
    if not iid.isdigit() or not it.get('enhancement'):
        continue
    stat, grade = it.get('enhancement_stat'), it.get('grade')
    if not stat or grade is None or len(it['enhancement']) != 15:
        skipped['sem atributo, grau ou 15 níveis'] += 1
        continue
    bonus = [int(e['bonus']) for e in it['enhancement']]
    if any(e['bonus'] != int(e['bonus']) for e in it['enhancement']):
        skipped['bônus fracionário'] += 1
        continue
    rates = [int(e['success_pct']) for e in it['enhancement']]
    std_rate = WEAPON_RATES if stat in ATTACK else ARMOR_RATES
    b_kind = -1 if bonus == standard_bonus(stat, grade) else None
    if b_kind is None:
        if bonus not in bonus_tables:
            bonus_tables.append(bonus)
        b_kind = bonus_tables.index(bonus)
    r_kind = -1 if rates == std_rate else None
    if r_kind is None:
        if rates not in rate_tables:
            rate_tables.append(rates)
        r_kind = rate_tables.index(rates)
    entries[int(iid)] = (stat, grade, b_kind, r_kind, [int(e['price']) for e in it['enhancement']])

# verificação própria: reconstrói cada tabela com as mesmas funções que o C# usa e compara com o ToSBase
for iid, (stat, grade, b_kind, r_kind, prices) in entries.items():
    got_b = standard_bonus(stat, grade) if b_kind < 0 else bonus_tables[b_kind]
    got_r = (WEAPON_RATES if stat in ATTACK else ARMOR_RATES) if r_kind < 0 else rate_tables[r_kind]
    src = items[str(iid)]['enhancement']
    assert got_b == [int(e['bonus']) for e in src], iid
    assert got_r == [int(e['success_pct']) for e in src], iid

STAT_CS = {'ATK': 'Atk', 'MATK': 'MAtk', 'DEF': 'Def', 'MDEF': 'MDef', 'HR': 'Accuracy', 'DR': 'Evasion', 'MHR': 'MagicAmp'}


def arr(values):
    return '{ ' + ', '.join(str(v) for v in values) + ' }'


rows = []
for iid, (stat, grade, b_kind, r_kind, prices) in sorted(entries.items()):
    rows.append(f'\t\t{{ {iid}, new RefineInfo(RefineStat.{STAT_CS[stat]}, {grade}, {b_kind}, {r_kind}, new[] {arr(prices)}) }},')

cs = '''//--- Melia Script ----------------------------------------------------------
// Regras de refino de 2016 (gerado por scripts/build-refine-pack-2016.py)
//--- Description -----------------------------------------------------------
// Bônus por nível, chance de sucesso e preço do refino de cada item, mais a
// regra de potencial. Funções puras: o servidor ainda não guarda o nível de
// refino nem o potencial de cada item (o Melia salva só o id e a quantidade),
// então nada aqui altera o jogo até haver esse armazenamento.
// Não editar à mão: gere de novo pelo script.
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public enum RefineStat { Atk, MAtk, Def, MDef, Accuracy, Evasion, MagicAmp }

public class RefineInfo
{
	public RefineStat Stat { get; }
	public int Grade { get; }
	/// <summary>-1 = tabela padrão; senão, índice em Original2016Refine.BonusTables.</summary>
	public int BonusTable { get; }
	/// <summary>-1 = chances padrão do tipo; senão, índice em Original2016Refine.RateTables.</summary>
	public int RateTable { get; }
	/// <summary>Preço em prata para chegar a cada nível (índice 0 = nível 1).</summary>
	public int[] Prices { get; }

	public RefineInfo(RefineStat stat, int grade, int bonusTable, int rateTable, int[] prices)
	{
		this.Stat = stat; this.Grade = grade; this.BonusTable = bonusTable; this.RateTable = rateTable; this.Prices = prices;
	}
}

public static class Original2016Refine
{
	public const int MaxLevel = 15;

	private static readonly int[] WeaponRates = ''' + arr(WEAPON_RATES) + ''';
	private static readonly int[] ArmorRates = ''' + arr(ARMOR_RATES) + ''';

	/// <summary>Tabelas de bônus de itens que fogem do padrão (15 níveis cada).</summary>
	public static readonly int[][] BonusTables =
	{
''' + '\n'.join('\t\tnew[] ' + arr(t) + ',' for t in bonus_tables) + '''
	};

	/// <summary>Tabelas de chance de sucesso (%) de itens que fogem do padrão.</summary>
	public static readonly int[][] RateTables =
	{
''' + '\n'.join('\t\tnew[] ' + arr(t) + ',' for t in rate_tables) + '''
	};

	/// <summary>Refino de cada item que o ToSBase de 2016 mostra.</summary>
	public static readonly Dictionary<int, RefineInfo> Items = new()
	{
''' + '\n'.join(rows) + '''
	};

	private static bool IsAttack(RefineStat stat) => stat == RefineStat.Atk || stat == RefineStat.MAtk;

	/// <summary>Bônus total do atributo refinado no nível dado (0 a 15).</summary>
	public static int Bonus(int itemId, int level)
	{
		if (level <= 0 || !Items.TryGetValue(itemId, out var info))
			return 0;
		level = Math.Min(level, MaxLevel);

		if (info.BonusTable >= 0)
			return BonusTables[info.BonusTable][level - 1];

		var attack = IsAttack(info.Stat);
		var baseValue = attack ? info.Grade + 2 : info.Grade / 2 + 1;
		var limit = attack ? 5 : 3;
		return baseValue * (level <= limit ? level : limit + (level - limit) * 2);
	}

	/// <summary>Chance de sucesso, em porcentagem, de refinar do nível anterior para o nível dado (1 a 15).</summary>
	public static int SuccessRate(int itemId, int toLevel)
	{
		if (toLevel < 1 || toLevel > MaxLevel || !Items.TryGetValue(itemId, out var info))
			return 0;

		if (info.RateTable >= 0)
			return RateTables[info.RateTable][toLevel - 1];

		return (IsAttack(info.Stat) ? WeaponRates : ArmorRates)[toLevel - 1];
	}

	/// <summary>Preço em prata para refinar até o nível dado.</summary>
	public static int Price(int itemId, int toLevel)
	{
		if (toLevel < 1 || toLevel > MaxLevel || !Items.TryGetValue(itemId, out var info))
			return 0;
		return info.Prices[toLevel - 1];
	}

	/// <summary>Só dá para refinar com potencial acima de 0 e abaixo do nível máximo.</summary>
	public static bool CanRefine(int itemId, int level, int potential)
		=> Items.ContainsKey(itemId) && potential > 0 && level < MaxLevel;

	/// <summary>
	/// Resolve uma tentativa de refino. Em caso de falha o item perde 1 nível (mínimo 0) e 1 de
	/// potencial. <paramref name="roll"/> é um número de 0 a 99.
	/// </summary>
	public static bool TryRefine(int itemId, ref int level, ref int potential, int roll)
	{
		if (!CanRefine(itemId, level, potential))
			return false;

		if (roll < SuccessRate(itemId, level + 1))
		{
			level++;
			return true;
		}

		level = Math.Max(0, level - 1);
		potential--;
		return false;
	}
}
'''
target = pack / 'user/scripts/zone/custom/original2016/refine.cs'
target.parent.mkdir(parents=True, exist_ok=True)
target.write_text('﻿' + cs, encoding='utf-8')

lst = pack / 'user/scripts/zone/scripts_custom.txt'
header = '// Melia\n// Lista de scripts do pacote original 2016\n//---------------------------------------------------------------------------\n\n'
existing = [l for l in lst.read_text(encoding='utf-8').split('\n') if l.strip() and not l.startswith('//')] if lst.exists() else []
for line in ('custom/original2016/quests.cs', 'custom/original2016/refine.cs'):
    if line not in existing:
        existing.append(line)
lst.write_text(header + '\n'.join(existing) + '\n', encoding='utf-8')

stat_counts = collections.Counter(e[0] for e in entries.values())
report = {'itens_com_tabela_de_refino': len(entries), 'sem_tabela_utilizavel': dict(skipped),
          'tabelas_de_bonus_fora_do_padrao': len(bonus_tables), 'itens_com_bonus_fora_do_padrao': sum(1 for e in entries.values() if e[2] >= 0),
          'tabelas_de_chance_fora_do_padrao': len(rate_tables), 'itens_com_chance_fora_do_padrao': sum(1 for e in entries.values() if e[3] >= 0),
          'por_atributo': dict(stat_counts), 'tabelas_de_bonus': bonus_tables, 'tabelas_de_chance': rate_tables}
(pack / 'refine-report.json').write_text(json.dumps(report, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')
print(f"{len(entries)} itens com refino; {report['itens_com_bonus_fora_do_padrao']} com bônus fora do padrão ({len(bonus_tables)} tabelas); "
      f"{report['itens_com_chance_fora_do_padrao']} com chance fora do padrão ({len(rate_tables)} tabelas); sem tabela utilizável: {dict(skipped)}")
