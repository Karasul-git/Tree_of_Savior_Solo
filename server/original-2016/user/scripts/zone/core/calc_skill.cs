//--- Melia Script ----------------------------------------------------------
// Skill Calculation Script
//--- Description -----------------------------------------------------------
// Functions that calculate skill-related values, such as properties.
//---------------------------------------------------------------------------

using System;
using Melia.Shared.Data.Database;
using Melia.Shared.Game.Const;
using Melia.Zone;
using Melia.Zone.Scripting;
using Melia.Zone.Skills;
using Melia.Zone.World.Actors.Characters;
using Melia.Zone.World.Actors.Characters.Components;
using Melia.Zone.World.Actors.Monsters;

public class SkillCalculationsScript : GeneralScript
{
	/// <summary>
	/// Returns skill's AoE Attack Ratio?
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_GET_SR_LV(Skill skill)
	{
		var baseValue = skill.Properties.GetFloat(PropertyName.SklSR);

		var byOwner = 0f;
		if (skill.Owner is Character character)
			byOwner += character.Properties.GetFloat(PropertyName.SR);

		return Math.Max(1, baseValue + byOwner);
	}


	/// <summary>
	/// Returns skill factor, which is a multiplier typically applied
	/// to an actors base damage.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_SklFactor(Skill skill)
	{
		var baseValue = skill.Data.Factor;
		var byConfRate = 1f;

		if (skill.IsNormalAttack)
			byConfRate = ZoneServer.Instance.Conf.World.NormalAttackSkillFactorRate;
		else if (skill.Owner is IMonster)
			byConfRate = ZoneServer.Instance.Conf.World.MonsterSkillFactorRate;
		else
			byConfRate = ZoneServer.Instance.Conf.World.PlayerSkillFactorRate;

		return baseValue * byConfRate;
	}

	/// <summary>
	/// Returns skill factor by level, which is a multiplier typically applied
	/// to an actors base damage for each level of the skill.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_SklFactorByLevel(Skill skill)
	{
		var baseValue = skill.Data.FactorByLevel;
		var byConfRate = 1f;

		if (skill.IsNormalAttack)
			byConfRate = ZoneServer.Instance.Conf.World.NormalAttackSkillFactorRate;
		else if (skill.Owner is IMonster)
			byConfRate = ZoneServer.Instance.Conf.World.MonsterSkillFactorRate;
		else
			byConfRate = ZoneServer.Instance.Conf.World.PlayerSkillFactorRate;

		return baseValue * byConfRate;
	}

	/// <summary>
	/// Returns skill's skill factor, which in most cases in equivilant
	/// to the skill's damage in percentage.
	/// </summary>
	/// <example>
	/// var damage = SCR_GetRandomAtk(attacker, target, skill) * SCR_Get_SkillFactor(skill) / 100f;
	/// </example>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_SkillFactor(Skill skill)
	{
		var SCR_Get_AbilityReinforceRate = ScriptableFunctions.Skill.Get("SCR_Get_AbilityReinforceRate");

		var sklFactor = skill.Properties.GetFloat(PropertyName.SklFactor);
		var sklFactorByLevel = skill.Properties.GetFloat(PropertyName.SklFactorByLevel);

		var value = sklFactor + (sklFactorByLevel * (skill.Level - 1));

		var byReinforceRate = SCR_Get_AbilityReinforceRate(skill);
		value += value * byReinforceRate;

		return value;
	}

	/// <summary>
	/// Returns the reinforce ability rate for the skill.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_AbilityReinforceRate(Skill skill)
	{
		if (skill.Data.ReinforceAbility == 0)
			return 0;

		if (!skill.Owner.Components.TryGet<AbilityComponent>(out var abilities))
			return 0;

		if (!abilities.TryGetActive(skill.Data.ReinforceAbility, out var reinforceAbility))
			return 0;

		var byAbility = reinforceAbility.Level * 0.005f;

		if (reinforceAbility.Level == 100)
			byAbility += 0.10f;

		var byHidden = 0f;
		if (skill.Data.HiddenReinforceAbility != 0 && abilities.TryGetActive(skill.Data.HiddenReinforceAbility, out var hiddenReinforceAbility))
		{
			var level = hiddenReinforceAbility.Level;
			var factorByLevel = skill.Data.HiddenReinforceAbilityFactorByLevel;

			byHidden = level * factorByLevel / 100f;
		}

		return byAbility + byHidden;
	}

	/// <summary>
	/// Returns skill's bonus damage.
	/// </summary>
	/// <example>
	/// var damage = SCR_GetRandomAtk(attacker, target, skill) + SCR_Get_SkillAtkAdd(skill);
	/// </example>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_SkillAtkAdd(Skill skill)
	{
		var sklAtkAdd = skill.Properties.GetFloat(PropertyName.SklAtkAdd);
		var sklAtkAddByLevel = skill.Properties.GetFloat(PropertyName.SklAtkAddByLevel);

		return sklAtkAdd + (sklAtkAddByLevel * (skill.Level - 1));
	}

	/// <summary>
	/// Returns the amount of SP spent when using the skill.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_SpendSP(Skill skill)
	{
		var SCR_Get_SpendSP_AbilityModifier = ScriptableFunctions.Skill.Get(nameof(SCR_Get_SpendSP_AbilityRate));

		var baseValue = skill.Data.BasicSp;
		if (baseValue == 0)
			return 0;

		// 2016: o custo vem de uma tabela por nível da habilidade, sem correção pelo nível do personagem.
		if (Original2016SkillTable.SpByLevel.TryGetValue((int)skill.Id, out var table) && table.Length > 0)
			return (int)Math.Max(0, table[Math.Clamp(skill.Level, 1, table.Length) - 1]);

		var ownerLevel = skill.Owner.Level;
		var levelCorrection = ownerLevel - 300f;

		// The value starts at ~18% at level 1 and keeps going up as the
		// owner's level increases. At level 100 it's 45%, at level 200
		// it's 72.5%, at 300 it's 100%, and it goes past that afterwards.
		if (levelCorrection < 0)
			levelCorrection = levelCorrection * 2.75f / 1000f;
		else if (levelCorrection >= 0)
			levelCorrection = levelCorrection * 1.25f / 1000f;

		var value = baseValue * (levelCorrection + 1);

		var byAbilityRate = SCR_Get_SpendSP_AbilityModifier(skill);
		value += value * (byAbilityRate / 100f);

		// TODO: Add ExProp "ZEMINA_BUFF_SP", reducing SpendSP by its
		// value times ZEMINA_BUFF_SP.

		return (int)Math.Max(0, value);
	}

	/// <summary>
	/// Returns the modifier for the skill's SP usage based on the owner's
	/// active abilities. The result is a relative percentage, such as
	/// 10 for a +10% increase.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_SpendSP_AbilityRate(Skill skill)
	{
		var result = 0;

		if (skill.Owner.Components.TryGet<AbilityComponent>(out var abilities))
			result += abilities.GetModifier(skill.Id.ToString(), AbilityModifierType.SP);

		return result;
	}

	/// <summary>
	/// Returns the amount of stamina spent when using the skill.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Skill_STA(Skill skill)
	{
		var baseValue = skill.Data.BasicStamina;
		if (baseValue == 0)
			return 0;

		// The value in the database is in "displayed stamina", so they
		// need to be multiplied to get the actual value, which is a
		// thousand times the display value. Alternatively we could
		// adjust the skill data, but it's safer to leave the game's
		// data untouched.
		return baseValue * 1000;
	}

	/// <summary>
	/// Returns the skill's wave length, which is related to its area
	/// of effect.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_WaveLength(Skill skill)
	{
		var baseValue = skill.Properties.GetFloat(PropertyName.SklWaveLength);

		var byOwner = 0f;
		if (skill.Data.SplashType == SplashType.Square)
		{
			byOwner += skill.Owner.Properties.GetFloat(PropertyName.SkillRange);
			byOwner += skill.Owner.Properties.GetFloat(skill.Data.AttackType + "_Range");
		}

		return baseValue + byOwner;
	}

	/// <summary>
	/// Returns the skill's splash angle, which is related to its area
	/// of effect.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_SPLANGLE(Skill skill)
	{
		if (skill.Data.SplashType != SplashType.Fan)
			return skill.Properties.GetFloat(PropertyName.SklSplAngle);

		var baseValue = skill.Properties.GetFloat(PropertyName.SklSplAngle);
		var byOwner = skill.Owner.Properties.GetFloat(PropertyName.SkillAngle);

		return baseValue + byOwner;
	}

	/// <summary>
	/// Returns the skill's wave length, which is related to its attack
	/// range.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_Get_SplRange(Skill skill)
	{
		var baseValue = skill.Properties.GetFloat(PropertyName.SklSplRange);

		var byOwner = 0f;
		if (skill.Data.SplashType == SplashType.Fan)
		{
			byOwner += skill.Owner.Properties.GetFloat(PropertyName.SkillRange);
			byOwner += skill.Owner.Properties.GetFloat(skill.Data.AttackType + "_Range");
		}
		else if (skill.Data.SplashType == SplashType.Square)
		{
			byOwner += skill.Owner.Properties.GetFloat(PropertyName.SkillAngle);
		}

		return baseValue + byOwner;
	}

	/// <summary>
	/// Returns the skill's delay time, which appears to be used as the
	/// delay until a monster can use a skill again.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_GET_DELAY_TIME(Skill skill)
	{
		if (skill.Owner is not IMonster monster)
			return skill.Properties.GetFloat(PropertyName.DelayTime);

		float baseValue;

		if (skill.Data.ClassType == SkillClassType.Missile || skill.Data.UseType == SkillUseType.Force || skill.Data.UseType == SkillUseType.ForceGround)
		{
			if (monster.Level < 75)
				baseValue = 3000;
			else if (monster.Level < 170)
				baseValue = 2500;
			else if (monster.Level < 220)
				baseValue = 2000;
			else
				baseValue = 1500;
		}
		else
		{
			if (monster.Level < 40)
				baseValue = 3000;
			else if (monster.Level < 75)
				baseValue = 2500;
			else if (monster.Level < 170)
				baseValue = 2000;
			else if (monster.Level < 220)
				baseValue = 1500;
			else
				baseValue = 1000;
		}

		var byDelayRate = ZoneServer.Instance.Conf.World.MonsterSkillDelayRate;

		return baseValue * byDelayRate;
	}

	/// <summary>
	/// Calculates and returns the skill's speed rate.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_GET_SklSpdRate(Skill skill)
	{
		// Formulas based on player accounts and experimentation. They're
		// not 100% accurate, but the goal was to get them close to the
		// values seen in the skill packets, and the result kinda works.
		// And isn't that all that matters? Yes. Yes it is. ^__^
		//
		// https://forum.treeofsavior.com/t/attack-speed-formula/395989
		// 
		// We can see the skill speed rate in the skill packets, and we
		// can see it change with the dex stat, so this is definitely a
		// factor, and the formulas found by the community somewhat match
		// the logged values.
		// The bonuses from skills and buffs seem weird at first glance
		// (for example, Hasisas adds "175 Attack Speed"), but given our
		// formula this value actually makes sense, as Hasisas was logged
		// to increase the speed rate by ~0.18, which is a close match
		// for bonus/1000.
		// 
		// Theory on how attack speed works:
		// Every skill, including normal attack skills, have a "shoot time",
		// which seems to be the time until the skill can be used again.
		// For example, if you have a skill speed rate of 1 and you use
		// a skill with a shoot time of 1000, the client will use the
		// skill every ~1000ms. If you then raise the skill speed rate,
		// the client will use the skill every ~1000ms divided by the
		// speed rate. This means you'd reach a hit rate of 2 hits per
		// second if you get your speed rate to 2.
		// For a skill with a much lower shoot time you attack faster
		// out of the gate and higher speed rates will have a lower
		// impact on your hits/second. For example, the default shoot
		// time of the dagger attack skill is 400ms, which would be
		// lowered to 200ms with a speed rate of 2.
		// It's currently unknown whether there's a speed cap, though
		// the hits get a little unrealiable at higher values, above a
		// speed rate of 3. It's safe to assume that there is a limit
		// somewhere in the 2~3 speed rate range. But for now we'll
		// leave it like this for funsies.

		var baseValue = skill.Properties.GetFloat(PropertyName.SklSpdRateValue);
		var byDex = 0f;
		var byBuff = 0f;

		if (skill.Owner is IMonster)
		{
			baseValue *= ZoneServer.Instance.Conf.World.MonsterSkillSpeedRate;
		}

		if (skill.Data.SpeedRateAffectedByDex)
		{
			var dex = skill.Owner.Properties.GetFloat(PropertyName.DEX);
			byDex = (float)Math.Pow(dex / 500f, 0.46f);
		}

		if (skill.Data.SpeedRateAffectedByBuff)
		{
			var spdBm = skill.Owner.Properties.GetFloat(PropertyName.SPD_BM, 0);
			byBuff = spdBm / 1000f;
		}

		// Don't let the result go to 0, as such a speed rate would result
		// in an infinite damage delay. Limiting it to 0.01 effectively
		// means that a skill can't be modified to be more than 100
		// times slower than normal.
		return (float)Math.Max(0.01f, baseValue + byDex + byBuff);
	}

	/// <summary>
	/// Calculates and returns the skill's shoot time.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_GET_ShootTime(Skill skill)
	{
		var sklSpdRate = skill.Properties.GetFloat(PropertyName.SklSpdRate);
		var baseValue = skill.Data.ShootTime.TotalMilliseconds;

		return (float)(baseValue / sklSpdRate);
	}

	/// <summary>
	/// Calculates and returns the skill's overheat cooldown time in
	/// milliseconds.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_GET_USEOVERHEAT(Skill skill)
	{
		var getAbilityRate = ScriptableFunctions.Skill.Get(nameof(SCR_GET_USEOVERHEAT_AbilityRate));

		var baseValue = (int)skill.Data.CooldownTime.TotalMilliseconds;
		var buffModifier = 1f;

		if (skill.Owner.Properties.TryGetFloat(PropertyName.OverHeat_BM, out var overheatBm))
			buffModifier += overheatBm / 100f;

		var abilityModifier = getAbilityRate(skill);

		return (int)Math.Max(0, baseValue * buffModifier + abilityModifier);
	}

	/// <summary>
	/// Returns the cooldown modifier for the skill, in milliseconds,
	/// based on the skill owner's active abilities.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_GET_USEOVERHEAT_AbilityRate(Skill skill)
	{
		var result = 0;

		if (skill.Owner.Components.TryGet<AbilityComponent>(out var abilities))
			result += abilities.GetModifier(skill.Id.ToString(), AbilityModifierType.CoolDown);

		return result;
	}

	/// <summary>
	/// Calculates and returns the skill's available max overheat count,
	/// indicating how many times the skill can be used before it goes on
	/// cooldown.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float GET_SKILL_OVERHEAT_COUNT(Skill skill)
	{
		var baseValue = skill.Data.OverheatCount;

		return Math.Max(0, baseValue);
	}

	/// <summary>
	/// Returns the hit delay of the skills, which affects animations.
	/// </summary>
	/// <param name="skill"></param>
	/// <returns></returns>
	[ScriptableFunction]
	public float SCR_GET_HIT_DELAY(Skill skill)
	{
		var baseValue = skill.Data.DefaultHitDelay.TotalMilliseconds;

		// TODO: Apply skill speed rate and other modifiers here? Probably.

		return (float)Math.Floor(baseValue);
	}
}


/// <summary>
/// Custo de SP por nível de habilidade em 2016 (ToSBase). Gerado por scripts/build-server-pack-2016.py.
/// </summary>
public static class Original2016SkillTable
{
	public static readonly System.Collections.Generic.Dictionary<int, float[]> SpByLevel = new()
	{
		{ 2111, new float[] { 13f, 16f, 19f, 22f, 25f, 29f, 32f, 35f, 38f, 41f, 45f, 48f, 51f, 54f, 57f } },
		{ 2112, new float[] { 13f, 16f, 19f, 22f, 25f, 29f, 32f, 35f, 38f, 41f, 45f, 48f, 51f, 54f, 57f } },
		{ 2113, new float[] { 13f, 16f, 19f, 22f, 25f, 29f, 32f, 35f, 38f, 41f, 45f, 48f, 51f, 54f, 57f } },
		{ 2114, new float[] { 13f, 16f, 19f, 22f, 25f, 29f, 32f, 35f, 38f, 41f, 45f, 48f, 51f, 54f, 57f } },
		{ 2115, new float[] { 13f, 16f, 19f, 22f, 25f, 29f, 32f, 35f, 38f, 41f, 45f, 48f, 51f, 54f, 57f } },
		{ 10001, new float[] { 8f, 9f, 10f, 11f, 12f, 14f, 15f, 16f, 17f, 18f, 20f, 21f, 22f, 23f, 24f } },
		{ 10002, new float[] { 8f, 9f, 10f, 12f, 13f, 15f, 16f, 17f, 19f, 20f, 22f, 23f, 24f, 26f, 27f } },
		{ 10003, new float[] { 7f, 8f, 9f, 11f, 12f, 14f, 15f, 16f, 18f, 19f, 21f, 22f, 23f, 25f, 26f } },
		{ 10004, new float[] { 11f, 12f, 13f, 15f, 16f, 18f, 19f, 20f, 22f, 23f, 25f, 26f, 27f, 29f, 30f } },
		{ 10005, new float[] { 27f, 30f, 33f, 37f, 40f, 44f, 47f, 50f, 54f, 57f, 61f, 64f, 67f, 71f, 74f } },
		{ 10006, new float[] { 14f, 15f, 17f, 19f, 20f, 22f, 24f, 25f, 27f, 29f } },
		{ 10007, new float[] { 13f, 14f, 16f, 18f, 20f, 22f, 23f, 25f, 27f, 29f } },
		{ 10008, new float[] { 15f, 17f, 19f, 21f, 23f } },
		{ 10101, new float[] { 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 30f, 32f, 35f, 37f, 39f, 41f, 43f } },
		{ 10102, new float[] { 13f, 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 30f, 32f, 33f, 35f, 37f, 39f } },
		{ 10103, new float[] { 17f, 18f, 20f, 22f, 24f, 26f, 27f, 29f, 31f, 33f, 35f, 36f, 38f, 40f, 42f } },
		{ 10104, new float[] { 12f, 13f, 15f, 17f, 19f, 21f, 22f, 24f, 26f, 28f, 30f, 31f, 33f, 35f, 37f } },
		{ 10105, new float[] { 14f, 15f, 17f, 19f, 20f, 22f, 24f, 25f, 27f, 29f } },
		{ 10106, new float[] { 17f, 19f, 21f, 23f, 25f, 27f, 29f, 31f, 33f, 35f } },
		{ 10108, new float[] { 14f, 16f, 18f, 20f, 22f } },
		{ 10109, new float[] { 19f, 22f, 25f, 28f, 31f } },
		{ 10201, new float[] { 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 30f, 32f, 34f, 36f, 38f, 40f, 42f } },
		{ 10202, new float[] { 15f, 17f, 19f, 21f, 23f, 25f, 27f, 29f, 31f, 33f, 35f, 37f, 39f, 41f, 43f } },
		{ 10203, new float[] { 17f, 19f, 21f, 23f, 25f, 27f, 29f, 31f, 33f, 35f, 38f, 40f, 42f, 44f, 46f } },
		{ 10204, new float[] { 12f, 13f, 15f, 17f, 19f, 21f, 22f, 24f, 26f, 28f, 30f, 31f, 33f, 35f, 37f } },
		{ 10205, new float[] { 19f, 21f, 24f, 26f, 29f, 31f, 34f, 36f, 39f, 41f } },
		{ 10206, new float[] { 18f, 20f, 22f, 24f, 26f } },
		{ 10207, new float[] { 8f, 10f, 12f, 14f, 16f, 18f, 20f, 22f, 24f, 26f } },
		{ 10208, new float[] { 8f, 9f, 10f, 11f, 12f, 14f, 15f, 16f, 17f, 18f } },
		{ 10209, new float[] { 8f, 9f, 10f, 11f, 12f } },
		{ 10301, new float[] { 19f, 21f, 24f, 27f, 29f, 32f, 35f, 37f, 40f, 43f, 46f, 48f, 51f, 54f, 56f } },
		{ 10302, new float[] { 18f, 20f, 22f, 24f, 27f, 29f, 31f, 34f, 36f, 38f, 41f, 43f, 45f, 47f, 50f } },
		{ 10303, new float[] { 21f, 23f, 26f, 29f, 32f, 35f, 38f, 41f, 44f, 47f, 50f, 52f, 55f, 58f, 61f } },
		{ 10304, new float[] { 20f, 22f, 25f, 28f, 30f, 33f, 36f, 38f, 41f, 44f, 47f, 49f, 52f, 55f, 57f } },
		{ 10305, new float[] { 24f, 27f, 30f, 33f, 36f, 39f, 42f, 45f, 48f, 51f } },
		{ 10306, new float[] { 25f, 28f, 31f, 34f, 37f } },
		{ 10307, new float[] { 14f, 17f, 20f, 23f, 26f, 30f, 33f, 36f, 39f, 42f } },
		{ 10401, new float[] { 19f, 21f, 23f, 26f, 28f, 31f, 33f, 35f, 38f, 40f, 43f, 45f, 47f, 50f, 52f } },
		{ 10404, new float[] { 20f, 22f, 25f, 28f, 31f, 34f, 36f, 39f, 42f, 45f, 48f, 50f, 53f, 56f, 59f } },
		{ 10405, new float[] { 22f, 25f, 28f, 31f, 34f, 37f, 40f, 43f, 46f, 49f } },
		{ 10406, new float[] { 24f, 27f, 30f, 33f, 36f, 39f, 42f, 45f, 48f, 51f } },
		{ 10407, new float[] { 25f, 28f, 31f, 34f, 37f } },
		{ 10408, new float[] { 25f, 28f, 31f, 34f, 37f } },
		{ 10410, new float[] { 25f, 28f, 31f, 34f, 37f, 40f, 43f, 46f, 49f, 52f } },
		{ 10411, new float[] { 19f, 21f, 24f, 26f, 29f, 31f, 34f, 36f, 39f, 41f, 44f, 46f, 49f, 51f, 54f } },
		{ 10501, new float[] { 23f, 25f, 28f, 31f, 34f, 37f, 39f, 42f, 45f, 48f, 51f, 53f, 56f, 59f, 62f } },
		{ 10502, new float[] { 22f, 25f, 28f, 31f, 34f, 37f, 40f, 43f, 46f, 49f, 52f, 55f, 58f, 61f, 64f } },
		{ 10503, new float[] { 24f, 27f, 30f, 33f, 36f, 39f, 42f, 45f, 48f, 51f, 55f, 58f, 61f, 64f, 67f } },
		{ 10504, new float[] { 23f, 26f, 29f, 32f, 35f, 38f, 41f, 44f, 47f, 50f, 53f, 56f, 59f, 62f, 65f } },
		{ 10505, new float[] { 25f, 28f, 31f, 34f, 37f, 41f, 44f, 47f, 50f, 53f } },
		{ 10506, new float[] { 26f, 29f, 32f, 36f, 39f } },
		{ 10507, new float[] { 17f, 19f, 22f, 24f, 27f, 30f, 32f, 35f, 37f, 40f, 43f, 45f, 48f, 50f, 53f } },
		{ 10508, new float[] { 24f, 27f, 30f, 33f, 36f, 40f, 43f, 46f, 49f, 52f } },
		{ 10509, new float[] { 24f, 27f, 30f, 33f, 36f } },
		{ 10601, new float[] { 22f, 24f, 27f, 30f, 33f, 36f, 38f, 41f, 44f, 47f, 50f, 52f, 55f, 58f, 61f } },
		{ 10602, new float[] { 24f, 27f, 30f, 33f, 36f, 39f, 42f, 45f, 48f, 51f, 54f, 57f, 60f, 63f, 66f } },
		{ 10603, new float[] { 23f, 26f, 29f, 32f, 35f, 39f, 42f, 45f, 48f, 51f, 55f, 58f, 61f, 64f, 67f } },
		{ 10604, new float[] { 22f, 25f, 28f, 31f, 34f, 37f, 40f, 43f, 46f, 49f, 53f, 56f, 59f, 62f, 65f } },
		{ 10605, new float[] { 25f, 28f, 31f, 34f, 38f, 41f, 44f, 48f, 51f, 54f } },
		{ 10606, new float[] { 26f, 29f, 33f, 36f, 40f } },
		{ 10901, new float[] { 35f, 39f, 43f, 47f, 51f, 55f, 59f, 63f, 67f, 71f, 75f, 79f, 83f, 87f, 91f } },
		{ 10902, new float[] { 36f, 40f, 44f, 48f, 52f, 57f, 61f, 65f, 69f, 73f, 78f, 82f, 86f, 90f, 94f } },
		{ 10903, new float[] { 36f, 40f, 44f, 48f, 52f, 57f, 61f, 65f, 69f, 73f, 78f, 82f, 86f, 90f, 94f } },
		{ 10904, new float[] { 37f, 41f, 45f, 50f, 54f, 59f, 63f, 67f, 72f, 76f, 81f, 85f, 89f, 94f, 98f } },
		{ 10905, new float[] { 37f, 41f, 45f, 50f, 54f, 59f, 63f, 67f, 72f, 76f, 81f, 85f, 89f, 94f, 98f } },
		{ 10906, new float[] { 38f, 42f, 47f, 51f, 56f, 60f, 65f, 69f, 74f, 78f, 83f, 87f, 92f, 96f, 101f } },
		{ 10907, new float[] { 40f, 44f, 49f, 53f, 58f, 62f, 67f, 71f, 76f, 80f, 85f, 89f, 94f, 98f, 103f } },
		{ 10908, new float[] { 28f, 31f, 35f, 38f, 42f, 45f, 49f, 52f, 56f, 59f, 63f, 66f, 70f, 73f, 77f } },
		{ 10910, new float[] { 26f, 29f, 32f, 35f, 39f, 42f, 45f, 49f, 52f, 55f, 59f, 62f, 65f, 68f, 72f } },
		{ 11001, new float[] { 36f, 40f, 44f, 48f, 53f, 57f, 61f, 66f, 70f, 74f, 79f, 83f, 87f, 91f, 96f } },
		{ 11002, new float[] { 37f, 41f, 46f, 50f, 55f, 59f, 64f, 68f, 73f, 77f, 82f, 86f, 91f, 95f, 100f } },
		{ 11003, new float[] { 35f, 39f, 44f, 48f, 53f, 57f, 62f, 66f, 71f, 75f, 80f, 84f, 89f, 93f, 98f } },
		{ 11004, new float[] { 40f, 45f, 50f, 55f, 60f, 65f, 70f, 75f, 80f, 85f, 90f, 95f, 100f, 105f, 110f } },
		{ 11005, new float[] { 42f, 47f, 52f, 57f, 62f, 68f, 73f, 78f, 83f, 88f, 94f, 99f, 104f, 109f, 114f } },
		{ 11006, new float[] { 42f, 48f, 54f, 60f, 66f, 72f, 78f, 84f, 90f, 96f } },
		{ 11007, new float[] { 45f, 53f, 61f, 69f, 77f, 85f, 93f, 101f, 109f, 117f } },
		{ 11008, new float[] { 8f, 51f, 94f, 137f, 180f, 223f, 266f, 309f, 352f, 395f } },
		{ 11101, new float[] { 35f, 40f, 45f, 50f, 55f, 61f, 66f, 71f, 76f, 81f } },
		{ 11102, new float[] { 30f, 33f, 36f, 39f, 42f, 46f, 49f, 52f, 55f, 58f, 62f, 65f, 68f, 71f, 74f } },
		{ 11103, new float[] { 42f, 47f, 53f, 59f, 65f, 71f, 77f, 83f, 89f, 95f, 101f, 106f, 112f, 118f, 124f } },
		{ 11104, new float[] { 31f, 35f, 39f, 43f, 47f, 51f, 55f, 59f, 63f, 67f, 72f, 76f, 80f, 84f, 88f } },
		{ 11105, new float[] { 43f, 47f, 52f, 57f, 62f, 67f, 72f, 77f, 82f, 87f, 92f, 96f, 101f, 106f, 111f } },
		{ 11106, new float[] { 46f, 52f, 58f, 64f, 70f } },
		{ 11107, new float[] { 30f, 34f, 38f, 42f, 46f, 51f, 55f, 59f, 63f, 67f, 72f, 76f, 80f, 84f, 88f } },
		{ 11301, new float[] { 24f, 27f, 30f, 33f, 36f, 39f, 42f, 45f, 48f, 51f, 55f, 58f, 61f, 64f, 67f } },
		{ 11302, new float[] { 24f, 27f, 30f, 33f, 36f, 39f, 42f, 45f, 48f, 51f, 55f, 58f, 61f, 64f, 67f } },
		{ 11401, new float[] { 48f, 55f, 62f, 69f, 76f, 84f, 91f, 98f, 105f, 112f, 120f, 127f, 134f, 141f, 148f } },
		{ 11402, new float[] { 52f, 60f, 68f, 76f, 84f, 93f, 101f, 109f, 117f, 125f, 134f, 142f, 150f, 158f, 166f } },
		{ 11403, new float[] { 49f, 57f, 65f, 73f, 81f, 89f, 97f, 105f, 113f, 121f, 129f, 137f, 145f, 153f, 161f } },
		{ 11404, new float[] { 43f, 48f, 53f, 58f, 63f, 68f, 73f, 78f, 83f, 88f, 94f, 99f, 104f, 109f, 114f } },
		{ 11501, new float[] { 12f, 15f, 18f, 21f, 24f, 27f, 30f, 33f, 36f, 39f, 42f, 45f, 48f, 51f, 54f } },
		{ 11502, new float[] { 12f } },
		{ 11504, new float[] { 8f, 9f, 10f, 11f, 12f } },
		{ 11505, new float[] { 8f, 9f, 10f, 11f, 12f, 14f, 15f, 16f, 17f, 18f, 20f, 21f, 22f, 23f, 24f } },
		{ 20001, new float[] { 15f, 18f, 21f, 25f, 28f, 32f, 35f, 38f, 42f, 45f, 49f, 52f, 55f, 59f, 62f } },
		{ 20002, new float[] { 11f, 14f, 18f, 21f, 25f, 28f, 32f, 35f, 39f, 42f, 46f, 49f, 53f, 56f, 60f } },
		{ 20003, new float[] { 16f, 19f, 23f, 26f, 30f, 33f, 37f, 40f, 44f, 47f, 51f, 54f, 58f, 61f, 65f } },
		{ 20004, new float[] { 17f, 20f, 24f, 27f, 31f, 35f, 38f, 42f, 45f, 49f, 53f, 56f, 60f, 63f, 67f } },
		{ 20005, new float[] { 20f, 23f, 27f, 31f, 35f, 39f, 43f, 47f, 51f, 55f, 59f, 62f, 66f, 70f, 74f } },
		{ 20006, new float[] { 20f, 23f, 27f, 31f, 35f, 39f, 43f, 47f, 51f, 55f } },
		{ 20007, new float[] { 23f, 27f, 31f, 35f, 39f } },
		{ 20008, new float[] { 25f, 29f, 33f, 37f, 41f } },
		{ 20101, new float[] { 22f, 26f, 30f, 34f, 38f, 42f, 46f, 50f, 54f, 58f, 62f, 66f, 70f, 74f, 78f } },
		{ 20102, new float[] { 30f, 36f, 42f, 48f, 54f, 60f, 66f, 72f, 78f, 84f, 90f, 96f, 102f, 108f, 114f } },
		{ 20103, new float[] { 27f, 31f, 35f, 39f, 43f, 47f, 51f, 55f, 59f, 63f, 68f, 72f, 76f, 80f, 84f } },
		{ 20104, new float[] { 23f, 26f, 30f, 34f, 37f, 41f, 45f, 48f, 52f, 56f, 60f, 63f, 67f, 71f, 74f } },
		{ 20105, new float[] { 30f, 36f, 43f, 49f, 56f, 62f, 69f, 75f, 82f, 88f } },
		{ 20106, new float[] { 34f, 39f, 44f, 49f, 54f } },
		{ 20107, new float[] { 35f, 44f, 53f, 62f, 71f, 80f, 89f, 98f, 107f, 116f, 125f, 134f, 143f, 152f, 161f } },
		{ 20201, new float[] { 23f, 26f, 30f, 34f, 38f, 42f, 45f, 49f, 53f, 57f, 61f, 64f, 68f, 72f, 76f } },
		{ 20202, new float[] { 28f, 32f, 37f, 42f, 47f, 52f, 56f, 61f, 66f, 71f, 76f, 80f, 85f, 90f, 95f } },
		{ 20203, new float[] { 39f, 44f, 49f, 54f, 59f, 64f, 69f, 74f, 79f, 84f, 89f, 94f, 99f, 104f, 109f } },
		{ 20204, new float[] { 23f, 26f, 30f, 34f, 37f, 41f, 45f, 48f, 52f, 56f, 60f, 63f, 67f, 71f, 74f } },
		{ 20205, new float[] { 27f, 31f, 35f, 39f, 43f, 47f, 51f, 55f, 59f, 63f } },
		{ 20206, new float[] { 30f, 35f, 40f, 45f, 50f, 56f, 61f, 66f, 71f, 76f } },
		{ 20207, new float[] { 32f, 39f, 46f, 53f, 60f } },
		{ 20208, new float[] { 35f, 41f, 47f, 53f, 59f } },
		{ 20302, new float[] { 33f, 38f, 43f, 48f, 53f, 58f, 63f, 68f, 73f, 78f, 83f, 88f, 93f, 98f, 103f } },
		{ 20303, new float[] { 37f, 42f, 47f, 52f, 58f, 63f, 68f, 74f, 79f, 84f, 90f, 95f, 100f, 105f, 111f } },
		{ 20304, new float[] { 40f, 45f, 51f, 56f, 62f, 67f, 73f, 78f, 84f, 89f, 95f, 100f, 106f, 111f, 117f } },
		{ 20305, new float[] { 42f, 47f, 53f, 59f, 65f, 71f, 76f, 82f, 88f, 94f } },
		{ 20306, new float[] { 45f, 51f, 57f, 63f, 69f } },
		{ 20601, new float[] { 52f, 60f, 68f, 76f, 85f, 93f, 101f, 110f, 118f, 126f, 135f, 143f, 151f, 159f, 168f } },
		{ 20602, new float[] { 48f, 56f, 64f, 72f, 80f, 88f, 96f, 104f, 112f, 120f, 128f, 136f, 144f, 152f, 160f } },
		{ 20603, new float[] { 50f, 58f, 66f, 74f, 82f, 90f, 98f, 106f, 114f, 122f, 130f, 138f, 146f, 154f, 162f } },
		{ 20605, new float[] { 65f, 78f, 91f, 104f, 117f, 130f, 143f, 156f, 169f, 182f } },
		{ 20606, new float[] { 70f, 82f, 94f, 106f, 118f } },
		{ 20607, new float[] { 39f, 47f, 55f, 64f, 72f, 81f, 89f, 97f, 106f, 114f } },
		{ 20608, new float[] { 63f, 75f, 87f, 99f, 111f } },
		{ 20701, new float[] { 50f, 57f, 65f, 72f, 80f, 87f, 95f, 102f, 110f, 117f, 125f, 132f, 140f, 147f, 155f } },
		{ 20702, new float[] { 38f } },
		{ 20703, new float[] { 40f } },
		{ 20704, new float[] { 50f } },
		{ 20705, new float[] { 38f } },
		{ 20706, new float[] { 55f, 63f, 71f, 79f, 87f, 95f, 103f, 111f, 119f, 127f, 135f, 143f, 151f, 159f, 167f } },
		{ 20707, new float[] { 53f, 60f, 68f, 75f, 83f, 90f, 98f, 105f, 113f, 120f, 128f, 135f, 143f, 150f, 158f } },
		{ 20708, new float[] { 55f, 63f, 71f, 79f, 87f } },
		{ 20709, new float[] { 75f, 90f, 105f, 120f, 135f } },
		{ 20710, new float[] { 59f, 66f, 74f, 81f, 89f } },
		{ 20802, new float[] { 58f, 68f, 78f, 88f, 98f, 108f, 118f, 128f, 138f, 148f, 158f, 168f, 178f, 188f, 198f } },
		{ 20803, new float[] { 70f, 83f, 96f, 109f, 122f, 135f, 148f, 161f, 174f, 187f, 200f, 213f, 226f, 239f, 252f } },
		{ 20804, new float[] { 55f, 64f, 73f, 82f, 91f, 100f, 109f, 118f, 127f, 136f, 145f, 154f, 163f, 172f, 181f } },
		{ 20806, new float[] { 150f, 170f, 190f, 210f, 230f } },
		{ 20807, new float[] { 65f, 77f, 89f, 101f, 113f } },
		{ 20901, new float[] { 35f, 40f, 46f, 51f, 57f, 62f, 68f, 73f, 79f, 84f, 90f, 95f, 101f, 106f, 112f } },
		{ 20902, new float[] { 92f, 103f, 114f, 125f, 136f } },
		{ 20903, new float[] { 70f, 79f, 88f, 97f, 107f, 116f, 125f, 135f, 144f, 153f, 163f, 172f, 181f, 190f, 200f } },
		{ 20904, new float[] { 65f, 72f, 80f, 88f, 96f, 104f, 112f, 120f, 128f, 136f, 144f, 151f, 159f, 167f, 175f } },
		{ 20906, new float[] { 60f } },
		{ 20907, new float[] { 75f, 83f, 92f, 100f, 109f, 117f, 126f, 134f, 143f, 151f, 160f, 168f, 177f, 185f, 194f } },
		{ 20908, new float[] { 48f, 55f, 62f, 69f, 76f, 83f, 90f, 97f, 104f, 111f } },
		{ 20909, new float[] { 69f, 80f, 91f, 102f, 113f } },
		{ 21001, new float[] { 55f, 61f, 68f, 74f, 81f, 87f, 94f, 100f, 107f, 113f, 120f, 126f, 133f, 139f, 146f } },
		{ 21002, new float[] { 40f, 45f, 50f, 55f, 60f, 65f, 70f, 75f, 80f, 85f, 90f, 95f, 100f, 105f, 110f } },
		{ 21007, new float[] { 420f, 520f, 620f, 720f, 820f } },
		{ 21101, new float[] { 75f, 85f, 95f, 105f, 115f, 125f, 135f, 145f, 155f, 165f, 175f, 185f, 195f, 205f, 215f } },
		{ 21103, new float[] { 64f, 72f, 80f, 88f, 96f, 104f, 112f, 120f, 128f, 136f, 144f, 152f, 160f, 168f, 176f } },
		{ 21104, new float[] { 45f, 52f, 59f, 66f, 73f, 80f, 87f, 94f, 101f, 108f, 115f, 122f, 129f, 136f, 143f } },
		{ 21105, new float[] { 71f, 81f, 91f, 101f, 112f, 122f, 132f, 143f, 153f, 163f, 174f, 184f, 194f, 204f, 215f } },
		{ 21201, new float[] { 82f, 95f, 108f, 121f, 134f, 147f, 160f, 173f, 186f, 199f, 212f, 225f, 238f, 251f, 264f } },
		{ 21202, new float[] { 75f, 86f, 97f, 108f, 119f, 130f, 141f, 152f, 163f, 174f, 185f, 196f, 207f, 218f, 229f } },
		{ 21203, new float[] { 66f, 75f, 85f, 94f, 104f, 113f, 123f, 132f, 142f, 151f, 161f, 170f, 180f, 189f, 199f } },
		{ 21204, new float[] { 69f, 79f, 89f, 99f, 109f, 119f, 129f, 139f, 149f, 159f, 169f, 179f, 189f, 199f, 209f } },
		{ 21205, new float[] { 55f } },
		{ 21301, new float[] { 73f, 88f, 104f, 119f, 135f, 150f, 166f, 181f, 197f, 212f, 228f, 243f, 259f, 274f, 290f } },
		{ 21302, new float[] { 67f, 79f, 91f, 103f, 115f, 127f, 139f, 151f, 163f, 175f, 187f, 199f, 211f, 223f, 235f } },
		{ 21303, new float[] { 72f, 86f, 100f, 114f, 128f, 142f, 156f, 170f, 184f, 198f, 212f, 226f, 240f, 254f, 268f } },
		{ 21304, new float[] { 70f, 84f, 99f, 113f, 128f, 142f, 157f, 171f, 186f, 200f, 215f, 229f, 244f, 258f, 273f } },
		{ 21305, new float[] { 65f, 77f, 89f, 101f, 113f, 125f, 137f, 149f, 161f, 173f, 185f, 197f, 209f, 221f, 233f } },
		{ 30001, new float[] { 8f, 10f, 12f, 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 30f, 32f, 34f, 36f } },
		{ 30002, new float[] { 10f, 12f, 15f, 17f, 20f, 22f, 25f, 27f, 30f, 32f, 35f, 37f, 40f, 42f, 45f } },
		{ 30004, new float[] { 10f, 12f, 14f, 16f, 19f, 21f, 23f, 26f, 28f, 30f, 33f, 35f, 37f, 39f, 42f } },
		{ 30005, new float[] { 8f, 10f, 11f, 13f, 14f, 16f, 17f, 19f, 20f, 22f, 23f, 25f, 26f, 28f, 29f } },
		{ 30006, new float[] { 11f, 13f, 16f, 18f, 21f, 23f, 26f, 28f, 31f, 33f } },
		{ 30007, new float[] { 15f, 18f, 21f, 24f, 27f } },
		{ 30101, new float[] { 17f, 20f, 23f, 26f, 29f, 32f, 35f, 38f, 41f, 44f, 48f, 51f, 54f, 57f, 60f } },
		{ 30102, new float[] { 20f, 23f, 27f, 30f, 34f, 37f, 41f, 44f, 48f, 51f, 55f, 58f, 62f, 65f, 69f } },
		{ 30103, new float[] { 15f, 17f, 20f, 22f, 25f, 27f, 30f, 32f, 35f, 37f, 40f, 42f, 45f, 47f, 50f } },
		{ 30104, new float[] { 20f, 23f, 26f, 30f, 33f, 37f, 40f, 43f, 47f, 50f, 54f, 57f, 60f, 64f, 67f } },
		{ 30105, new float[] { 22f, 25f, 29f, 33f, 36f, 40f, 44f, 47f, 51f, 55f } },
		{ 30106, new float[] { 23f, 26f, 30f, 34f, 37f } },
		{ 30107, new float[] { 23f, 26f, 30f, 34f, 38f } },
		{ 30201, new float[] { 18f, 21f, 24f, 27f, 30f, 34f, 37f, 40f, 43f, 46f, 50f, 53f, 56f, 59f, 62f } },
		{ 30202, new float[] { 20f, 23f, 26f, 29f, 33f, 36f, 39f, 43f, 46f, 49f, 53f, 56f, 59f, 62f, 66f } },
		{ 30204, new float[] { 22f, 27f, 32f, 37f, 42f, 47f, 52f, 57f, 62f, 67f, 73f, 78f, 83f, 88f, 93f } },
		{ 30205, new float[] { 19f, 22f, 26f, 29f, 33f, 36f, 40f, 43f, 47f, 50f } },
		{ 30206, new float[] { 15f, 18f, 21f, 24f, 27f, 30f, 33f, 36f, 39f, 42f } },
		{ 30207, new float[] { 23f, 26f, 30f, 33f, 37f } },
		{ 30301, new float[] { 25f, 28f, 32f, 36f, 39f, 43f, 47f, 50f, 54f, 58f, 62f, 65f, 69f, 73f, 76f } },
		{ 30302, new float[] { 28f, 32f, 36f, 40f, 44f, 48f, 52f, 56f, 60f, 64f, 68f, 72f, 76f, 80f, 84f } },
		{ 30303, new float[] { 29f, 33f, 37f, 41f, 45f, 49f, 53f, 57f, 61f, 65f, 70f, 74f, 78f, 82f, 86f } },
		{ 30304, new float[] { 31f, 35f, 39f, 43f, 48f, 52f, 56f, 61f, 65f, 69f, 74f, 78f, 82f, 86f, 91f } },
		{ 30305, new float[] { 20f, 23f, 27f, 30f, 34f, 37f, 41f, 44f, 48f, 51f, 55f, 58f, 62f, 65f, 69f } },
		{ 30306, new float[] { 34f, 39f, 44f, 49f, 54f, 59f, 64f, 69f, 74f, 79f } },
		{ 30307, new float[] { 34f, 39f, 44f, 49f, 54f, 60f, 65f, 70f, 75f, 80f } },
		{ 30308, new float[] { 35f, 40f, 46f, 51f, 57f } },
		{ 30401, new float[] { 25f, 28f, 32f, 36f, 39f, 43f, 47f, 50f, 54f, 58f, 62f, 65f, 69f, 73f, 76f } },
		{ 30402, new float[] { 26f, 29f, 33f, 37f, 41f, 45f, 48f, 52f, 56f, 60f, 64f, 67f, 71f, 75f, 79f } },
		{ 30403, new float[] { 22f, 25f, 29f, 32f, 36f, 39f, 43f, 46f, 50f, 53f, 57f, 60f, 64f, 67f, 71f } },
		{ 30404, new float[] { 30f, 34f, 38f, 42f, 46f } },
		{ 30405, new float[] { 28f, 32f, 36f, 40f, 44f, 48f, 52f, 56f, 60f, 64f, 68f, 72f, 76f, 80f, 84f } },
		{ 30406, new float[] { 29f, 33f, 37f, 41f, 45f, 49f, 53f, 57f, 61f, 65f } },
		{ 30407, new float[] { 30f, 34f, 38f, 42f, 47f, 51f, 55f, 60f, 64f, 68f } },
		{ 30408, new float[] { 32f, 36f, 41f, 46f, 50f } },
		{ 30501, new float[] { 34f, 39f, 44f, 49f, 54f, 59f, 64f, 69f, 74f, 79f, 84f, 89f, 94f, 99f, 104f } },
		{ 30502, new float[] { 35f, 40f, 45f, 50f, 55f, 60f, 65f, 70f, 75f, 80f, 85f, 90f, 95f, 100f, 105f } },
		{ 30503, new float[] { 32f } },
		{ 30504, new float[] { 37f, 42f, 47f, 52f, 58f, 63f, 68f, 74f, 79f, 84f, 90f, 95f, 100f, 105f, 111f } },
		{ 30505, new float[] { 38f, 43f, 48f, 54f, 59f, 65f, 70f, 75f, 81f, 86f } },
		{ 30506, new float[] { 41f, 46f, 52f, 57f, 63f } },
		{ 30507, new float[] { 40f, 45f, 51f, 56f, 62f, 68f, 73f, 79f, 84f, 90f, 96f, 101f, 107f, 112f, 118f } },
		{ 30601, new float[] { 33f, 37f, 42f, 46f, 51f } },
		{ 30602, new float[] { 35f, 40f, 45f, 50f, 55f, 60f, 65f, 70f, 75f, 80f, 85f, 90f, 95f, 100f, 105f } },
		{ 30603, new float[] { 37f, 42f, 47f, 53f, 58f, 64f, 69f, 74f, 80f, 85f, 91f, 96f, 101f, 107f, 112f } },
		{ 30606, new float[] { 40f, 45f, 51f, 56f, 62f } },
		{ 30608, new float[] { 17f, 22f, 27f, 32f, 37f } },
		{ 30801, new float[] { 22f, 26f, 30f, 34f, 38f, 42f, 46f, 50f, 54f, 58f, 62f, 66f, 70f, 74f, 78f } },
		{ 30802, new float[] { 25f, 29f, 33f, 37f, 41f, 46f, 50f, 54f, 58f, 62f, 67f, 71f, 75f, 79f, 83f } },
		{ 30803, new float[] { 25f, 29f, 33f, 37f, 41f, 46f, 50f, 54f, 58f, 62f, 67f, 71f, 75f, 79f, 83f } },
		{ 30804, new float[] { 27f, 32f, 36f, 41f, 45f, 50f, 54f, 59f, 63f, 68f, 72f, 77f, 81f, 86f, 90f } },
		{ 30805, new float[] { 30f, 34f, 39f, 43f, 48f, 53f, 57f, 62f, 66f, 71f } },
		{ 30806, new float[] { 30f, 35f, 40f, 45f, 50f } },
		{ 31001, new float[] { 20f } },
		{ 31002, new float[] { 27f } },
		{ 31003, new float[] { 35f, 40f, 45f, 50f, 55f, 60f, 65f, 70f, 75f, 80f, 85f, 90f, 95f, 100f, 105f } },
		{ 31004, new float[] { 34f, 38f, 43f, 48f, 53f, 58f, 63f, 68f, 73f, 78f, 83f, 87f, 92f, 97f, 102f } },
		{ 31005, new float[] { 38f, 43f, 48f, 53f, 58f, 64f, 69f, 74f, 79f, 84f, 90f, 95f, 100f, 105f, 110f } },
		{ 31006, new float[] { 40f, 45f, 51f, 57f, 63f, 69f, 74f, 80f, 86f, 92f, 98f, 103f, 109f, 115f, 121f } },
		{ 31008, new float[] { 45f, 52f, 59f, 66f, 73f, 80f, 87f, 94f, 101f, 108f } },
		{ 31101, new float[] { 52f, 60f, 68f, 76f, 84f, 92f, 100f, 108f, 116f, 124f, 132f, 140f, 148f, 156f, 164f } },
		{ 31102, new float[] { 53f, 61f, 70f, 78f, 87f, 95f, 104f, 112f, 121f, 129f, 138f, 146f, 155f, 163f, 172f } },
		{ 31103, new float[] { 65f, 76f, 87f, 98f, 109f, 120f, 131f, 142f, 153f, 164f, 175f, 186f, 197f, 208f, 219f } },
		{ 31104, new float[] { 59f, 68f, 77f, 86f, 95f, 104f, 113f, 122f, 131f, 140f, 149f, 158f, 167f, 176f, 185f } },
		{ 31201, new float[] { 55f, 63f, 72f, 80f, 89f, 97f, 106f, 114f, 123f, 131f, 140f, 148f, 157f, 165f, 174f } },
		{ 31202, new float[] { 51f, 58f, 66f, 74f, 82f, 90f, 97f, 105f, 113f, 121f, 129f, 136f, 144f, 152f, 160f } },
		{ 31203, new float[] { 60f, 70f, 80f, 90f, 100f, 110f, 120f, 130f, 140f, 150f, 160f, 170f, 180f, 190f, 200f } },
		{ 31204, new float[] { 56f, 64f, 73f, 82f, 91f, 100f, 109f, 118f, 127f, 136f, 145f, 153f, 162f, 171f, 180f } },
		{ 31205, new float[] { 39f, 45f, 51f, 57f, 63f, 69f, 75f, 81f, 87f, 93f, 99f, 105f, 111f, 117f, 123f } },
		{ 31206, new float[] { 41f, 47f, 54f, 60f, 67f, 73f, 80f, 86f, 93f, 99f, 106f, 112f, 119f, 125f, 132f } },
		{ 39008, new float[] { 5f, 7f, 9f, 11f, 13f, 15f, 17f, 19f, 21f, 23f, 25f, 27f, 29f, 31f, 33f } },
		{ 39010, new float[] { 5f, 7f, 9f, 11f, 13f, 15f, 17f, 19f, 21f, 23f, 25f, 27f, 29f, 31f, 33f } },
		{ 40001, new float[] { 12f, 15f, 18f, 21f, 24f, 27f, 30f, 33f, 36f, 39f, 42f, 45f, 48f, 51f, 54f } },
		{ 40002, new float[] { 13f, 16f, 19f, 22f, 26f, 29f, 32f, 36f, 39f, 42f, 46f, 49f, 52f, 55f, 59f } },
		{ 40003, new float[] { 16f, 19f, 23f, 26f, 30f, 33f, 37f, 40f, 44f, 47f, 51f, 54f, 58f, 61f, 65f } },
		{ 40004, new float[] { 15f, 18f, 21f, 24f, 28f, 31f, 34f, 38f, 41f, 44f, 48f, 51f, 54f, 57f, 61f } },
		{ 40006, new float[] { 20f, 24f, 28f, 32f, 36f, 40f, 44f, 48f, 52f, 56f } },
		{ 40007, new float[] { 22f, 26f, 30f, 34f, 38f } },
		{ 40101, new float[] { 24f, 28f, 33f, 37f, 42f, 46f, 51f, 55f, 60f, 64f, 69f, 73f, 78f, 82f, 87f } },
		{ 40102, new float[] { 22f, 26f, 30f, 34f, 38f, 42f, 46f, 50f, 54f, 58f, 62f, 66f, 70f, 74f, 78f } },
		{ 40103, new float[] { 20f, 23f, 27f, 31f, 34f, 38f, 42f, 45f, 49f, 53f, 57f, 60f, 64f, 68f, 71f } },
		{ 40104, new float[] { 25f, 29f, 34f, 38f, 43f, 47f, 52f, 56f, 61f, 65f, 70f, 74f, 79f, 83f, 88f } },
		{ 40105, new float[] { 22f, 26f, 30f, 34f, 38f, 43f, 47f, 51f, 55f, 59f } },
		{ 40201, new float[] { 22f, 26f, 30f, 34f, 38f, 42f, 46f, 50f, 54f, 58f, 62f, 66f, 70f, 74f, 78f } },
		{ 40202, new float[] { 23f, 27f, 31f, 35f, 40f, 44f, 48f, 53f, 57f, 61f, 66f, 70f, 74f, 78f, 83f } },
		{ 40203, new float[] { 26f, 30f, 35f, 39f, 44f, 48f, 53f, 57f, 62f, 66f, 71f, 75f, 80f, 84f, 89f } },
		{ 40204, new float[] { 30f, 35f, 40f, 45f, 50f } },
		{ 40205, new float[] { 32f, 37f, 42f, 47f, 52f, 58f, 63f, 68f, 73f, 78f } },
		{ 40206, new float[] { 33f, 38f, 43f, 48f, 53f, 59f, 64f, 69f, 74f, 79f } },
		{ 40207, new float[] { 30f, 35f, 41f, 46f, 52f } },
		{ 40209, new float[] { 30f, 34f, 39f, 44f, 49f, 54f, 59f, 64f, 69f, 74f } },
		{ 40401, new float[] { 22f, 27f, 32f, 37f, 42f, 47f, 52f, 57f, 62f, 67f, 72f, 77f, 82f, 87f, 92f } },
		{ 40402, new float[] { 33f, 38f, 44f, 49f, 55f, 60f, 66f, 71f, 77f, 82f, 88f, 93f, 99f, 104f, 110f } },
		{ 40403, new float[] { 33f, 38f, 44f, 49f, 55f, 60f, 66f, 71f, 77f, 82f, 88f, 93f, 99f, 104f, 110f } },
		{ 40404, new float[] { 29f, 33f, 38f, 43f, 48f, 53f, 58f, 63f, 68f, 73f, 78f, 82f, 87f, 92f, 97f } },
		{ 40405, new float[] { 37f, 42f, 48f, 54f, 59f, 65f, 71f, 76f, 82f, 88f } },
		{ 40406, new float[] { 39f, 44f, 50f, 56f, 61f, 67f, 73f, 78f, 84f, 90f } },
		{ 40407, new float[] { 40f, 46f, 52f, 58f, 64f } },
		{ 40502, new float[] { 20f } },
		{ 40504, new float[] { 45f, 51f, 57f, 63f, 69f, 76f, 82f, 88f, 94f, 100f, 107f, 113f, 119f, 125f, 131f } },
		{ 40505, new float[] { 35f, 40f, 46f, 51f, 57f, 62f, 68f, 73f, 79f, 84f } },
		{ 40506, new float[] { 37f, 42f, 48f, 54f, 59f } },
		{ 40602, new float[] { 35f, 40f, 46f, 51f, 57f, 63f, 68f, 74f, 79f, 85f, 91f, 96f, 102f, 107f, 113f } },
		{ 40603, new float[] { 37f, 42f, 48f, 54f, 59f, 65f, 71f, 76f, 82f, 88f, 94f, 99f, 105f, 111f, 116f } },
		{ 40605, new float[] { 40f, 46f, 52f, 58f, 64f, 71f, 77f, 83f, 89f, 95f } },
		{ 40606, new float[] { 45f, 51f, 58f, 65f, 71f } },
		{ 40607, new float[] { 41f, 47f, 53f, 59f, 65f } },
		{ 40701, new float[] { 35f, 40f, 45f, 50f, 55f, 60f, 65f, 70f, 75f, 80f, 85f, 90f, 95f, 100f, 105f } },
		{ 40703, new float[] { 30f, 34f, 39f, 43f, 48f, 52f, 57f, 61f, 66f, 70f, 75f, 79f, 84f, 88f, 93f } },
		{ 40704, new float[] { 28f, 32f, 36f, 40f, 44f, 49f, 53f, 57f, 61f, 65f, 70f, 74f, 78f, 82f, 86f } },
		{ 40705, new float[] { 32f, 36f, 41f, 46f, 50f, 55f, 60f, 64f, 69f, 74f } },
		{ 40707, new float[] { 35f, 40f, 45f, 50f, 55f, 60f, 65f, 70f, 75f, 80f } },
		{ 40708, new float[] { 40f, 46f, 52f, 58f, 64f } },
		{ 40802, new float[] { 45f, 52f, 59f, 66f, 73f, 80f, 87f, 94f, 101f, 108f, 115f, 122f, 129f, 136f, 143f } },
		{ 40803, new float[] { 43f, 49f, 56f, 62f, 69f, 75f, 82f, 88f, 95f, 101f, 108f, 114f, 121f, 127f, 134f } },
		{ 40808, new float[] { 45f, 52f, 59f, 66f, 73f, 80f, 87f, 94f, 101f, 108f, 115f, 122f, 129f, 136f, 143f } },
		{ 40809, new float[] { 35f, 40f, 45f, 50f, 55f } },
		{ 40901, new float[] { 55f, 63f, 71f, 79f, 87f, 96f, 104f, 112f, 120f, 128f, 137f, 145f, 153f, 161f, 169f } },
		{ 40902, new float[] { 45f, 52f, 59f, 66f, 73f, 80f, 87f, 94f, 101f, 108f, 115f, 122f, 129f, 136f, 143f } },
		{ 40903, new float[] { 47f, 54f, 61f, 68f, 75f, 83f, 90f, 97f, 104f, 111f, 119f, 126f, 133f, 140f, 147f } },
		{ 40904, new float[] { 47f, 54f, 61f, 68f, 75f, 83f, 90f, 97f, 104f, 111f } },
		{ 40905, new float[] { 45f, 52f, 59f, 66f, 73f, 80f, 87f, 94f, 101f, 108f } },
		{ 40906, new float[] { 52f, 60f, 68f, 76f, 84f, 92f, 100f, 108f, 116f, 124f, 132f, 140f, 148f, 156f, 164f } },
		{ 41002, new float[] { 53f, 61f, 69f, 77f, 86f, 94f, 102f, 111f, 119f, 127f, 136f, 144f, 152f, 160f, 169f } },
		{ 41003, new float[] { 56f, 64f, 73f, 81f, 90f, 98f, 107f, 115f, 124f, 132f, 141f, 149f, 158f, 166f, 175f } },
		{ 41004, new float[] { 55f } },
		{ 41005, new float[] { 65f, 75f, 85f, 95f, 105f, 115f, 125f, 135f, 145f, 155f, 165f, 175f, 185f, 195f, 205f } },
		{ 41006, new float[] { 53f } },
		{ 41007, new float[] { 58f } },
		{ 41008, new float[] { 60f, 69f, 78f, 87f, 96f, 106f, 115f, 124f, 133f, 142f } },
		{ 41009, new float[] { 65f, 75f, 85f, 95f, 105f, 115f, 125f, 135f, 145f, 155f, 165f, 175f, 185f, 195f, 205f } },
		{ 41101, new float[] { 72f, 86f, 100f, 114f, 128f, 142f, 156f, 170f, 184f, 198f, 212f, 226f, 240f, 254f, 268f } },
		{ 41102, new float[] { 62f, 73f, 84f, 96f, 107f, 119f, 130f, 141f, 153f, 164f, 176f, 187f, 198f, 210f, 221f } },
		{ 41103, new float[] { 67f, 79f, 91f, 103f, 115f } },
		{ 41104, new float[] { 60f, 70f, 80f, 90f, 100f, 110f, 120f, 130f, 140f, 150f, 160f, 170f, 180f, 190f, 200f } },
		{ 41105, new float[] { 62f, 73f, 84f, 95f, 106f, 117f, 128f, 139f, 150f, 161f, 172f, 183f, 194f, 205f, 216f } },
		{ 41106, new float[] { 58f, 67f, 76f, 85f, 94f, 103f, 112f, 121f, 130f, 139f, 148f, 157f, 166f, 175f, 184f } },
		{ 41201, new float[] { 59f, 68f, 78f, 87f, 97f, 106f, 116f, 125f, 135f, 144f, 154f, 163f, 173f, 182f, 192f } },
		{ 41203, new float[] { 62f, 73f, 84f, 95f, 107f, 118f, 129f, 141f, 152f, 163f, 175f, 186f, 197f, 208f, 220f } },
		{ 41204, new float[] { 75f, 89f, 103f, 117f, 131f } },
		{ 41205, new float[] { 45f, 51f, 57f, 63f, 69f, 75f, 81f, 87f, 93f, 99f, 105f, 111f, 117f, 123f, 129f } },
		{ 41206, new float[] { 45f, 51f, 57f, 63f, 69f, 75f, 81f, 87f, 93f, 99f, 105f, 111f, 117f, 123f, 129f } },
		{ 41208, new float[] { 55f, 63f, 71f, 79f, 87f } },
		{ 41301, new float[] { 42f, 48f, 54f, 60f, 66f, 72f, 78f, 84f, 90f, 96f, 102f, 108f, 114f, 120f, 126f } },
		{ 41302, new float[] { 55f, 63f, 71f, 80f, 88f, 97f, 105f, 113f, 122f, 130f, 139f, 147f, 155f, 164f, 172f } },
		{ 41303, new float[] { 45f, 51f, 57f, 64f, 70f, 77f, 83f, 89f, 96f, 102f, 109f, 115f, 121f, 128f, 134f } },
		{ 41304, new float[] { 47f, 54f, 61f, 68f, 75f, 82f, 89f, 96f, 103f, 110f, 117f, 124f, 131f, 138f, 145f } }
	};
}
