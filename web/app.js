import { findPages } from './search.mjs';

const root = document.documentElement.dataset.root;
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
    link.href = root + page.path;
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
if (catalogForm) {
  const entries = [...document.querySelectorAll('[data-catalog-entry]')];
  const count = document.querySelector('#catalog-count');
  const empty = document.querySelector('#catalog-empty');
  function updateCatalog() {
    const data = new FormData(catalogForm);
    const filters = Object.fromEntries(data);
    let visible = 0;
    for (const entry of entries) {
      const match = matchesCatalogEntry(entry.dataset, filters);
      entry.hidden = !match;
      if (match) visible++;
      const sprite = entry.querySelector('[data-catalog-sprite]');
      const role = filters.visual === 'male' ? 'male' : 'female';
      const target = sprite.dataset[role];
      if (sprite.getAttribute('src') !== target) sprite.src = target;
      sprite.alt = `${entry.dataset.name} · visual ${role === 'male' ? 'masculino' : 'feminino'} oficial`;
    }
    count.textContent = `${visible} de ${entries.length} classes`;
    empty.hidden = visible !== 0;
  }
  catalogForm.addEventListener('submit', event => event.preventDefault());
  catalogForm.addEventListener('input', updateCatalog);
  catalogForm.addEventListener('change', updateCatalog);
  catalogForm.addEventListener('reset', () => setTimeout(updateCatalog, 0));
  updateCatalog();
}
