//--- Melia Script ----------------------------------------------------------
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
	Def, MDef, Mhp, Rhp, Rsp, MaxSta, Str, Con, Int, Spr, Dex, Evasion, CritRate, MoveSpeed, AoeAttackRatio, ResFire, ResIce, ResLightning, ResEarth, ResPoison, ResHoly, ResDark
}

public class SetEffect
{
	/// <summary>Quantidade de peças do conjunto necessária para o efeito.</summary>
	public int Pieces { get; }
	public SetStat Stat { get; }
	public float Value { get; }

	public SetEffect(int pieces, SetStat stat, float value)
	{
		this.Pieces = pieces; this.Stat = stat; this.Value = value;
	}
}

public class ItemSet2016
{
	public int Id { get; }
	public string Name { get; }
	public int[] ItemIds { get; }
	public SetEffect[] Effects { get; }

	public ItemSet2016(int id, string name, int[] itemIds, SetEffect[] effects)
	{
		this.Id = id; this.Name = name; this.ItemIds = itemIds; this.Effects = effects;
	}
}

public static class Original2016Sets
{
	/// <summary>
	/// true: com 3 peças valem os efeitos de 2 e de 3 peças somados; false: vale só o efeito da maior
	/// quantidade atingida. O ToSBase lista cada nível separado e não diz qual é a regra: confirmar no jogo.
	/// </summary>
	public const bool Cumulative = true;

	public static readonly ItemSet2016[] All =
	{
		new ItemSet2016(1, "Light Plate Set", new[] { 522110, 532110 }, new SetEffect[] { new SetEffect(2, SetStat.AoeAttackRatio, 1f) }),
		new ItemSet2016(2, "Zega Set", new[] { 532111 }, new SetEffect[] { new SetEffect(2, SetStat.Def, 6f) }),
		new ItemSet2016(3, "Magi Set", new[] { 522112, 532112 }, new SetEffect[] {  }),
		new ItemSet2016(4, "Jude Set", new[] { 522113, 532113 }, new SetEffect[] { new SetEffect(2, SetStat.MoveSpeed, 5f) }),
		new ItemSet2016(5, "Ismin Set", new[] { 522114, 532114 }, new SetEffect[] {  }),
		new ItemSet2016(7, "Cafrisun Set", new[] { 502123, 512123, 522123, 532123 }, new SetEffect[] { new SetEffect(2, SetStat.Rhp, 16f), new SetEffect(3, SetStat.Str, 1f), new SetEffect(3, SetStat.Con, 1f), new SetEffect(3, SetStat.Int, 1f), new SetEffect(3, SetStat.Spr, 1f), new SetEffect(3, SetStat.Dex, 1f) }),
		new ItemSet2016(8, "Riena Set", new[] { 532150, 522150 }, new SetEffect[] { new SetEffect(2, SetStat.ResFire, 16f) }),
		new ItemSet2016(9, "Riena Set", new[] { 532151, 522151 }, new SetEffect[] { new SetEffect(2, SetStat.ResFire, 16f) }),
		new ItemSet2016(10, "Riena Set", new[] { 532152, 522152 }, new SetEffect[] { new SetEffect(2, SetStat.ResFire, 16f) }),
		new ItemSet2016(11, "Watcher Set", new[] { 502153, 512153, 522153, 532153 }, new SetEffect[] {  }),
		new ItemSet2016(12, "Abomination Set", new[] { 603106, 583105 }, new SetEffect[] { new SetEffect(2, SetStat.Str, 3f), new SetEffect(2, SetStat.Con, 3f), new SetEffect(2, SetStat.Int, 3f), new SetEffect(2, SetStat.Spr, 3f), new SetEffect(2, SetStat.Dex, 3f) }),
		new ItemSet2016(13, "Earth Cloth Armor Set", new[] { 502154, 512154, 522154, 532154 }, new SetEffect[] { new SetEffect(2, SetStat.Con, 5f), new SetEffect(3, SetStat.MaxSta, 10f) }),
		new ItemSet2016(14, "Earth Leather Armor Set", new[] { 502155, 512155, 522155, 532155 }, new SetEffect[] { new SetEffect(2, SetStat.Con, 5f), new SetEffect(3, SetStat.MaxSta, 10f) }),
		new ItemSet2016(15, "Earth Plate Armor Set", new[] { 502156, 512156, 522156, 532156 }, new SetEffect[] { new SetEffect(2, SetStat.Con, 5f), new SetEffect(3, SetStat.MaxSta, 10f) }),
		new ItemSet2016(16, "Legwyn Family's Cloth Armor Set", new[] { 502157, 512157, 522157, 532157 }, new SetEffect[] { new SetEffect(2, SetStat.ResDark, 10f), new SetEffect(3, SetStat.Spr, 6f) }),
		new ItemSet2016(17, "Legwyn Family's Leather Armor Set", new[] { 502158, 512158, 522158, 532158 }, new SetEffect[] { new SetEffect(2, SetStat.ResDark, 10f), new SetEffect(3, SetStat.Spr, 6f) }),
		new ItemSet2016(18, "Legwyn Family's Plate Armor Set", new[] { 502159, 512159, 522159, 532159 }, new SetEffect[] { new SetEffect(2, SetStat.ResDark, 10f), new SetEffect(3, SetStat.Spr, 6f) }),
		new ItemSet2016(19, "Karacha Set", new[] { 183108, 113002 }, new SetEffect[] { new SetEffect(2, SetStat.Evasion, 188f) }),
		new ItemSet2016(20, "Velnia Monkey Set", new[] { 522187, 532187 }, new SetEffect[] { new SetEffect(2, SetStat.Dex, 6f) }),
		new ItemSet2016(21, "Elgos Set", new[] { 502188, 512188, 522188, 532188 }, new SetEffect[] { new SetEffect(2, SetStat.MDef, 5f), new SetEffect(3, SetStat.MDef, 10f), new SetEffect(4, SetStat.Int, 15f) }),
		new ItemSet2016(22, "Ibre Set", new[] { 502189, 512189, 522189, 532189 }, new SetEffect[] { new SetEffect(2, SetStat.Def, 5f), new SetEffect(3, SetStat.Def, 10f), new SetEffect(4, SetStat.Evasion, 15f) }),
		new ItemSet2016(23, "Grynas Set", new[] { 502190, 512190, 522190, 532190 }, new SetEffect[] { new SetEffect(2, SetStat.ResDark, 10f), new SetEffect(3, SetStat.ResDark, 20f), new SetEffect(4, SetStat.Mhp, 1020f) }),
		new ItemSet2016(24, "Poratore Set", new[] { 603103, 603104 }, new SetEffect[] {  }),
		new ItemSet2016(25, "Virtov Set", new[] { 503114, 513114, 523114, 533114 }, new SetEffect[] {  }),
		new ItemSet2016(26, "Virtov Leather Set", new[] { 503115, 513115, 523115, 533115 }, new SetEffect[] {  }),
		new ItemSet2016(27, "Virtov Plate Set", new[] { 503116, 513116, 523116, 533116 }, new SetEffect[] {  }),
		new ItemSet2016(28, "Lolopanther Set", new[] { 504106, 514106, 524106, 534106 }, new SetEffect[] { new SetEffect(2, SetStat.Rsp, 100f), new SetEffect(4, SetStat.Int, 25f) }),
		new ItemSet2016(29, "Lolopanther Leather Set", new[] { 504107, 514107, 524107, 534107 }, new SetEffect[] { new SetEffect(2, SetStat.CritRate, 46f), new SetEffect(4, SetStat.Dex, 25f) }),
		new ItemSet2016(30, "Lolopanther Plate Set", new[] { 504108, 514108, 524108, 534108 }, new SetEffect[] { new SetEffect(2, SetStat.Rhp, 190f), new SetEffect(4, SetStat.Str, 25f) }),
		new ItemSet2016(31, "Bronza Set", new[] { 582119, 602116 }, new SetEffect[] { new SetEffect(3, SetStat.MDef, 10f) }),
		new ItemSet2016(32, "Argint Set", new[] { 582120, 602117 }, new SetEffect[] { new SetEffect(3, SetStat.MDef, 14f) }),
		new ItemSet2016(33, "Auru Set", new[] { 582121, 602118 }, new SetEffect[] { new SetEffect(3, SetStat.MDef, 18f) }),
		new ItemSet2016(34, "Platina Set", new[] { 582122, 602119 }, new SetEffect[] { new SetEffect(3, SetStat.MDef, 22f) }),
	};

	/// <summary>Efeitos de conjunto que o ToSBase mostra e que ainda não foram implementados (conjunto, peças, texto).</summary>
	public static readonly (int SetId, int Pieces, string Text)[] Pending =
	{
		(3, 2, "Dark Property Attack 30"),
		(5, 2, "Fire Property Attack 15"),
		(7, 4, "Earth Property Additional Damage 12"),
		(11, 2, "Attack against Devil-type Targets 6"),
		(11, 3, "Attack against Devil-type Targets 8"),
		(11, 4, "1% chance to gain 30 seconds of increased attack against Devil-type Targets by 157"),
		(13, 4, "Gives additional Earth property damage when receiving Earth property attacks"),
		(14, 4, "Gives additional Earth property damage when receiving Earth property attacks"),
		(15, 4, "Gives additional Earth property damage when receiving Earth property attacks"),
		(16, 4, "1% chance of recovering 200 SP for 5 seconds when attacked by a Devil-type enemy"),
		(17, 4, "1% chance of recovering 200 SP for 5 seconds when attacked by a Devil-type enemy"),
		(18, 4, "1% chance of recovering 200 SP for 5 seconds when attacked by a Devil-type enemy"),
		(19, 2, "Increases [Backstab] damage by 3x when attacking an enemy from behind"),
		(24, 2, "6 magic defense, 6 physical defense for 30 seconds with every basic and physical attack"),
		(24, 2, "Increases Holy and Dark property attack equal to 7.5% magic defense for 10 seconds when stacked 20 times"),
		(25, 2, "Ice Property Attack 12"),
		(25, 3, "Ice Property Attack 26"),
		(25, 4, "Pierce-type Defense 25"),
		(26, 2, "Ice Property Attack 12"),
		(26, 3, "Ice Property Attack 26"),
		(26, 4, "Strike-type Defense 25"),
		(27, 2, "Ice Property Attack 12"),
		(27, 3, "Ice Property Attack 26"),
		(27, 4, "Slash-type Defense 25"),
		(28, 3, "Earth Property Attack 42"),
		(29, 3, "Earth Property Attack 42"),
		(30, 3, "Earth Property Attack 42"),
	};

	/// <summary>
	/// Soma o bônus do atributo para as peças equipadas, dadas pelos ids dos itens (um por espaço de equipamento).
	/// A mesma peça em dois espaços conta duas vezes: o conjunto Bronza lista só a medalha e a pulseira, mas pede 3
	/// peças, o que só fecha com as duas pulseiras.
	/// </summary>
	public static float Bonus(IEnumerable<int> equippedItemIds, SetStat stat)
	{
		var equipped = equippedItemIds.ToList();
		var sum = 0f;

		foreach (var set in All)
		{
			var count = equipped.Count(id => set.ItemIds.Contains(id));
			if (count == 0)
				continue;

			var active = set.Effects.Where(e => e.Stat == stat && e.Pieces <= count).ToList();
			if (!Cumulative && active.Count > 0)
			{
				var top = active.Max(e => e.Pieces);
				active = active.Where(e => e.Pieces == top).ToList();
			}

			sum += active.Sum(e => e.Value);
		}

		return sum;
	}

	/// <summary>Bônus de conjunto do atributo para os itens que o personagem tem equipados.</summary>
	public static float EquipBonus(Character character, SetStat stat)
		=> Bonus(character.Inventory.GetEquip().Values.Select(i => i.Id), stat);
}
