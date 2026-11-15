import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';
import vm from 'node:vm';

const catalog = JSON.parse(readFileSync(new URL('../data/catalog/classes-2016.json', import.meta.url)));
catalog.later_classes = JSON.parse(readFileSync(new URL('../data/catalog/classes-later.json', import.meta.url))).classes;
const solo = JSON.parse(readFileSync(new URL('../data/catalog/classes-solo.json', import.meta.url)));
const code = readFileSync(new URL('../dist/assets/app.js', import.meta.url), 'utf8');

// Unit harness for the controller's events and output; this does not replace browser QA.
function mount(url, classId = '', mobile = false) {
  const nodes = new Map();
  const listeners = new Map();
  const windowListeners = new Map();
  function node() {
    const events = new Map();
    return { innerHTML: '', textContent: '', value: '', hidden: false, disabled: false, events,
      addEventListener(type, handler) { events.set(type, handler); },
      setAttribute() {}, focus() { this.focused = true; }, scrollIntoView() {}, replaceChildren() {},
      querySelectorAll() { return [{ focus() {} }]; },
      querySelector() { return this.select; } };
  }
  for (const id of ['search-dialog', 'search-input', 'search-results', 'search-status', 'menu-toggle', 'sidebar', 'menu-shade', 'back-top', 'sidebar-tree', 'nav-tree-views', 'edition-view']) nodes.set('#' + id, node());
  if (classId) nodes.set('#class-tree-context', node());
  else for (const id of ['catalog-filters', 'tree-views', 'tree-bases', 'tree-branches', 'tree-intro', 'catalog-count', 'arvore', 'rank-coverage', 'rank-filter-label']) nodes.set('#' + id, node());
  nodes.set('[data-search-close]', node());
  const fields = Object.fromEntries(['query', 'rank', 'kind', 'visual'].map(key => [key, { value: key === 'visual' ? 'female' : '', disabled: false }]));
  if (!classId) nodes.get('#catalog-filters').elements = { namedItem: key => fields[key] };
  const historical = ['kind'].map(key => ({ hidden: true, querySelector: () => fields[key] }));
  const doc = {
    documentElement: { dataset: { root: classId ? '../../../' : '../../', classId } },
    body: { classList: { remove() {}, contains: () => false } },
    querySelector: selector => nodes.get(selector) || null,
    querySelectorAll: selector => selector === '[data-historical-filter]' ? historical : selector === '#tree-views, #tree-bases' ? [node()] : [],
    addEventListener(type, handler) { const handlers = listeners.get(type) || []; handlers.push(handler); listeners.set(type, handlers); }
  };
  const win = { WIKI_CATALOG: catalog, WIKI_SOLO: solo, WIKI_SEARCH: [], addEventListener: (type, fn) => windowListeners.set(type, fn) };
  const sandbox = { document: doc, window: win, location: new URL(url), URL, URLSearchParams,
    matchMedia: () => ({ matches: mobile, addEventListener() {} }), setTimeout,
    FormData: class { constructor() { this.items = Object.entries(fields).filter(([, field]) => !field.disabled).map(([key, field]) => [key, field.value]); } [Symbol.iterator]() { return this.items[Symbol.iterator](); } }
  };
  sandbox.history = { replaceState(_, __, path) { sandbox.location = new URL(path, sandbox.location); }, pushState(_, __, path) { sandbox.location = new URL(path, sandbox.location); } };
  vm.createContext(sandbox);
  vm.runInContext(code, sandbox);
  return { nodes, fields, sandbox,
    filter() { nodes.get('#catalog-filters').events.get('input')({}); },
    reset() { nodes.get('#catalog-filters').events.get('reset')({ preventDefault() {} }); },
    click(search, dataset, fromMenu = false) {
      const link = { href: new URL('catalogo-2016.html' + search, sandbox.location).href, dataset, closest: () => fromMenu ? nodes.get('#sidebar') : null };
      const event = { target: { closest: () => link }, button: 0, preventDefault() {} };
      for (const handler of listeners.get('click') || []) handler(event);
    },
    pop(url) { sandbox.location = new URL(url); windowListeners.get('popstate')(); }
  };
}

test('selecionar Scout, filtrar poções e voltar restaura a árvore, a busca e o visual', () => {
  const app = mount('https://example.test/docs/classes/catalogo-2016.html');
  assert.equal(app.nodes.get('#catalog-count').textContent, '36 de 36 especializações');
  app.click('?view=solo&base=scout#arvore', { base: 'scout' });
  assert.equal(app.nodes.get('#catalog-count').textContent, '12 de 12 especializações');
  app.fields.query.value = 'pocoes';
  app.fields.visual.value = 'male';
  app.filter();
  const restore = app.sandbox.location.href;
  assert.equal(app.nodes.get('#catalog-count').textContent, '1 de 12 especializações');
  assert.ok(app.nodes.get('#tree-branches').innerHTML.includes('/alchemist/male.gif'));
  const detail = mount('https://example.test/docs/classes/catalogo/alchemist.html' + app.sandbox.location.search, 'alchemist');
  assert.ok(detail.nodes.get('#class-tree-context').innerHTML.includes('base=scout&query=pocoes&visual=male#arvore'));
  app.click('?view=solo&base=warrior', { base: 'warrior' });
  app.pop(restore);
  assert.equal(app.fields.query.value, 'pocoes');
  assert.equal(app.fields.visual.value, 'male');
  assert.equal(app.nodes.get('#catalog-count').textContent, '1 de 12 especializações');
});

test('alternar a visão limpa filtros, habilita os controles históricos e permite reset', () => {
  const app = mount('https://example.test/docs/classes/catalogo-2016.html?view=solo&base=scout&query=pocoes&visual=male');
  assert.equal(app.fields.rank.disabled, false);
  assert.equal(app.fields.kind.disabled, true);
  app.click('?view=2016', { view: '2016' });
  assert.equal(app.fields.query.value, '');
  assert.equal(app.fields.rank.disabled, false);
  assert.equal(app.nodes.get('#catalog-count').textContent, '51 de 51 especializações');
  app.fields.kind.value = 'hidden';
  app.filter();
  assert.equal(app.nodes.get('#catalog-count').textContent, '3 de 51 especializações');
  app.reset();
  assert.equal(app.nodes.get('#catalog-count').textContent, '51 de 51 especializações');
});


test('selecionar uma base pelo menu móvel fecha o painel e leva foco à árvore', () => {
  const app = mount('https://example.test/docs/classes/catalogo-2016.html', '', true);
  app.click('?view=solo&base=warrior#arvore', { base: 'warrior' }, true);
  assert.equal(app.nodes.get('#sidebar').inert, true);
  assert.equal(app.nodes.get('#arvore').focused, true);
  assert.equal(app.nodes.get('#catalog-count').textContent, '12 de 12 especializações');
});

test('filtro de rank solo persiste em URL, retorno da ficha e navegação para trás', () => {
  const app = mount('https://example.test/docs/classes/catalogo-2016.html?view=solo&base=scout');
  app.fields.rank.value = '4';
  app.filter();
  const restore = app.sandbox.location.href;
  assert.equal(app.nodes.get('#catalog-count').textContent, '2 de 12 especializações');
  assert.ok(app.nodes.get('#tree-branches').innerHTML.includes('data-catalog-entry="rogue"'));
  const detail = mount('https://example.test/docs/classes/catalogo/rogue.html' + app.sandbox.location.search, 'rogue');
  assert.ok(detail.nodes.get('#class-tree-context').innerHTML.includes('base=scout&rank=4#arvore'));
  app.click('?view=2016', { view: '2016' });
  assert.equal(app.fields.rank.value, '');
  app.pop(restore);
  assert.equal(app.fields.rank.value, '4');
  assert.equal(app.nodes.get('#catalog-count').textContent, '2 de 12 especializações');
  assert.ok(app.nodes.get('#rank-coverage').innerHTML.includes('Meta: duas especializações por rank'));
});
