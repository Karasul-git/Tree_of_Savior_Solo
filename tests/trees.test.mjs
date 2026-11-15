import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import { treeBases, soloMembership, readTreeState, treeQuery, branchEntries, treeVisibleCount,
  renderTreeBranches, renderClassContext, renderTreeViews, renderTreeSidebar, renderTreePage, treeRankCoverage } from '../web/trees.mjs';

const catalog = JSON.parse(readFileSync(new URL('../data/catalog/classes-2016.json', import.meta.url)));
catalog.later_classes = JSON.parse(readFileSync(new URL('../data/catalog/classes-later.json', import.meta.url))).classes;
const solo = JSON.parse(readFileSync(new URL('../data/catalog/classes-solo.json', import.meta.url)));
const soloState = readTreeState('', catalog, solo);

test('a entrada solo tem 12/12/12 especializações, todas ratificadas', () => {
  assert.equal(soloState.view, 'solo');
  assert.deepEqual(treeBases(catalog, solo).map(b => b.classes.length), [12, 12, 12]);
  assert.equal(treeVisibleCount(catalog, solo, soloState).total, 36);
  assert.equal(new Set(solo.bases.flatMap(b => [b.source_class_id, ...b.classes.map(c => c.class_id)])).size, 39);
  assert.equal(solo.bases.flatMap(b => b.classes.filter(c => c.placement_status === 'defined')).length, 36);
  assert.equal(solo.status, 'ratified-tree');
  assert.equal(soloMembership(solo, 'linker'), null);
  assert.equal(soloMembership(solo, 'thaumaturge'), null);
  assert.equal(soloMembership(solo, 'monk').base.id, 'warrior');
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
  assert.equal(readTreeState('?view=solo&rank=7&kind=hidden', catalog, solo).rank, '7');
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

test('as três bases cobrem R2–R7 e os ranks solo não alteram a história', () => {
  for (const base of solo.bases) {
    assert.ok(treeRankCoverage(catalog, base).every(r => r.count > 0));
  }
  assert.deepEqual(treeRankCoverage(catalog, solo.bases[0]).map(r => r.count), [2, 2, 2, 2, 2, 2]);
  assert.deepEqual(treeRankCoverage(catalog, solo.bases[2]).filter(r => r.soloRank).map(r => r.rank), [2, 3, 4]);
  const state = readTreeState('?view=solo&base=scout&query=rogue&rank=4', catalog, solo);
  assert.deepEqual(branchEntries(catalog, solo.bases[2], state).map(c => c.id), ['rogue']);
  assert.equal(catalog.classes.find(c => c.id === 'rogue').first_rank, 5);
  const historical = readTreeState('?view=2016&base=archer&rank=5&query=rogue', catalog, solo);
  assert.deepEqual(branchEntries(catalog, treeBases(catalog, solo, '2016')[2], historical).map(c => c.id), ['rogue']);
  const html = renderTreeBranches(catalog, solo, state, '../../');
  assert.ok(html.includes('Rank solo definido'));
  assert.ok(html.includes('Original: Archer · R5'));
});

test('classes posteriores abrem Scout com versão explícita e não entram em 2016', () => {
  assert.equal(catalog.classes.length, 55);
  for (const id of catalog.later_classes.map(c => c.id)) {
    assert.ok(!treeBases(catalog, solo, '2016').some(b => b.classes.some(c => c.class_id === id)));
    const state = readTreeState('?view=2016', catalog, solo, id);
    assert.equal(state.view, 'solo');
    assert.equal(state.base, catalog.later_classes.find(c => c.id === id).tree.toLowerCase());
    const html = renderClassContext(catalog, solo, state, id, '../../../');
    assert.ok(html.includes('Habilidades da página oficial atual'));
    const page = readFileSync(new URL(`../dist/docs/classes/catalogo/${id}.html`, import.meta.url), 'utf8');
    assert.ok(page.includes('Não pertence ao catálogo de 2016') || page.includes('Fora do recorte de março–abril de 2016'));
    assert.ok(page.includes('data-class-id="' + id + '"'));
  }
});

test('a detecção de lacunas funciona antes de se propor novos ranks', () => {
  const scout = solo.bases[2];
  const oldScout = { ...scout, classes: scout.classes.filter(c => !['assassin', 'outlaw', 'clown', 'sheriff', 'ardito'].includes(c.class_id))
    .map(c => c.class_id === 'rogue' ? { ...c, solo_rank: 5, rank_status: 'historical-reference' } : c) };
  assert.deepEqual(treeRankCoverage(catalog, oldScout).filter(r => !r.count).map(r => r.rank), [2, 3, 4]);
  const oldSolo = { ...solo, bases: [oldScout] };
  const html = renderTreeBranches(catalog, oldSolo, { view: 'solo', visual: 'female' }, '../../');
  assert.ok(html.includes('Lacunas: R2, R3, R4'));
  assert.equal((html.match(/Sem especialização neste rank/g) || []).length, 3);
});
