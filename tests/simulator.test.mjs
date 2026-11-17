import { test } from 'node:test';
import assert from 'node:assert/strict';
import { fileURLToPath } from 'node:url';
import path from 'node:path';
import { compute, investmentBonus, distribute, interpolate } from '../web/simulador-core.mjs';
import { simulatorData } from '../web/simulador-data.mjs';

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const data = simulatorData(root);

const build = (preset, level, skillLevel = 5) => {
  const p = data.presets.find(x => x.id === preset);
  return { base: p.base, level, main: p.main, off: p.off, armor: p.armor, gearLevel: level, weaponGrade: 1, skillLevel, points: distribute(p.pct, level - 1) };
};
const near = (actual, expected, tolerance, label) => assert.ok(Math.abs(actual - expected) <= tolerance, `${label}: ${actual} longe de ${expected}`);

test('o bônus por investimento confere com os exemplos de points-per-level.json', () => {
  const ranges = data.investment_bonus_ranges;
  assert.equal(investmentBonus(50, ranges), 10);
  assert.equal(investmentBonus(100, ranges), 22);
  assert.equal(investmentBonus(150, ranges), 35);
  assert.equal(investmentBonus(279, ranges), 78);
});

test('a distribuição fecha o total exato e mantém as proporções', () => {
  const points = distribute({ str: 60, con: 40, int: 0, spr: 0, dex: 0 }, 119);
  assert.equal(points.str + points.con + points.int + points.spr + points.dex, 119);
  assert.equal(points.str, 71);
  assert.equal(points.con, 48);
});

test('a interpolação ignora faixas sem dado e usa as pontas fora do intervalo', () => {
  assert.equal(interpolate([1, 15, 40], [null, 10, 20], 5), 10);
  assert.equal(interpolate([1, 15, 40], [null, 10, 20], 27.5), 15);
  assert.equal(interpolate([1, 15, 40], [null, 10, 20], 99), 20);
});

// Valores de base-comparison.json (conta à mão feita em 15/11/2026).
test('Warrior defensivo no nível 120 confere com a comparação das bases', () => {
  const r = compute(data, build('warrior-defensivo', 120));
  near(r.attack, 1301, 3, 'ataque');
  near(r.defense, 3147, 3, 'defesa');
  near(r.hp, 12408, 30, 'HP');
  near(r.fight.playerReductionPct, 64, 1, 'redução');
  near(r.fight.hitPctHp, 5.2, 0.2, 'golpe recebido');
  near(r.fight.skillsToKill, 6.0, 0.2, 'habilidades para derrubar');
});

test('Warrior de duas mãos no nível 120 confere com a comparação das bases', () => {
  const r = compute(data, build('warrior-ofensivo', 120));
  near(r.attack, 1469, 3, 'ataque');
  near(r.defense, 1617, 3, 'defesa');
  near(r.fight.hitPctHp, 7.5, 0.2, 'golpe recebido');
  near(r.fight.skillsToKill, 5.2, 0.2, 'habilidades para derrubar');
});

test('Archer e Scout no nível 120 conferem com a comparação das bases', () => {
  const archer = compute(data, build('archer', 120));
  near(archer.attack, 1429, 3, 'ataque do Archer');
  near(archer.defense, 1554, 3, 'defesa do Archer');
  near(archer.hp, 10048, 30, 'HP do Archer');
  near(archer.dodge, 64, 1, 'esquiva do Archer');
  near(archer.critAttack, 253, 2, 'ataque crítico do Archer');
  near(archer.fight.dodgePct, 11.5, 0.5, 'desvio do Archer');
  const scout = compute(data, build('scout', 120));
  near(scout.attack, 1442, 3, 'ataque do Scout');
  near(scout.hp, 10074, 30, 'HP do Scout');
});

test('o nível não é preenchido sozinho: sem pontos, o resultado usa só os iniciais', () => {
  const r = compute(data, { base: 'warrior', level: 120, main: 'Sword', off: 'none', armor: 'leather', gearLevel: 120, weaponGrade: 1, skillLevel: 5, points: { str: 0, con: 0, int: 0, spr: 0, dex: 0 } });
  assert.equal(r.stat.str, 7);
  assert.equal(r.pointsUsed, 0);
});

test('abaixo do nível 15 não há monstro de referência', () => {
  assert.equal(compute(data, build('archer', 10)).fight, undefined);
});
