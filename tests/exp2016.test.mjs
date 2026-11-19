import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

const d = JSON.parse(readFileSync(new URL('../data/research/tosbase-2016-exp.json', import.meta.url), 'utf8'));

test('a tabela de experiência base de 2016 confere com as capturas de 02/04/2016', () => {
  assert.deepEqual(d.base.slice(0, 3), [{ level: 2, required: 8, total: 8 }, { level: 3, required: 272, total: 280 }, { level: 4, required: 462, total: 742 }]);
  const l100 = d.base.find(r => r.level === 100);
  assert.deepEqual(l100, { level: 100, required: 342280, total: 13229936 });
  for (let i = 1; i < d.base.length; i += 1) assert.equal(d.base[i].total - d.base[i - 1].total, d.base[i].required, `nível ${d.base[i].level}`);
});

test('a experiência de classe tem sete ranks de 15 níveis e o rank 1 fecha em 19.377', () => {
  const ranks = Object.keys(d.class_by_rank).filter(k => k.startsWith('Rank'));
  assert.equal(ranks.length, 7);
  for (const r of ranks) assert.equal(d.class_by_rank[r].length, 15);
  assert.equal(d.class_by_rank['Rank 1'][14].total, 19377);
});

test('as páginas de experiência e de regras existem no site', () => {
  for (const f of ['original-2016/experiencia', 'sistemas/regras-do-jogo-2016']) assert.ok(existsSync(new URL(`../dist/docs/${f}.html`, import.meta.url)), f);
});
