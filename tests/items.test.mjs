import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url), 'utf8'));
const { items, categories } = read('data/items/items.json');

test('o banco tem as 20 categorias e só itens até o nível 280, sem itens de teste ou temporários', () => {
  assert.equal(categories.length, 20);
  assert.ok(items.length > 2000);
  assert.ok(items.every(i => i.level <= 280 && i.grade !== 99 && !i.name.includes('Days)')));
  assert.equal(new Set(items.map(i => i.id)).size, items.length);
  assert.equal(new Set(items.map(i => `${i.folder}/${i.slug}`)).size, items.length);
});

test('cada item e cada categoria têm página no site', () => {
  for (const c of categories) assert.ok(existsSync(new URL(`../dist/docs/itens/${c.folder}/index.html`, import.meta.url)), c.folder);
  for (const i of items) assert.ok(existsSync(new URL(`../dist/docs/itens/${i.folder}/${i.slug}.html`, import.meta.url)), i.slug);
});

test('os valores do servidor chegam sem alteração à página do item', () => {
  const sabre = items.find(i => i.class_name === 'SWD01_106');
  assert.deepEqual([sabre.name, sabre.level, sabre.atk_min, sabre.atk_max, sabre.price], ['Sabre', 40, 317, 337, 3640]);
  const page = readFileSync(new URL('../docs/itens/espadas/swd01-106.md', import.meta.url), 'utf8');
  assert.match(page, /Ataque \| 317 a 337 \(média 327\)/);
  assert.match(page, /Preço de compra \| 3\.640/);
  const armor = items.find(i => i.class_name === 'TOP01_106');
  assert.equal(armor.material, 'Leather');
  assert.equal(armor.def, 145);
});
