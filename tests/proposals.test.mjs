import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import { treeRankCoverage, readTreeState, branchEntries } from '../web/trees.mjs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url)));
const catalog = read('data/catalog/classes-2016.json');
catalog.later_classes = read('data/catalog/classes-later.json').classes;
const solo = read('data/catalog/classes-solo.json');
const proposal = read('data/proposals/six-classes-solo.json');

test('os seis pares de cada base ficam completos e o rank histórico de Paladin permanece intacto', () => {
  for (const base of solo.bases) assert.deepEqual(treeRankCoverage(catalog, base).map(r => r.count), [2, 2, 2, 2, 2, 2]);
  assert.equal(catalog.classes.find(c => c.id === 'paladin').first_rank, 4);
  const position = solo.bases[0].classes.find(c => c.class_id === 'paladin');
  assert.equal(position.solo_rank, 5);
  assert.equal(position.rank_status, 'proposed');
  const state = readTreeState('?view=solo&base=archer&rank=5&query=besta', catalog, solo);
  assert.deepEqual(branchEntries(catalog, solo.bases[1], state).map(c => c.id), ['arbalester']);
});

test('as 36 avaliações preservam o inventário atual e não inventam círculos históricos', () => {
  assert.equal(proposal.classes.length, 6);
  assert.equal(proposal.classes.reduce((n, c) => n + c.skills.length, 0), 36);
  for (const c of proposal.classes) {
    const ref = catalog.later_classes.find(r => r.id === c.class_id);
    assert.deepEqual(c.skills.map(s => s.name), ref.skills.map(s => s.name));
    assert.ok(ref.skills.every(s => !('circle' in s)));
    assert.equal(ref.first_rank, null);
    assert.equal(ref.introduced_in_itos, null);
    assert.ok(c.skills.every(s => s.circle_proposed <= Math.min(3, 8 - c.solo_rank)));
  }
  const mergen = proposal.classes.find(c => c.class_id === 'mergen');
  assert.equal(mergen.max_circle_in_scope, 2);
  assert.ok(mergen.skills.every(s => s.circle_proposed <= 2));
  const lens = proposal.classes.find(c => c.class_id === 'appraiser').skills.find(s => s.name === 'Triplet Lense');
  assert.equal(lens.assessment, 'hold');
  const appraiser = readFileSync(new URL('../dist/docs/classes/catalogo/appraiser.html', import.meta.url), 'utf8');
  assert.ok(appraiser.includes('Suspender por enquanto'));
  assert.ok(appraiser.includes('não a inclui no kit jogável'));
});

test('as 18 novas imagens têm procedência e todas as fichas publicadas têm os dois visuais', () => {
  const assets = read('data/assets.json');
  for (const c of proposal.classes) {
    assert.deepEqual(assets.filter(a => a.class_id === c.class_id).map(a => a.role).sort(), ['female', 'icon', 'male']);
    const html = readFileSync(new URL(`../dist/docs/classes/catalogo/${c.class_id}.html`, import.meta.url), 'utf8');
    assert.ok(html.includes(`/assets/classes/${c.class_id}/female.gif`));
    assert.ok(html.includes(`/assets/classes/${c.class_id}/male.gif`));
    assert.ok(html.includes('Círculo proposto'));
    assert.ok(html.includes('seis-classes-habilidades-e-circulos.html'));
  }
});
