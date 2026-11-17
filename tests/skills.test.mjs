import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url), 'utf8'));
const { classes } = read('data/skills/skills.json');
const solo = read('data/catalog/classes-solo.json');

test('o catálogo de habilidades cobre as 39 classes da seleção solo', () => {
  const expected = solo.bases.flatMap(b => [b.source_class_id, ...b.classes.map(c => c.class_id)]).sort();
  assert.deepEqual(classes.map(c => c.class_id).sort(), expected);
  assert.equal(classes.length, 39);
});

test('cada habilidade tem página própria, nome único na classe e situação válida', () => {
  for (const c of classes) {
    assert.ok(c.skills.length > 0, c.class_id);
    assert.equal(new Set(c.skills.map(s => s.slug)).size, c.skills.length, `slugs repetidos em ${c.class_id}`);
    assert.ok(existsSync(new URL(`../docs/habilidades/${c.class_id}/README.md`, import.meta.url)));
    for (const s of c.skills) {
      assert.ok(['no-jogo', 'em-revisao'].includes(s.status), `${c.class_id}/${s.slug}`);
      assert.ok(existsSync(new URL(`../docs/habilidades/${c.class_id}/${s.slug}.md`, import.meta.url)), `${c.class_id}/${s.slug}`);
      assert.ok(existsSync(new URL(`../dist/docs/habilidades/${c.class_id}/${s.slug}.html`, import.meta.url)), `site: ${c.class_id}/${s.slug}`);
    }
  }
});

test('o Scout usa o kit definido: cinco habilidades em C1, duas em C2 e uma em C3', () => {
  const scout = classes.find(c => c.class_id === 'scout');
  assert.deepEqual(scout.skills.map(s => s.name).sort(), ['Camouflage', 'Cloaking', 'Dagger Slash', 'Double Attack', 'Free Step', 'Oblique Fire', 'Perspective Distortion', 'Scan']);
  const byCircle = n => scout.skills.filter(s => s.circle === n).length;
  assert.deepEqual([byCircle(1), byCircle(2), byCircle(3)], [5, 2, 1]);
});

test('o fator no nosso jogo é o do Melia dividido por 10 e cresce 10% do nível 1 por nível', () => {
  const page = readFileSync(new URL('../docs/habilidades/swordsman/thrust.md', import.meta.url), 'utf8');
  assert.match(page, /\| 1 \| 168% \| 1\.678% \|/);
  assert.match(page, /\| 15 \| 403% \| 5\.206% \|/);
});
