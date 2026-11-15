import { findPages } from './search.mjs';

const root = document.documentElement.dataset.root;
const treeCatalog = window.WIKI_CATALOG;
const treeSolo = window.WIKI_SOLO;
const currentClassId = document.documentElement.dataset.classId || '';
let navigationState = readTreeState(location.search, treeCatalog, treeSolo, currentClassId);
const dialog = document.querySelector('#search-dialog');
const input = document.querySelector('#search-input');
const results = document.querySelector('#search-results');
const status = document.querySelector('#search-status');
const searchOpeners = document.querySelectorAll('[data-search-open]');
let searchOrigin;

function openSearch() {
  searchOrigin = document.activeElement;
  if (!dialog.open) dialog.showModal();
  input.focus();
}

searchOpeners.forEach(button => button.addEventListener('click', openSearch));
document.querySelector('[data-search-close]').addEventListener('click', () => dialog.close());
dialog.addEventListener('click', event => { if (event.target === dialog) dialog.close(); });
dialog.addEventListener('close', () => searchOrigin?.focus());
document.addEventListener('keydown', event => {
  const editable = event.target.closest('input, textarea, [contenteditable]');
  if (event.key === '/' && !editable && !event.ctrlKey && !event.metaKey && !event.altKey) {
    event.preventDefault();
    openSearch();
  }
});

input.addEventListener('input', () => {
  results.replaceChildren();
  if (!input.value.trim()) {
    status.textContent = 'Procure uma classe, habilidade ou assunto da wiki.';
    return;
  }
  const matches = findPages(window.WIKI_SEARCH, input.value);
  status.textContent = matches.length ? `${matches.length} página(s) encontrada(s)` : 'Nenhum resultado. Tente outro nome ou menos palavras.';
  for (const page of matches) {
    const item = document.createElement('li');
    const link = document.createElement('a');
    const classId = page.path.match(/docs\/classes\/(?:catalogo\/)?([^/]+)\.html$/)?.[1];
    link.href = root + page.path + (treeCatalog.classes.some(c => c.id === classId) ? treeQuery(readTreeState(treeQuery(navigationState), treeCatalog, treeSolo, classId)) : '');
    const title = document.createElement('strong');
    title.textContent = page.title;
    const snippet = document.createElement('span');
    snippet.textContent = page.snippet;
    link.append(title, snippet);
    item.append(link);
    results.append(item);
  }
});

const menuButton = document.querySelector('#menu-toggle');
const sidebar = document.querySelector('#sidebar');
function closeMenu() {
  document.body.classList.remove('menu-open');
  menuButton.setAttribute('aria-expanded', 'false');
  sidebar.inert = matchMedia('(max-width: 900px)').matches;
}
menuButton.addEventListener('click', () => {
  const expanded = document.body.classList.toggle('menu-open');
  menuButton.setAttribute('aria-expanded', String(expanded));
  sidebar.inert = !expanded;
});
document.querySelector('#menu-shade').addEventListener('click', closeMenu);
document.addEventListener('keydown', event => {
  if (event.key === 'Escape' && document.body.classList.contains('menu-open')) {
    closeMenu();
    menuButton.focus();
  }
});
matchMedia('(max-width: 900px)').addEventListener('change', closeMenu);
closeMenu();

const headings = [...document.querySelectorAll('main h2[id]')];
const tocLinks = [...document.querySelectorAll('.toc a')];
if ('IntersectionObserver' in window && headings.length) {
  const observer = new IntersectionObserver(entries => {
    const entry = entries.find(value => value.isIntersecting);
    if (!entry) return;
    tocLinks.forEach(link => {
      const active = link.hash === '#' + entry.target.id;
      link.classList.toggle('active', active);
      if (active) link.setAttribute('aria-current', 'location');
      else link.removeAttribute('aria-current');
    });
  }, { rootMargin: '-10% 0px -65% 0px' });
  headings.forEach(heading => observer.observe(heading));
}

const topButton = document.querySelector('#back-top');
topButton.addEventListener('click', () => {
  window.scrollTo({ top: 0, behavior: matchMedia('(prefers-reduced-motion: reduce)').matches ? 'instant' : 'smooth' });
  document.querySelector('main').focus({ preventScroll: true });
});

const catalogForm = document.querySelector('#catalog-filters');

function updateTreeUI() {
  document.querySelector('#sidebar-tree').innerHTML = renderTreeSidebar(treeCatalog, treeSolo, navigationState, root, currentClassId);
  document.querySelector('#nav-tree-views').innerHTML = renderTreeViews(navigationState, root);
  document.querySelector('#edition-view').textContent = navigationState.view === 'solo' ? 'Projeto solo' : 'Original de 2016';
  const context = document.querySelector('#class-tree-context');
  if (context) context.innerHTML = renderClassContext(treeCatalog, treeSolo, navigationState, currentClassId, root);
  if (catalogForm) {
    document.querySelector('#tree-views').innerHTML = renderTreeViews(navigationState, root);
    document.querySelector('#tree-bases').innerHTML = renderBaseChoices(treeCatalog, treeSolo, navigationState, root);
    document.querySelector('#tree-branches').innerHTML = renderTreeBranches(treeCatalog, treeSolo, navigationState, root);
    document.querySelector('#tree-intro').textContent = navigationState.view === 'solo'
      ? 'Três bases, 26 especializações. Uma distribuição em construção, com posições definidas e propostas.'
      : 'Quatro bases e 51 especializações do catálogo histórico de 2016, incluindo três classes ocultas.';
    const counts = treeVisibleCount(treeCatalog, treeSolo, navigationState);
    document.querySelector('#catalog-count').textContent = `${counts.visible} de ${counts.total} especializações`;
    for (const key of ['query', 'rank', 'kind', 'visual']) {
      const field = catalogForm.elements.namedItem(key);
      if (field.value !== navigationState[key]) field.value = navigationState[key];
    }
    document.querySelectorAll('[data-historical-filter]').forEach(label => {
      label.hidden = navigationState.view !== '2016';
      label.querySelector('select').disabled = label.hidden;
    });
  }
  // Preserve the selected view and filters when following class references.
  for (const link of document.querySelectorAll('a[href]')) {
    const target = new URL(link.getAttribute('href'), location.href);
    if (target.origin !== location.origin) continue;
    const id = target.pathname.match(/docs\/classes\/(?:catalogo\/)?([^/]+)\.html$/)?.[1];
    if (!treeCatalog.classes.some(c => c.id === id)) continue;
    if (!link.hasAttribute('data-class-link') && target.searchParams.has('view')) continue;
    const state = readTreeState(treeQuery(navigationState), treeCatalog, treeSolo, id);
    target.search = treeQuery(state);
    link.href = target.pathname + target.search + target.hash;
  }
}

function storeTreeURL() {
  history.replaceState(null, '', location.pathname + treeQuery(navigationState) + location.hash);
}

if (catalogForm) {
  catalogForm.addEventListener('submit', event => event.preventDefault());
  function applyFilters() {
    const fields = Object.fromEntries(new FormData(catalogForm));
    navigationState = readTreeState(treeQuery({ ...navigationState, ...fields }), treeCatalog, treeSolo);
    storeTreeURL();
    updateTreeUI();
  }
  catalogForm.addEventListener('input', applyFilters);
  catalogForm.addEventListener('change', applyFilters);
  catalogForm.addEventListener('reset', event => {
    event.preventDefault();
    navigationState = { ...navigationState, query: '', rank: '', kind: '', visual: 'female' };
    storeTreeURL();
    updateTreeUI();
  });
  document.addEventListener('click', event => {
    const link = event.target.closest('a[data-tree-state]');
    if (!link || event.defaultPrevented || event.button !== 0 || event.metaKey || event.ctrlKey || event.shiftKey || event.altKey) return;
    const target = new URL(link.href);
    if (target.origin !== location.origin || target.pathname !== location.pathname) return;
    event.preventDefault();
    const fromMenu = Boolean(link.closest('#sidebar'));
    const focusKey = link.dataset.base ? `[data-base="${link.dataset.base}"]` : `[data-view="${link.dataset.view}"]`;
    const focusArea = fromMenu ? '#nav-tree-views, #sidebar-tree' : '#tree-views, #tree-bases';
    navigationState = readTreeState(target.search, treeCatalog, treeSolo);
    history.pushState(null, '', target.pathname + treeQuery(navigationState) + target.hash);
    updateTreeUI();
    if (fromMenu && matchMedia('(max-width: 900px)').matches) {
      closeMenu();
      document.querySelector('#arvore').focus({ preventScroll: true });
    } else {
      [...document.querySelectorAll(focusArea)].flatMap(area => [...area.querySelectorAll(focusKey)])[0]?.focus({ preventScroll: true });
    }
    if (target.hash === '#arvore') document.querySelector('#arvore').scrollIntoView({ block: 'start' });
  });
}
window.addEventListener('popstate', () => {
  navigationState = readTreeState(location.search, treeCatalog, treeSolo, currentClassId);
  updateTreeUI();
});
updateTreeUI();
