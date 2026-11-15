import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import { matchesCatalogEntry } from '../web/catalog.mjs';

const { classes } = JSON.parse(readFileSync(new URL('../data/catalog/classes-2016.json', import.meta.url), 'utf8'));
const select = filters => classes.filter(entry => matchesCatalogEntry(entry, filters)).map(entry => entry.name);

test('o catálogo usa as árvores antigas ao combinar nome, árvore e rank', () => {
  assert.deepEqual(select({ query: 'Scout', tree: 'Archer', rank: '4' }), ['Scout']);
  assert.deepEqual(select({ query: 'Scout', tree: 'Swordsman', rank: '4' }), []);
});

test('a pesquisa encontra funções em português sem acentos e nomes de habilidades', () => {
  assert.ok(select({ query: 'pocoes' }).includes('Alchemist'));
  assert.deepEqual(select({ query: 'Bunshin no Jutsu', kind: 'hidden' }), ['Shinobi']);
  assert.deepEqual(select({ query: 'Bunshin no Jutsu', kind: 'regular' }), []);
});

test('filtros simultâneos e limpeza preservam o conjunto completo', () => {
  assert.deepEqual(select({ tree: 'Wizard', rank: '6', kind: 'hidden' }), ['Rune Caster']);
  assert.deepEqual(select({ query: 'Cyclone termo-inexistente-xyz' }), []);
  assert.equal(select({ query: '   ', tree: '', rank: '', kind: '' }).length, 55);
});
