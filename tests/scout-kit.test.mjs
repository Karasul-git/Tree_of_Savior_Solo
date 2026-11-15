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
  assert.equal(kit.status, 'proposal-not-ratified');
});
