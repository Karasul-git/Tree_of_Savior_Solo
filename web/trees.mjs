import { matchesCatalogEntry } from './catalog.mjs';

const treeEscape = value => String(value).replace(/[&<>"']/g, char => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[char]));
const treeSortName = (a, b) => a.name.localeCompare(b.name, 'pt-BR');
export function treeRecords(catalog) { return [...catalog.classes, ...(catalog.later_classes || [])]; }
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
    if (base.source_class_id === classId) return { base, placement_status: 'base', solo_rank: 1 };
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
  const entry = treeRecords(catalog).find(c => c.id === classId);
  if (entry) {
    const membership = soloMembership(solo, classId);
    if (membership && (view === 'solo' || !entry.first_rank)) { view = 'solo'; base = membership.base.id; }
    else { view = '2016'; base = entry.tree.toLowerCase(); }
  }
  if (!treeBases(catalog, solo, view).some(b => b.id === base)) base = '';
  return { view, base, query: params.get('query') || '',
    rank: /^[2-7]$/.test(params.get('rank') || '') ? params.get('rank') : '',
    kind: view === '2016' && ['regular', 'hidden'].includes(params.get('kind')) ? params.get('kind') : '',
    visual: params.get('visual') === 'male' ? 'male' : 'female' };
}
export function treeQuery(state) {
  const params = new URLSearchParams({ view: state.view === '2016' ? '2016' : 'solo' });
  for (const key of ['base', 'query', 'rank', 'kind']) if (state[key] && (key !== 'kind' || state.view === '2016')) params.set(key, state[key]);
  if (state.visual === 'male') params.set('visual', 'male');
  return '?' + params.toString();
}
export function branchEntries(catalog, base, state) {
  const records = treeRecords(catalog);
  const entries = base.classes.map(position => {
    const c = records.find(c => c.id === position.class_id);
    return { ...c, ...position, display_rank: state.view === 'solo' ? position.solo_rank : c.first_rank };
  });
  entries.sort((a, b) => (a.display_rank ?? 8) - (b.display_rank ?? 8) || treeSortName(a, b));
  return entries.filter(c => matchesCatalogEntry({ ...c, first_rank: c.display_rank,
    search: `${base.name} ${c.name} ${c.summary} ${c.skills.map(s => `${s.name} ${s.summary}`).join(' ')}` }, state));
}
export function treeRankCoverage(catalog, base, view = 'solo') {
  const entries = branchEntries(catalog, base, { view });
  return Array.from({ length: 6 }, (_, i) => {
    const rank = i + 2;
    const choices = entries.filter(c => c.display_rank === rank);
    return { rank, count: choices.length, proposed: view === 'solo' && choices.some(c => c.rank_status === 'proposed') };
  });
}
export function treeIntro(catalog, solo, view) {
  return view === 'solo'
    ? `Três bases, ${solo.bases.reduce((n, b) => n + b.classes.length, 0)} especializações organizadas por rank. Posições e ranks propostos continuam em estudo.`
    : 'Quatro bases e 51 especializações do catálogo histórico de 2016, incluindo três classes ocultas.';
}
export function renderTreeViews(state, root) {
  return `<div class="tree-view-switch" aria-label="Visão das classes">${[['solo', 'Projeto solo'], ['2016', 'Original de 2016']].map(([view, label]) => `<a data-tree-state data-view="${view}" href="${root}docs/classes/catalogo-2016.html${treeQuery({ view })}" ${state.view === view ? 'aria-current="page"' : ''}>${label}</a>`).join('')}</div>`;
}
export function renderTreeSidebar(catalog, solo, state, root, activeId = '') {
  return `<div class="tree-sidebar-heading">${state.view === 'solo' ? 'BASES DO PROJETO SOLO' : 'ÁRVORES DE 2016'}</div><ul class="tree-nav">${treeBases(catalog, solo, state.view).map(base => {
    const selected = state.base === base.id;
    const entries = branchEntries(catalog, base, { view: state.view });
    let previousRank;
    const children = entries.map(c => {
      const heading = previousRank !== c.display_rank ? `<li class="tree-nav-rank">Rank ${c.display_rank}</li>` : '';
      previousRank = c.display_rank;
      return `${heading}<li><a data-class-link="${c.id}" href="${root}docs/classes/catalogo/${c.id}.html${treeQuery({ ...state, base: base.id })}" ${activeId === c.id ? 'aria-current="page" class="current"' : ''}>${treeEscape(c.name)}${state.view === 'solo' ? `<span class="placement-dot ${c.placement_status}" title="${c.placement_status === 'defined' ? 'Posição definida' : 'Posição proposta'}" aria-label="${c.placement_status === 'defined' ? 'Posição definida' : 'Posição proposta'}"></span>` : ''}</a></li>`;
    }).join('');
    return `<li><a data-tree-state data-base="${base.id}" class="tree-base-link${selected ? ' selected' : ''}" href="${root}docs/classes/catalogo-2016.html${treeQuery({ ...state, base: base.id })}#arvore" aria-expanded="${selected}" aria-controls="tree-nav-children-${base.id}" ${selected ? 'aria-current="true"' : ''}><img src="${root}docs/assets/classes/${base.source_class_id}/icon.png" width="26" height="26" alt=""><span>${treeEscape(base.name)}${base.provisional_name ? '<small>nome provisório</small>' : ''}</span><span class="tree-nav-count">${base.classes.length}</span></a><ul id="tree-nav-children-${base.id}" ${selected ? '' : 'hidden'} aria-label="Especializações de ${treeEscape(base.name)}">${children}</ul></li>`;
  }).join('')}</ul>`;
}
export function renderBaseChoices(catalog, solo, state, root) {
  return treeBases(catalog, solo, state.view).map(base => `<a data-tree-state data-base="${base.id}" class="tree-choice${state.base === base.id ? ' selected' : ''}" href="${root}docs/classes/catalogo-2016.html${treeQuery({ ...state, base: base.id })}#arvore" ${state.base === base.id ? 'aria-current="true"' : ''}><img src="${root}docs/assets/classes/${base.source_class_id}/icon.png" alt="" width="48" height="48"><span><strong>${treeEscape(base.name)}</strong><small>${base.provisional_name ? 'Nome provisório · ' : ''}${base.classes.length} especializações</small></span><span aria-hidden="true">↓</span></a>`).join('');
}
export function treeVisibleCount(catalog, solo, state) {
  const bases = treeBases(catalog, solo, state.view).filter(b => !state.base || b.id === state.base);
  return { visible: bases.reduce((sum, b) => sum + branchEntries(catalog, b, state).length, 0), total: bases.reduce((sum, b) => sum + b.classes.length, 0) };
}
export function renderRankCoverage(catalog, solo, state, root) {
  const bases = treeBases(catalog, solo, state.view);
  return `<section class="rank-coverage" aria-labelledby="coverage-heading"><h2 id="coverage-heading">Opções em cada rank</h2><p>Rank 1 é a base. Contagem completa, antes dos filtros. * Inclui rank solo proposto.</p><div class="table-scroll" tabindex="0" role="region" aria-label="Cobertura de ranks"><table><thead><tr><th scope="col">Base</th>${Array.from({length:6}, (_,i) => `<th scope="col">R${i+2}</th>`).join('')}</tr></thead><tbody>${bases.map(b => `<tr><th scope="row"><a data-tree-state data-base="${b.id}" href="${root}docs/classes/catalogo-2016.html${treeQuery({view:state.view,base:b.id})}#arvore">${treeEscape(b.name)}</a></th>${treeRankCoverage(catalog,b,state.view).map(r => `<td class="${r.count ? '' : 'rank-gap'}">${r.count || 'Lacuna'}${r.proposed ? '*' : ''}</td>`).join('')}</tr>`).join('')}</tbody></table></div>${state.view === 'solo' ? `<p>Scout tinha lacunas em R2–R4. A hipótese antecipa Rogue de R5 para R4 e propõe Assassin R2 e Outlaw R3. <a href="${root}docs/projeto-solo/ranks-e-especializacoes.html">Ver estudo e alternativas →</a></p>` : ''}</section>`;
}
export function renderTreeBranches(catalog, solo, state, root) {
  return treeBases(catalog, solo, state.view).filter(b => !state.base || b.id === state.base).map(base => {
    const source = catalog.classes.find(c => c.id === base.source_class_id);
    const entries = branchEntries(catalog, base, state);
    const coverage = treeRankCoverage(catalog, base, state.view);
    const missing = coverage.filter(r => !r.count).map(r => 'R' + r.rank);
    const ranks = state.rank ? [Number(state.rank)] : Array.from({length:6},(_,i)=>i+2);
    const filtered = Boolean(state.rank || state.query || state.kind);
    const groups = ranks.map(rank => {
      const choices = entries.filter(c => c.display_rank === rank);
      if (!choices.length && filtered) return '';
      return `<section class="tree-rank" data-rank="${rank}" aria-labelledby="rank-${base.id}-${rank}"><h3 id="rank-${base.id}-${rank}">Rank ${rank}<span>${choices.length} ${choices.length === 1 ? 'opção' : 'opções'}</span></h3><div class="catalog-grid" role="list">${choices.map(c => {
        const label = state.view === '2016' ? `Rank histórico ${c.first_rank}${c.hidden ? ' · Oculta' : ''}` : `${c.placement_status === 'defined' ? 'Posição definida' : 'Posição proposta'} · ${c.rank_status === 'proposed' ? 'Rank solo proposto' : 'Rank de referência'}`;
        const provenance = c.first_rank ? `Original: ${c.tree} · R${c.first_rank}` : 'Posterior: Re:Build 2019 · sem rank antigo';
        return `<div role="listitem" class="tree-child" data-catalog-entry="${c.id}"><a class="catalog-card ${base.id}" data-class-link="${c.id}" href="${root}docs/classes/catalogo/${c.id}.html${treeQuery({ ...state, base: base.id })}"><div class="catalog-card-head"><img src="${root}docs/assets/classes/${c.id}/icon.png" alt="" width="40" height="40" loading="lazy"><span class="placement-label ${state.view === 'solo' ? c.placement_status : ''}">${label}</span></div><div class="catalog-sprite"><img data-catalog-sprite src="${root}docs/assets/classes/${c.id}/${state.visual}.gif" alt="${treeEscape(c.name)} · visual ${state.visual === 'male' ? 'masculino' : 'feminino'} oficial" loading="lazy" width="155" height="155"></div><h4>${treeEscape(c.name)}</h4><p>${treeEscape(c.summary)}</p>${state.view === 'solo' ? `<small class="class-origin">${provenance}</small>` : ''}<span class="catalog-card-foot">${c.skills.length} habilidades · ${c.first_rank ? 'referência 2016' : 'página oficial atual'}<span aria-hidden="true">↗</span></span></a></div>`;
      }).join('')}</div>${choices.length ? '' : '<p class="rank-gap-note">Sem especialização neste rank.</p>'}</section>`;
    }).join('');
    return `<section class="class-tree" aria-labelledby="branch-${base.id}"><div class="tree-root-node"><img src="${root}docs/assets/classes/${base.source_class_id}/icon.png" alt="" width="42" height="42"><div><h2 id="branch-${base.id}">${treeEscape(base.name)}${base.provisional_name ? '<small>nome provisório</small>' : ''}</h2><p>Rank 1 · ${base.classes.length} especializações · ${missing.length ? 'Lacunas: '+missing.join(', ') : 'Opções de R2 a R7'}</p><a data-class-link="${source.id}" href="${root}docs/classes/catalogo/${source.id}.html${treeQuery({ ...state, base: base.id })}">Ver ficha ${state.view === 'solo' ? 'original ' : ''}de ${treeEscape(source.name)} →</a></div></div><div class="tree-children">${groups}</div>${entries.length ? '' : '<p class="tree-no-results">Nenhuma especialização corresponde aos filtros nesta base.</p>'}</section>`;
  }).join('');
}
export function renderClassContext(catalog, solo, state, classId, root) {
  const c = treeRecords(catalog).find(entry => entry.id === classId);
  if (!c) return '';
  const position = soloMembership(solo, classId);
  const base = treeBases(catalog, solo, state.view).find(b => b.id === state.base);
  const name = base?.name || c.tree;
  const label = state.view === '2016' ? `Original de 2016 · ${c.tree} · Rank ${c.first_rank}` : `${name}${base?.provisional_name ? ' (nome provisório)' : ''} · ${position?.placement_status === 'defined' || position?.placement_status === 'base' ? 'Posição definida' : 'Posição proposta'} · Rank solo ${position?.solo_rank}${position?.rank_status === 'proposed' ? ' proposto' : ''}`;
  const origin = c.first_rank ? `Referência original: ${c.tree}, Rank ${c.first_rank} em 2016.` : 'Classe adicionada em 2019, após a remoção de ranks e círculos. Habilidades da página oficial atual.';
  const counterpart = position && state.view === '2016' ? `<a href="${root}docs/classes/catalogo/${classId}.html${treeQuery({ view: 'solo', base: position.base.id, visual: state.visual })}">Ver no projeto solo →</a>` : '';
  return `<p class="tree-breadcrumb"><a data-tree-state href="${root}docs/classes/catalogo-2016.html${treeQuery(state)}">${state.view === 'solo' ? 'Projeto solo' : 'Original de 2016'}</a> <span aria-hidden="true">/</span> ${treeEscape(name)} <span aria-hidden="true">/</span> ${treeEscape(c.name)}</p><div class="class-context"><strong>${treeEscape(label)}</strong><p>${state.view === 'solo' ? `${origin} Posição e rank organizam a proposta; habilidades, círculos e requisitos solo continuam em estudo.` : 'Esta ficha preserva a árvore e as habilidades históricas. A presença no acervo não significa seleção para a versão solo.'}</p><div><a data-tree-state href="${root}docs/classes/catalogo-2016.html${treeQuery(state)}#arvore">← Voltar à árvore de ${treeEscape(name)}</a>${counterpart}</div></div>`;
}
export function renderTreePage(catalog, solo, state, root) {
  const count = treeVisibleCount(catalog, solo, state);
  return `<div class="catalog-heading"><span class="eyebrow">CLASSES, CAMINHOS E POSSIBILIDADES</span><h1 id="catalogo">Escolha uma base.<br><em>Descubra seus caminhos.</em></h1><p id="tree-intro">${treeIntro(catalog, solo, state.view)}</p></div><div id="tree-views">${renderTreeViews(state, root)}</div><div id="tree-bases" class="tree-base-choices">${renderBaseChoices(catalog, solo, state, root)}</div><p class="tree-explanation">A base ocupa o Rank 1; as especializações aparecem por primeiro rank disponível. Escolhas de uma mesma base não representam evolução obrigatória entre si. Os círculos solo ainda serão definidos.</p><div id="rank-coverage">${renderRankCoverage(catalog, solo, state, root)}</div><form id="catalog-filters" class="catalog-filters" role="search" aria-label="Filtrar especializações"><label class="catalog-query">Buscar classe, habilidade ou estilo<input type="search" name="query" value="${treeEscape(state.query)}" placeholder="Lança, veneno, Cyclone…" autocomplete="off"></label><label><span id="rank-filter-label">${state.view === 'solo' ? 'Rank da proposta solo' : 'Primeiro rank histórico'}</span><select name="rank"><option value="">Todos os ranks</option>${Array.from({ length: 6 }, (_, i) => `<option value="${i + 2}" ${state.rank === String(i + 2) ? 'selected' : ''}>Rank ${i + 2}</option>`).join('')}</select></label><label data-historical-filter ${state.view === 'solo' ? 'hidden' : ''}>Tipo histórico<select name="kind" ${state.view === 'solo' ? 'disabled' : ''}><option value="">Todas as classes</option><option value="regular" ${state.kind === 'regular' ? 'selected' : ''}>Regulares</option><option value="hidden" ${state.kind === 'hidden' ? 'selected' : ''}>Ocultas</option></select></label><label>Visual<select name="visual"><option value="female" ${state.visual === 'female' ? 'selected' : ''}>Feminino</option><option value="male" ${state.visual === 'male' ? 'selected' : ''}>Masculino</option></select></label><button type="reset">Limpar filtros</button></form><div class="catalog-results"><p id="catalog-count" role="status" aria-live="polite">${count.visible} de ${count.total} especializações</p><a href="#recorte">Sobre as duas visões ↓</a></div><div id="arvore" tabindex="-1"><div id="tree-branches">${renderTreeBranches(catalog, solo, state, root)}</div></div><aside class="catalog-policy"><strong>Novos encaixes em estudo</strong><p>Monk e Paladin em Warrior; Assassin e Outlaw em Scout. A seleção não aprova habilidades mágicas. Linker e Thaumaturge continuam removidos.</p><a href="${root}docs/projeto-solo/ranks-e-especializacoes.html">Ler o estudo de ranks e adaptações →</a></aside><section class="catalog-method" id="recorte"><h2>Projeto solo e memória de 2016</h2><p>Os ranks solo são uma proposta de organização até R7, sem pré-requisitos ou círculos definidos. Warrior é um nome provisório. Na visão histórica, Scout continua em Archer e as quatro árvores mantêm seus ranks originais.</p><p>O acervo de 2016 preserva 55 classes, 382 habilidades resumidas e 165 imagens originais da IMC. Assassin e Outlaw ficam em um conjunto posterior separado, com 13 resumos de habilidades atuais e seis imagens oficiais adicionais. A identidade exata dos arquivos nas versões antigas ainda não foi comprovada.</p><a href="${root}docs/imagens.html">Procedência das imagens</a> · <a href="${root}docs/fontes.html">Fontes da pesquisa</a></section>`;
}
