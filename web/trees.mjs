import { matchesCatalogEntry } from './catalog.mjs';

const treeEscape = value => String(value).replace(/[&<>"']/g, char => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[char]));
const treeSortName = (a, b) => a.name.localeCompare(b.name, 'pt-BR');

export function treeBases(catalog, solo, view = 'solo') {
  if (view === 'solo') return solo.bases;
  return catalog.trees.map(name => {
    const base = catalog.classes.find(c => c.name === name);
    return { id: base.id, name, source_class_id: base.id, provisional_name: false,
      classes: catalog.classes.filter(c => c.tree === name && c.id !== base.id).map(c => ({ class_id: c.id, placement_status: 'historical' })) };
  });
}

export function soloMembership(solo, classId) {
  for (const base of solo.bases) {
    if (base.source_class_id === classId) return { base, placement_status: 'base' };
    const entry = base.classes.find(c => c.class_id === classId);
    if (entry) return { base, ...entry };
  }
  return null;
}

export function readTreeState(search, catalog, solo, classId = '') {
  const params = new URLSearchParams(search);
  const legacyTree = catalog.trees.includes(params.get('tree')) ? params.get('tree') : '';
  let view = params.get('view') === '2016' || (!params.has('view') && legacyTree) ? '2016' : 'solo';
  let base = params.get('base') || (legacyTree ? legacyTree.toLowerCase() : '');
  const entry = catalog.classes.find(c => c.id === classId);
  if (entry) {
    const membership = soloMembership(solo, classId);
    if (view === 'solo' && membership) base = membership.base.id;
    else { view = '2016'; base = entry.tree.toLowerCase(); }
  }
  if (!treeBases(catalog, solo, view).some(b => b.id === base)) base = '';
  return { view, base, query: params.get('query') || '',
    rank: view === '2016' && /^[1-7]$/.test(params.get('rank') || '') ? params.get('rank') : '',
    kind: view === '2016' && ['regular', 'hidden'].includes(params.get('kind')) ? params.get('kind') : '',
    visual: params.get('visual') === 'male' ? 'male' : 'female' };
}

export function treeQuery(state) {
  const params = new URLSearchParams({ view: state.view === '2016' ? '2016' : 'solo' });
  for (const key of ['base', 'query', 'rank', 'kind']) if (state[key] && (!(key === 'rank' || key === 'kind') || state.view === '2016')) params.set(key, state[key]);
  if (state.visual === 'male') params.set('visual', 'male');
  return '?' + params.toString();
}

export function branchEntries(catalog, base, state) {
  const entries = base.classes.map(position => ({ ...catalog.classes.find(c => c.id === position.class_id), placement_status: position.placement_status }));
  entries.sort(state.view === '2016' ? (a, b) => a.first_rank - b.first_rank || treeSortName(a, b) : treeSortName);
  return entries.filter(c => matchesCatalogEntry({ ...c, search: `${base.name} ${c.name} ${c.summary} ${c.skills.map(s => `${s.name} ${s.summary}`).join(' ')}` }, state));
}

export function renderTreeViews(state, root) {
  return `<div class="tree-view-switch" aria-label="Visão das classes">${[['solo', 'Projeto solo'], ['2016', 'Original de 2016']].map(([view, label]) => `<a data-tree-state data-view="${view}" href="${root}docs/classes/catalogo-2016.html${treeQuery({ view })}" ${state.view === view ? 'aria-current="page"' : ''}>${label}</a>`).join('')}</div>`;
}

export function renderTreeSidebar(catalog, solo, state, root, activeId = '') {
  return `<div class="tree-sidebar-heading">${state.view === 'solo' ? 'BASES DO PROJETO SOLO' : 'ÁRVORES DE 2016'}</div><ul class="tree-nav">${treeBases(catalog, solo, state.view).map(base => {
    const selected = state.base === base.id;
    const entries = base.classes.map(position => ({ ...catalog.classes.find(c => c.id === position.class_id), ...position })).sort(state.view === '2016' ? (a, b) => a.first_rank - b.first_rank || treeSortName(a, b) : treeSortName);
    return `<li><a data-tree-state data-base="${base.id}" class="tree-base-link${selected ? ' selected' : ''}" href="${root}docs/classes/catalogo-2016.html${treeQuery({ ...state, base: base.id })}#arvore" aria-expanded="${selected}" aria-controls="tree-nav-children-${base.id}" ${selected ? 'aria-current="true"' : ''}><img src="${root}docs/assets/classes/${base.source_class_id}/icon.png" width="26" height="26" alt=""><span>${treeEscape(base.name)}${base.provisional_name ? '<small>nome provisório</small>' : ''}</span><span class="tree-nav-count">${base.classes.length}</span></a>${`<ul id="tree-nav-children-${base.id}" ${selected ? '' : 'hidden'} aria-label="Especializações de ${treeEscape(base.name)}">${entries.map(c => `<li><a data-class-link="${c.id}" href="${root}docs/classes/catalogo/${c.id}.html${treeQuery({ ...state, base: base.id })}" ${activeId === c.id ? 'aria-current="page" class="current"' : ''}>${treeEscape(c.name)}${state.view === 'solo' ? `<span class="placement-dot ${c.placement_status}" title="${c.placement_status === 'defined' ? 'Posição definida' : 'Posição proposta'}" aria-label="${c.placement_status === 'defined' ? 'Posição definida' : 'Posição proposta'}"></span>` : ''}</a></li>`).join('')}</ul>`}</li>`;
  }).join('')}</ul>`;
}

export function renderBaseChoices(catalog, solo, state, root) {
  return treeBases(catalog, solo, state.view).map(base => `<a data-tree-state data-base="${base.id}" class="tree-choice${state.base === base.id ? ' selected' : ''}" href="${root}docs/classes/catalogo-2016.html${treeQuery({ ...state, base: base.id })}#arvore" ${state.base === base.id ? 'aria-current="true"' : ''}><img src="${root}docs/assets/classes/${base.source_class_id}/icon.png" alt="" width="48" height="48"><span><strong>${treeEscape(base.name)}</strong><small>${base.provisional_name ? 'Nome provisório · ' : ''}${base.classes.length} especializações</small></span><span aria-hidden="true">↓</span></a>`).join('');
}

export function treeVisibleCount(catalog, solo, state) {
  const bases = treeBases(catalog, solo, state.view).filter(b => !state.base || b.id === state.base);
  return { visible: bases.reduce((sum, b) => sum + branchEntries(catalog, b, state).length, 0), total: bases.reduce((sum, b) => sum + b.classes.length, 0) };
}

export function renderTreeBranches(catalog, solo, state, root) {
  return treeBases(catalog, solo, state.view).filter(b => !state.base || b.id === state.base).map(base => {
    const source = catalog.classes.find(c => c.id === base.source_class_id);
    const entries = branchEntries(catalog, base, state);
    return `<section class="class-tree" aria-labelledby="branch-${base.id}"><div class="tree-root-node"><img src="${root}docs/assets/classes/${base.source_class_id}/icon.png" alt="" width="42" height="42"><div><h2 id="branch-${base.id}">${treeEscape(base.name)}${base.provisional_name ? '<small>nome provisório</small>' : ''}</h2><p>${base.classes.length} opções na mesma base</p><a data-class-link="${source.id}" href="${root}docs/classes/catalogo/${source.id}.html${treeQuery({ ...state, base: base.id })}">Ver ficha ${state.view === 'solo' ? 'original ' : ''}de ${treeEscape(source.name)} →</a></div></div><div class="tree-children catalog-grid" role="list" aria-label="Especializações de ${treeEscape(base.name)}">${entries.map(c => {
      const label = state.view === '2016' ? `Rank histórico ${c.first_rank}${c.hidden ? ' · Oculta' : ''}` : c.placement_status === 'defined' ? 'Posição definida' : 'Posição proposta';
      return `<div role="listitem" class="tree-child" data-catalog-entry="${c.id}"><a class="catalog-card ${base.id}" data-class-link="${c.id}" href="${root}docs/classes/catalogo/${c.id}.html${treeQuery({ ...state, base: base.id })}"><div class="catalog-card-head"><img src="${root}docs/assets/classes/${c.id}/icon.png" alt="" width="40" height="40" loading="lazy"><span class="placement-label ${state.view === 'solo' ? c.placement_status : ''}">${label}</span></div><div class="catalog-sprite"><img data-catalog-sprite src="${root}docs/assets/classes/${c.id}/${state.visual}.gif" alt="${treeEscape(c.name)} · visual ${state.visual === 'male' ? 'masculino' : 'feminino'} oficial" loading="lazy" width="155" height="155"></div><h3>${treeEscape(c.name)}</h3><p>${treeEscape(c.summary)}</p><span class="catalog-card-foot">${c.skills.length} habilidades originais<span aria-hidden="true">↗</span></span></a></div>`;
    }).join('')}</div>${entries.length ? '' : '<p class="tree-no-results">Nenhuma especialização corresponde aos filtros nesta base.</p>'}</section>`;
  }).join('');
}

export function renderClassContext(catalog, solo, state, classId, root) {
  const c = catalog.classes.find(entry => entry.id === classId);
  if (!c) return '';
  const position = soloMembership(solo, classId);
  const base = treeBases(catalog, solo, state.view).find(b => b.id === state.base);
  const name = base?.name || c.tree;
  const label = state.view === '2016' ? `Original de 2016 · ${c.tree} · Rank ${c.first_rank}` : `${name}${base?.provisional_name ? ' (nome provisório)' : ''} · ${position?.placement_status === 'defined' || position?.placement_status === 'base' ? 'Posição definida' : 'Posição proposta'}`;
  const counterpart = position && state.view === '2016' ? `<a href="${root}docs/classes/catalogo/${classId}.html${treeQuery({ view: 'solo', base: position.base.id, visual: state.visual })}">Ver no projeto solo →</a>` : '';
  return `<p class="tree-breadcrumb"><a data-tree-state href="${root}docs/classes/catalogo-2016.html${treeQuery(state)}">${state.view === 'solo' ? 'Projeto solo' : 'Original de 2016'}</a> <span aria-hidden="true">/</span> ${treeEscape(name)} <span aria-hidden="true">/</span> ${treeEscape(c.name)}</p><div class="class-context"><strong>${treeEscape(label)}</strong><p>${state.view === 'solo' ? 'A posição na árvore é uma decisão de organização. As habilidades abaixo são referências de 2016; sua adaptação para o solo continua em estudo.' : 'Esta ficha preserva a árvore e as habilidades históricas. A presença no acervo não significa seleção para a versão solo.'}</p><div><a data-tree-state href="${root}docs/classes/catalogo-2016.html${treeQuery(state)}#arvore">← Voltar à árvore de ${treeEscape(name)}</a>${counterpart}</div></div>`;
}

export function renderTreePage(catalog, solo, state, root) {
  const count = treeVisibleCount(catalog, solo, state);
  return `<div class="catalog-heading"><span class="eyebrow">CLASSES, CAMINHOS E POSSIBILIDADES</span><h1 id="catalogo">Escolha uma base.<br><em>Descubra seus caminhos.</em></h1><p id="tree-intro">${state.view === 'solo' ? 'Três bases, 26 especializações. Uma distribuição em construção, com posições definidas e propostas.' : 'Quatro bases e 51 especializações do catálogo histórico de 2016, incluindo três classes ocultas.'}</p></div><div id="tree-views">${renderTreeViews(state, root)}</div><div id="tree-bases" class="tree-base-choices">${renderBaseChoices(catalog, solo, state, root)}</div><p class="tree-explanation">Os ramos mostram opções de uma mesma base. As ligações não representam evolução obrigatória entre especializações.</p><form id="catalog-filters" class="catalog-filters" role="search" aria-label="Filtrar especializações"><label class="catalog-query">Buscar classe, habilidade ou estilo<input type="search" name="query" value="${treeEscape(state.query)}" placeholder="Lança, veneno, Cyclone…" autocomplete="off"></label><label data-historical-filter ${state.view === 'solo' ? 'hidden' : ''}>Primeiro rank histórico<select name="rank" ${state.view === 'solo' ? 'disabled' : ''}><option value="">Todos os ranks</option>${Array.from({ length: 7 }, (_, i) => `<option value="${i + 1}" ${state.rank === String(i + 1) ? 'selected' : ''}>Rank ${i + 1}</option>`).join('')}</select></label><label data-historical-filter ${state.view === 'solo' ? 'hidden' : ''}>Tipo histórico<select name="kind" ${state.view === 'solo' ? 'disabled' : ''}><option value="">Todas as classes</option><option value="regular" ${state.kind === 'regular' ? 'selected' : ''}>Regulares</option><option value="hidden" ${state.kind === 'hidden' ? 'selected' : ''}>Ocultas</option></select></label><label>Visual<select name="visual"><option value="female" ${state.visual === 'female' ? 'selected' : ''}>Feminino</option><option value="male" ${state.visual === 'male' ? 'selected' : ''}>Masculino</option></select></label><button type="reset">Limpar filtros</button></form><div class="catalog-results"><p id="catalog-count" role="status" aria-live="polite">${count.visible} de ${count.total} especializações</p><a href="#recorte">Sobre as duas visões ↓</a></div><div id="arvore" tabindex="-1"><div id="tree-branches">${renderTreeBranches(catalog, solo, state, root)}</div></div><aside class="catalog-policy"><strong>Além desta proposta</strong><p>Monk, Paladin e outras classes não selecionadas continuam no acervo de 2016. Linker e Thaumaturge estão removidos da seleção solo. Classes posteriores ficam para outra etapa.</p><a href="${root}docs/projeto-solo/diretrizes.html">Consultar decisões e pendências →</a></aside><section class="catalog-method" id="recorte"><h2>Projeto solo e memória de 2016</h2><p>A visão solo organiza opções; não define ranks, pré-requisitos ou habilidades adaptadas. Warrior é um nome provisório. Na visão histórica, Scout continua em Archer e as quatro árvores mantêm seus ranks originais.</p><p>O acervo preserva 55 classes, 382 habilidades resumidas e 165 imagens originais da IMC. As habilidades vêm de páginas arquivadas; os arquivos visuais são do site oficial atual, com correspondência exata em 2016 ainda pendente.</p><a href="${root}docs/imagens.html">Procedência das imagens</a> · <a href="${root}docs/fontes.html">Fontes da pesquisa</a></section>`;
}
