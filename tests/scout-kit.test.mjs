import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url)));
const kit = read('data/proposals/scout-base-kit.json');
const scout2016 = read('data/catalog/classes-2016.json').classes.find(c => c.id === 'scout');
const later = read('data/research/later-reuse-options.json').variants.find(v => v.class_id === 'scout');
const sources = new Set(read('data/sources.json').map(s => s.id));

test('o kit base de Scout só reaproveita habilidades existentes, sem criar novas', () => {
  const historical = new Set(scout2016.skills.map(s => s.name));
  const posterior = new Set(later.skills.map(s => s.name));
  for (const skill of kit.proposed_kit) {
    assert.ok(historical.has(skill.name) || posterior.has(skill.name), skill.name);
    assert.ok(sources.has(skill.source_id), skill.source_id);
    assert.ok([1, 2, 3].includes(skill.proposed_circle));
  }
  const used = new Set([...kit.proposed_kit, ...kit.left_in_history].map(s => s.name));
  for (const name of historical) assert.ok(used.has(name), `habilidade de 2016 sem destino: ${name}`);
  for (const name of posterior) assert.ok(used.has(name), `habilidade posterior sem destino: ${name}`);
});

test('a proposta não altera a ficha histórica de Scout', () => {
  assert.equal(scout2016.tree, 'Archer');
  assert.equal(scout2016.first_rank, 4);
  assert.deepEqual(scout2016.skills.map(s => [s.name, s.circle]), [
    ['Camouflage', 1], ['Flu Flu', 1], ['Flare Shot', 1], ['Cloaking', 1],
    ['Perspective Distortion', 2], ['Scan', 3], ['Split Arrow', 3],
  ]);
  assert.equal(kit.status, 'ratified-design');
  assert.equal(kit.ratified_option, 'B');
});

test('a especificação de bots registra as decisões do proprietário sem dar o limite de 2016 por confirmado', () => {
  const bots = read('data/proposals/bots-spec.json');
  assert.equal(bots.origin.decision, 'both');
  assert.equal(bots.party.max_bots, 3);
  assert.equal(bots.party.max_members, 4);
  assert.equal(bots.party.historical_party_limit_verified, false);
  assert.equal(bots.control.model, 'stances-and-simple-orders');
  assert.equal(bots.progression.model, 'shared-experience');
});

test('a tabela da fase 0 cobre as 39 classes solo com ranks e círculos coerentes', () => {
  const map = read('data/proposals/solo-jobs-melia.json');
  const solo = read('data/catalog/classes-solo.json');
  assert.equal(map.classes.length, 39);
  for (const base of solo.bases) {
    for (const c of base.classes) {
      const row = map.classes.find(r => r.class_id === c.class_id);
      assert.ok(row, c.class_id);
      assert.equal(row.solo_rank, c.solo_rank);
      assert.equal(row.solo_base, base.id);
    }
  }
  for (const row of map.classes) assert.equal(row.max_circle_at_r7, Math.min(3, 8 - row.solo_rank));
  assert.deepEqual(map.classes.filter(r => r.changes_needed.includes('tree')).map(r => r.class_id).sort(),
    ['alchemist', 'monk', 'paladin', 'plague-doctor']);
});

test('os pontos por nível registram as decisões do proprietário e somam corretamente', () => {
  const pts = read('data/proposals/points-per-level.json');
  assert.deepEqual(pts.decisions, { int: 'repurpose', investment_bonus: 'keep', extra_points: 'relocate' });
  assert.equal(pts.attribute_points.from_levels, pts.attribute_points.max_level - 1);
  assert.equal(pts.skill_points.total_at_r7, 7 * pts.skill_points.class_levels_per_rank * pts.skill_points.per_class_level);
  assert.equal(pts.examples.bonus_for_150, 35);
  assert.equal(pts.int_role.decision, 'preparation-potency');
  assert.equal(pts.attribute_names.SPR.project_name_pt, 'Sabedoria');
});

test('o levantamento de magia só aponta classes da árvore solo e soma 26 habilidades', () => {
  const magic = read('data/proposals/magic-skills-int.json');
  const map = read('data/proposals/solo-jobs-melia.json');
  const ids = new Set(map.classes.map(c => c.class_id));
  assert.equal(magic.skills.length, 26);
  assert.ok(magic.skills.every(s => ids.has(s.class_id)));
  assert.deepEqual([...new Set(magic.skills.map(s => s.class_id))].sort(), ['alchemist', 'monk', 'paladin', 'plague-doctor']);
});

test('as decisões das fórmulas de dano ficam registradas', () => {
  const f = read('data/proposals/damage-formulas.json');
  assert.equal(f.decisions.preparation_healing_scales_with_int.status, 'decided');
  assert.equal(f.decisions.con_adds_defense.status, 'decided');
  assert.equal(f.decisions.dex_crit_chance.status, 'pending-source');
});

test('as metas de calibragem dos monstros seguem as decisões do proprietário', () => {
  const c = read('data/proposals/monster-calibration.json');
  assert.equal(c.targets.group_size, 4);
  assert.deepEqual(c.targets.skills_per_member_to_kill_normal, [1, 2]);
  assert.deepEqual(c.targets.damage_per_hit_pct_of_hp_non_tank, [5, 10]);
  for (const b of c.bands) assert.equal(b.target_hit_min, Math.round(b.player_hp_no_gear * 0.05));
});

test('a curva de equipamentos cresce por nível e mantém a decisão de escala pendente', () => {
  const e = read('data/proposals/equipment-curve.json');
  const sword = Object.values(e.equipment_curve.one_handed_sword_avg_atk.grade02);
  for (let i = 1; i < sword.length; i++) assert.ok(sword[i] > sword[i - 1]);
  assert.equal(e.status, 'proposal-pending-skill-scale-decision');
  assert.ok(e.results.every(r => r.mult_hp_F150 < r.mult_hp_F2000));
});
