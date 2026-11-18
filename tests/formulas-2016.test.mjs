import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

// Fórmulas da calculadora do ToSBase (15/03/2016), reescritas aqui para conferir a página da wiki.
const bonus = v => Math.floor(v < 51 ? v / 5 : v < 151 ? 10 + (v - 50) / 4 : v < 301 ? 35 + (v - 150) / 3 : v < 501 ? 85 + (v - 300) / 2 : 185 + (v - 500));
const hp = (rate, level, con) => Math.floor(rate * Math.floor((level - 1) * 17) + Math.floor(con * 85));

test('o bônus por investimento de 2016 confere com os exemplos do beta de 2015', () => {
  assert.equal(bonus(50), 10);
  assert.equal(bonus(150), 35);
  assert.equal(bonus(300), 85);
});

test('o exemplo da página de fórmulas de 2016 confere com a conta', () => {
  const con = 8 + 99;
  const final = con + bonus(con);
  assert.equal(final, 131);
  assert.equal(hp(3.3, 100, final), 16688);
  const page = readFileSync(new URL('../docs/sistemas/formulas-de-status-2016.md', import.meta.url), 'utf8');
  assert.match(page, /\*\*16\.688\*\*/);
  assert.ok(existsSync(new URL('../dist/docs/sistemas/formulas-de-status-2016.html', import.meta.url)));
});
