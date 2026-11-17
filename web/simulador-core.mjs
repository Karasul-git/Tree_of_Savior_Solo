// Fórmulas do simulador de builds. Funções puras, sem acesso à página,
// para o navegador e para os testes usarem o mesmo código.
// Regras: Melia (system/scripts/zone/core) e decisões do projeto; ver
// docs/projeto-solo/todas-as-formulas.md. Tudo é proposta de balanceamento.

export const ATTRIBUTES = ['str', 'con', 'int', 'spr', 'dex'];

// Valor de uma curva por nível, interpolado entre as faixas da tabela.
// Faixas sem dado (null) são ignoradas; fora do intervalo usa a ponta.
export function interpolate(tiers, values, level) {
  const points = tiers.map((t, i) => [t, values[i]]).filter(([, v]) => typeof v === 'number');
  if (!points.length) return null;
  if (level <= points[0][0]) return points[0][1];
  const last = points[points.length - 1];
  if (level >= last[0]) return last[1];
  for (let i = 1; i < points.length; i += 1) {
    const [x1, y1] = points[i];
    if (level <= x1) {
      const [x0, y0] = points[i - 1];
      return y0 + ((y1 - y0) * (level - x0)) / (x1 - x0);
    }
  }
  return last[1];
}

// Bônus por investimento: a cada faixa de pontos gastos num atributo,
// soma-se ⌊pontos da faixa ÷ intervalo⌋ (cada faixa é arredondada à parte).
export function investmentBonus(invested, ranges) {
  let bonus = 0;
  let from = 0;
  for (const { up_to: upTo, every } of ranges) {
    if (invested <= from) break;
    bonus += Math.floor((Math.min(invested, upTo) - from) / every);
    from = upTo;
  }
  return bonus;
}

// Distribui o total entre os atributos, mantendo as proporções
// (maior resto), para o total fechar exatamente.
export function distribute(weights, total) {
  const sum = ATTRIBUTES.reduce((s, k) => s + (weights[k] || 0), 0);
  const out = Object.fromEntries(ATTRIBUTES.map(k => [k, 0]));
  if (total <= 0) return out;
  if (sum <= 0) { out.str = total; return out; }
  const parts = ATTRIBUTES.map(k => {
    const exact = ((weights[k] || 0) * total) / sum;
    return { k, floor: Math.floor(exact), rest: exact - Math.floor(exact) };
  });
  let left = total - parts.reduce((s, p) => s + p.floor, 0);
  parts.sort((a, b) => b.rest - a.rest);
  for (const p of parts) { out[p.k] = p.floor + (left > 0 ? 1 : 0); if (left > 0) left -= 1; }
  return out;
}

const lerpRow = (cfg, tiers, row, level) => interpolate(tiers, row, level) ?? 0;

export function weaponAttack(data, weaponId, gearLevel) {
  const row = data.weapons[weaponId];
  if (!row) return 0;
  return lerpRow(data, data.tiers, row, gearLevel);
}

export function armorDefense(data, armor, gearLevel) {
  const set = data.armor[armor];
  if (!set) return 0;
  // Pernas repetem o corpo e pés repetem as mãos, como no couro do Melia.
  const body = lerpRow(data, data.tiers, set.body, gearLevel);
  const hands = lerpRow(data, data.tiers, set.hands, gearLevel);
  return 2 * (body + hands);
}

export function shieldDefense(data, gearLevel) {
  return lerpRow(data, data.tiers, data.shield, gearLevel);
}

export function monsterAt(data, level) {
  const table = data.monsters;
  const min = data.constants.monster_reference_min_level;
  if (level < min) return null;
  const levels = table.map(row => row.level);
  const pick = key => interpolate(levels, table.map(row => row[key]), level);
  return { level, hp: pick('hp'), atk: pick('atk'), def: pick('def'), hit: pick('hit') };
}

// Fração do golpe que a defesa corta: defesa ÷ (defesa + ataque), com teto.
export function reduction(defense, attack, capPct) {
  if (defense <= 0 || attack <= 0) return 0;
  return Math.min(capPct / 100, defense / (defense + attack));
}

export function skillFactor(constants, skillLevel) {
  const base = constants.skill_factor_lv1_pct;
  return base + base * constants.skill_factor_growth_per_level * (skillLevel - 1);
}

// build: { base, level, points{str,con,int,spr,dex}, main, off, armor, gearLevel, weaponGrade, skillLevel }
export function compute(data, build) {
  const c = data.constants;
  const base = data.bases[build.base];
  const level = build.level;
  const stat = {};
  const bonus = {};
  for (const k of ATTRIBUTES) {
    const invested = build.points[k] || 0;
    bonus[k] = investmentBonus(invested, data.investment_bonus_ranges);
    stat[k] = base.stats[k] + invested + bonus[k];
  }
  const grade = build.weaponGrade === 2 ? '_02' : '_01';
  const mainId = build.main + grade;
  const mainAtk = weaponAttack(data, mainId, build.gearLevel);
  const offIsWeapon = build.off && build.off !== 'none' && build.off !== 'shield';
  const offAtk = offIsWeapon ? weaponAttack(data, build.off + grade, build.gearLevel) : 0;
  const weaponTotal = mainAtk + c.offhand_attack_share * offAtk;

  const strPart = 2 * stat.str + Math.floor(stat.str / 10) * (level * 0.05);
  const intPart = 2 * stat.int + Math.floor(stat.int / 10) * (level * 0.05);
  const attack = 20 + level + strPart + weaponTotal;
  const prepAttack = 20 + level + intPart + mainAtk;

  const hpBase = Math.floor(400 * base.hp_rate) + Math.floor((level - 1) * 80 * base.hp_rate);
  const hpStat = Math.floor((stat.con * 0.003 + Math.floor(stat.con / 10) * 0.01) * hpBase);
  const hp = hpBase + hpStat;
  const spBase = Math.floor(200 * base.sp_rate) + Math.floor((level - 1) * 18 * base.sp_rate);
  const spStat = Math.floor((stat.spr * 0.005 + Math.floor(stat.spr / 10) * 0.015) * spBase);
  const sp = spBase + spStat;

  const armorDef = armorDefense(data, build.armor, build.gearLevel);
  const shieldDef = build.off === 'shield' ? shieldDefense(data, build.gearLevel) : 0;
  const defense = 20 + level + armorDef + shieldDef + c.con_defense_per_point * stat.con;
  const accuracy = level / 4 + stat.str / 2 + Math.floor(stat.str / 15) * 3;
  const dodge = level / 4 + stat.dex / 2 + Math.floor(stat.dex / 15) * 3;
  const out = {
    stat, bonus, hp, sp, attack, prepAttack, defense, accuracy, dodge,
    critAttack: 4 * stat.dex + Math.floor(stat.dex / 10) * 10,
    critRate: level / 2,
    healBonusPct: stat.int,
    stamina: 25 + Math.floor(stat.con / 20),
    weight: 8000 + 5 * stat.str + 5 * stat.con,
    weaponAttack: weaponTotal,
    pointsUsed: ATTRIBUTES.reduce((s, k) => s + (build.points[k] || 0), 0),
    pointsTotal: (level - 1) * c.points_per_level,
  };

  const monster = monsterAt(data, level);
  out.monster = monster;
  if (monster) {
    const factor = skillFactor(c, build.skillLevel) / 100;
    const dealt = reduction(monster.def, attack, c.defense_cap_pct);
    const skillDamage = attack * (1 - dealt) * factor;
    const taken = reduction(defense, monster.atk, c.defense_cap_pct);
    const hit = monster.atk * (1 - taken);
    const dodgeChance = dodge > monster.hit ? Math.min(c.dodge_cap_pct / 100, Math.pow(dodge - monster.hit, c.dodge_exponent) / 100) : 0;
    out.fight = {
      skillFactorPct: factor * 100,
      monsterReductionPct: dealt * 100,
      skillDamage,
      skillsToKill: monster.hp / skillDamage,
      playerReductionPct: taken * 100,
      hitDamage: hit,
      hitPctHp: (hit / hp) * 100,
      dodgePct: dodgeChance * 100,
      hitsToDie: hp / hit / (1 - dodgeChance),
    };
  }
  return out;
}
