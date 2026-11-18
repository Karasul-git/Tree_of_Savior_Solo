import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url), 'utf8'));
const { items } = read('data/research/tosbase-2016-items.json');
const index = read('data/original-2016/items-index.json');

test('o Old Gladius de 2016 confere com a página capturada em 16/04/2016', () => {
  const g = items['101101'];
  assert.equal(g.name, 'Old Gladius');
  assert.deepEqual([g.weight, g.required_level, g.potential, g.durability, g.stats['Physical Attack']], [90, 1, '3 / 3', '36 / 36', '4 ~ 5']);
  assert.deepEqual(g.required_classes, ['Swordsman', 'Wizard', 'Cleric']);
  assert.equal(g.enhancement.length, 15);
  assert.deepEqual(g.enhancement[0], { level: 1, bonus: 3, success_pct: 100, price: 115 });
  assert.deepEqual(g.enhancement[14], { level: 15, bonus: 75, success_pct: 51, price: 2702 });
});

test('todo item lido traz captura de 2016 e tem página no site', () => {
  assert.ok(Object.keys(items).length > 3000);
  for (const i of Object.values(items)) {
    assert.match(i.captured_at, /^2016\d{10}$/);
    assert.match(i.html_sha256, /^[0-9a-f]{64}$/);
  }
  assert.equal(index.items.length, Object.keys(items).length);
  for (const i of index.items) assert.ok(existsSync(new URL(`../dist/docs/original-2016/itens/${i.folder}/${i.id}.html`, import.meta.url)), i.id);
});

test('os drops do bestiário ligam aos itens de 2016', () => {
  const page = readFileSync(new URL('../docs/original-2016/monstros/57078.md', import.meta.url), 'utf8');
  assert.match(page, /\[Kepa Stem\]\(\.\.\/itens\/[a-z-]+\/645117\.md\)/);
});
