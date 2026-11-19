import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url), 'utf8'));
const { quests } = read('data/research/tosbase-2016-quests.json');
const { recipes } = read('data/research/tosbase-2016-recipes.json');
const { sets } = read('data/research/tosbase-2016-sets.json');
const index = read('data/original-2016/world-index.json');

test('a quest Beyond the Darkness confere com a página capturada em 06/04/2016', () => {
  const q = quests['8527'];
  assert.deepEqual([q.name, q.level, q.required_level, q.start_npc.name, q.map.name], ['Beyond the Darkness', 40, 30, 'Follower Vaidas', 'Tenet Church B1']);
  assert.deepEqual(q.task_monsters, [{ monster_id: 57087, name: 'Cyclops', count: 1 }]);
  assert.deepEqual(q.reward_all, [{ item_id: 640082, count: 3 }, { item_id: 582113, count: 1 }, { item_id: 644043, count: 1 }]);
  assert.equal(q.required_quests[0].quest_id, 8522);
});

test('a receita Fedimian Spear tem o resultado e os materiais', () => {
  const r = recipes['17016'];
  assert.deepEqual(r.result, { item_id: 242110, name: 'Fedimian Spear' });
  assert.deepEqual(r.materials.map(m => [m.item_id, m.count]), [[17016, 1], [649003, 26]]);
});

test('quests, receitas e conjuntos têm captura de 2016 e página no site', () => {
  for (const group of [quests, recipes, sets]) for (const v of Object.values(group)) assert.match(v.captured_at, /^2016\d{10}$/);
  assert.ok(Object.keys(quests).length > 1700 && Object.keys(recipes).length > 600);
  for (const q of index.quests) assert.ok(existsSync(new URL(`../dist/docs/original-2016/quests/${q.id}.html`, import.meta.url)), q.id);
  for (const r of index.recipes) assert.ok(existsSync(new URL(`../dist/docs/original-2016/receitas/${r.id}.html`, import.meta.url)), r.id);
  for (const s of index.sets) assert.ok(existsSync(new URL(`../dist/docs/original-2016/conjuntos/${s.id}.html`, import.meta.url)), s.id);
});

test('o item de recompensa liga à quest e a receita liga ao resultado', () => {
  const item = readFileSync(new URL('../docs/original-2016/itens/colares/582113.md', import.meta.url), 'utf8');
  assert.match(item, /\(\.\.\/\.\.\/quests\/8527\.md\)/);
  const recipe = readFileSync(new URL('../docs/original-2016/receitas/17016.md', import.meta.url), 'utf8');
  assert.match(recipe, /\]\(\.\.\/itens\/[a-z-]+\/242110\.md\)/);
});
