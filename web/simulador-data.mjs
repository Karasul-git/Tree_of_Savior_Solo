// Monta os dados do simulador a partir dos arquivos do projeto.
import { readFileSync } from 'node:fs';
import path from 'node:path';

export function simulatorData(root) {
  const read = file => JSON.parse(readFileSync(path.join(root, file), 'utf8'));
  const config = read('data/simulator.json');
  const equipment = read('data/proposals/equipment-curve.json');
  const points = read('data/proposals/points-per-level.json');
  const armor = {};
  for (const [kind, set] of Object.entries(equipment.equipment_curve.armor_def_grade01)) {
    armor[kind] = { body: set.body, hands: set.hands };
  }
  return {
    reviewed_at: config.reviewed_at,
    constants: config.constants,
    bases: config.bases,
    weapon_families: config.weapon_families,
    presets: config.presets,
    investment_bonus_ranges: points.attribute_points.investment_bonus_ranges,
    tiers: equipment.weapon_curves.tiers,
    weapons: equipment.weapon_curves.avg_atk_median,
    armor,
    shield: equipment.equipment_curve.shield_def.grade01,
    monsters: equipment.monster_multipliers.table.map(row => ({
      level: row.level, hp: row.target_hp, atk: row.target_atk, def: row.target_def, hit: row.target_hit,
    })),
  };
}
