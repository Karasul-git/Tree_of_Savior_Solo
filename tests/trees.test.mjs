import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import { treeBases, soloMembership, readTreeState, treeQuery, branchEntries, treeVisibleCount,
  renderTreeBranches, renderClassContext, renderTreeViews, renderTreeSidebar, renderTreePage } from '../web/trees.mjs';

const catalog = JSON.parse(readFileSync(new URL('../data/catalog/classes-2016.json', import.meta.url)));
const solo = JSON.parse(readFileSync(new URL('../data/catalog/classes-solo.json', import.meta.url)));
const soloState = readTreeState('', catalog, solo);

test('a entrada solo tem 10/9/7 especializações e somente três posições definidas', () => {
  assert.equal(soloState.view, 'solo');
  assert.deepEqual(treeBases(catalog, solo).map(b => b.classes.length), [10, 9, 7]);
  assert.equal(treeVisibleCount(catalog, solo, soloState).total, 26);
  assert.equal(new Set(solo.bases.flatMap(b => [b.source_class_id, ...b.classes.map(c => c.class_id)])).size, 29);
  assert.deepEqual(solo.bases.flatMap(b => b.classes.filter(c => c.placement_status === 'defined').map(c => c.class_id)).sort(), ['alchemist', 'plague-doctor', 'templar']);
  assert.equal(soloMembership(solo, 'linker'), null);
  assert.equal(soloMembership(solo, 'thaumaturge'), null);
  assert.equal(soloMembership(solo, 'monk'), null);
});

test('a ficha de Alchemist volta para Scout com busca e visual preservados', () => {
  const state = readTreeState('?view=solo&base=scout&query=pocoes&visual=male', catalog, solo);
  const scout = solo.bases.find(b => b.id === 'scout');
  assert.deepEqual(branchEntries(catalog, scout, state).map(c => c.id), ['alchemist']);
  const detail = readTreeState(treeQuery(state), catalog, solo, 'alchemist');
  assert.deepEqual(detail, state);
  const html = renderClassContext(catalog, solo, detail, 'alchemist', '../../../');
  assert.ok(html.includes('catalogo-2016.html' + treeQuery(state) + '#arvore'));
  assert.ok(html.includes('Posição definida'));
  assert.ok(renderTreeBranches(catalog, solo, state, '../../').includes('/alchemist/male.gif'));
});

test('a visão histórica preserva Scout em Archer e acesso às 55 classes', () => {
  const snapshot = JSON.stringify(catalog);
  const state = readTreeState('?view=2016', catalog, solo);
  const bases = treeBases(catalog, solo, '2016');
  assert.deepEqual(bases.map(b => b.classes.length), [13, 13, 12, 13]);
  const html = renderTreeBranches(catalog, solo, state, '../../');
  const ids = [...html.matchAll(/data-class-link="([^"]+)"/g)].map(m => m[1]);
  assert.equal(new Set(ids).size, 55);
  assert.ok(bases.find(b => b.id === 'archer').classes.some(c => c.class_id === 'scout'));
  const detail = readTreeState('?view=2016&base=archer&rank=4&kind=regular', catalog, solo, 'scout');
  assert.equal(detail.base, 'archer');
  assert.equal(detail.rank, '4');
  assert.equal(JSON.stringify(catalog), snapshot);
});

test('busca, ranks históricos e classes ocultas continuam combináveis', () => {
  const state = readTreeState('?view=2016&base=wizard&rank=6&kind=hidden', catalog, solo);
  const wizard = treeBases(catalog, solo, '2016').find(b => b.id === 'wizard');
  assert.deepEqual(branchEntries(catalog, wizard, state).map(c => c.id), ['rune-caster']);
  const oldLink = readTreeState('?tree=Wizard&rank=6', catalog, solo);
  assert.equal(oldLink.view, '2016');
  assert.equal(oldLink.base, 'wizard');
  assert.equal(readTreeState('?view=solo&rank=7&kind=hidden', catalog, solo).rank, '');
});

test('links diretos reconhecem a base e classes fora do solo abrem a referência histórica', () => {
  assert.equal(readTreeState('', catalog, solo, 'swordsman').base, 'warrior');
  assert.equal(readTreeState('', catalog, solo, 'rogue').base, 'scout');
  assert.equal(readTreeState('', catalog, solo, 'linker').view, '2016');
  assert.equal(readTreeState('?view=solo&base=desconhecida', catalog, solo).base, '');
  const menu = renderTreeSidebar(catalog, solo, readTreeState('?view=solo&base=scout', catalog, solo), '../../', 'alchemist');
  assert.ok(menu.includes('data-class-link="alchemist"'));
  assert.ok(menu.includes('aria-current="page" class="current">Alchemist'));
  assert.ok(menu.includes('id="tree-nav-children-warrior" hidden'));
  assert.ok(menu.includes('aria-expanded="true" aria-controls="tree-nav-children-scout"'));
});

test('alternar a visão limpa seleção e filtros; texto pesquisado não vira marcação', () => {
  const state = readTreeState('?view=solo&base=warrior&query=%22%3E%3Cscript%3E&visual=male', catalog, solo);
  const views = renderTreeViews(state, '../../');
  assert.ok(views.includes('catalogo-2016.html?view=2016"'));
  assert.ok(!views.includes('base='));
  const html = renderTreePage(catalog, solo, state, '../../');
  assert.ok(!html.includes('<script>'));
  assert.ok(html.includes('&quot;&gt;&lt;script&gt;'));
});

test('a saída publicada inclui dados de árvore, controles e três entradas solo', () => {
  const html = readFileSync(new URL('../dist/docs/classes/catalogo-2016.html', import.meta.url), 'utf8');
  assert.ok(html.includes('id="tree-bases"'));
  assert.ok(html.includes('id="arvore" tabindex="-1"'));
  assert.ok(html.includes('tree-data.js'));
  assert.equal((html.match(/class="tree-choice/g) || []).length, 3);
  const home = readFileSync(new URL('../dist/index.html', import.meta.url), 'utf8');
  for (const base of ['warrior', 'archer', 'scout']) assert.ok(home.includes(`view=solo&amp;base=${base}#arvore`));
  const ref = readFileSync(new URL('../dist/docs/classes/catalogo/linker.html', import.meta.url), 'utf8');
  assert.ok(ref.includes('Original de 2016 · Wizard'));
});
