import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url), 'utf8'));
const { monsters } = read('data/research/tosbase-2016-monsters.json');
const index = read('data/original-2016/monsters-index.json');

test('todos os monstros lidos têm nível, HP, atributos e combate, e a captura de 2016', () => {
  const list = Object.values(monsters);
  assert.ok(list.length > 1100);
  for (const m of list) {
    assert.ok(m.name && Number.isFinite(m.level) && Number.isFinite(m.hp), m.tosbase_id);
    assert.ok(m.attributes && m.combat, m.tosbase_id);
    assert.match(m.captured_at, /^2016\d{10}$/);
    assert.match(m.html_sha256, /^[0-9a-f]{64}$/);
    for (const d of m.drops) assert.ok(d.chance_pct === null || (d.chance_pct > 0 && d.chance_pct <= 100), `${m.tosbase_id} ${d.name}`);
  }
});

test('o Q2 Minotaur confere com a página capturada em 09/05/2016', () => {
  const m = monsters['57078'];
  assert.deepEqual([m.level, m.hp, m.base_exp, m.class_exp, m.combat.DEF, m.combat.ATK], [116, 64094, 8520, 6150, 163, [331, 416]]);
  assert.deepEqual(m.drops.slice(0, 2).map(d => [d.name, d.chance_pct]), [['Kepa Stem', 12], ['Kepa Doll', 1.5]]);
});

test('cada monstro e cada faixa têm página no site', () => {
  assert.equal(index.monsters.length, Object.keys(monsters).length);
  for (const b of index.bands) assert.ok(existsSync(new URL(`../dist/docs/original-2016/monstros/nivel-${b.low}.html`, import.meta.url)));
  for (const m of index.monsters) assert.ok(existsSync(new URL(`../dist/docs/original-2016/monstros/${m.id}.html`, import.meta.url)), m.id);
});
