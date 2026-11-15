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
