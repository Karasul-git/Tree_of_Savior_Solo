import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import { findPages } from '../web/search.mjs';

const script = readFileSync(new URL('../dist/search-index.js', import.meta.url), 'utf8');
const pages = JSON.parse(script.slice('window.WIKI_SEARCH='.length).trim().replace(/;$/, ''));

test('a busca ignora acentos e encontra o documento de círculos', () => {
  assert.ok(findPages(pages, 'circulos').some(page => page.path.endsWith('ranks-e-circulos.html')));
});

test('uma habilidade removida da versão atual continua encontrável no acervo histórico', () => {
  assert.ok(findPages(pages, 'Reflect Shield').some(page => page.path.endsWith('wizard.html')));
});

test('a busca exige todos os termos e prioriza o título da classe', () => {
  assert.equal(findPages(pages, 'archer')[0].path, 'docs/classes/archer.html');
  assert.equal(findPages(pages, 'archer termo-inexistente-xyz').length, 0);
  assert.equal(findPages(pages, '  ').length, 0);
});

test('as pendências e suas fontes podem ser encontradas pela busca', () => {
  const result = findPages(pages, 'Guardian Saint');
  assert.ok(result.some(page => page.path.endsWith('cleric.html')));
  assert.ok(result.some(page => page.path.endsWith('fontes.html')));
});

test('a mudança de sistema tem um documento próprio encontrável na pesquisa', () => {
  assert.ok(findPages(pages, 'Re:Build').some(page => page.path.endsWith('evolucao-das-classes.html')));
});

test('a tabela histórica recuperada pode ser encontrada sem conhecer o nome do arquivo', () => {
  assert.ok(findPages(pages, 'Swordsman tabelas 2016').some(page => page.path.endsWith('swordsman-valores-2016.html')));
});
