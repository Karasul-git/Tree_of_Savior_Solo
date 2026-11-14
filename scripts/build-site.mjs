import { readFile, writeFile, mkdir, rm, cp } from 'node:fs/promises';
import path from 'node:path';
import { fileURLToPath } from 'node:url';
import { Marked } from 'marked';

const root = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '..');
const dist = path.join(root, 'dist');
const repoURL = 'https://github.com/Karasul-git/Tree_of_Savior_Solo';
const classes = [
  { slug: 'swordsman', name: 'Swordsman', pt: 'Espadachim', note: 'O primeiro passo na linha de frente.', role: 'COMBATE PRÓXIMO', count: 8, color: 'red' },
  { slug: 'wizard', name: 'Wizard', pt: 'Mago', note: 'Magia, controle e novas possibilidades.', role: 'MAGIA E CONTROLE', count: 8, color: 'blue' },
  { slug: 'archer', name: 'Archer', pt: 'Arqueiro', note: 'Distância, precisão e movimento.', role: 'COMBATE À DISTÂNCIA', count: 7, color: 'green' },
  { slug: 'cleric', name: 'Cleric', pt: 'Clérigo', note: 'Cura e proteção pelo caminho.', role: 'CURA E PROTEÇÃO', count: 7, color: 'gold' },
];
const documents = [
  ['README.md', 'Início'],
  ['docs/classes/README.md', 'Classes iniciais'],
  ...classes.map(c => [`docs/classes/${c.slug}.md`, `${c.name} · ${c.pt}`]),
  ['docs/sistemas/ranks-e-circulos.md', 'Ranks e círculos'],
  ['docs/recorte-historico.md', 'O lançamento de 2016'],
  ['docs/fontes.md', 'Fontes da pesquisa'],
  ['docs/imagens.md', 'Imagens originais'],
  ['ROADMAP.md', 'Próximas etapas'],
  ['CONTRIBUTING.md', 'Como contribuir'],
  ['docs/versao-navegavel.md', 'Sobre a versão navegável'],
];
const outPath = source => source.replace(/(^|\/)README\.md$/, '$1index.html').replace(/\.md$/, '.html');
const escape = text => String(text).replace(/[&<>"']/g, char => ({ '&': '&amp;', '<': '&lt;', '>': '&gt;', '"': '&quot;', "'": '&#39;' }[char]));
const strip = html => html.replace(/<[^>]*>/g, ' ').replace(/&[^;]+;/g, ' ').replace(/\s+/g, ' ').trim();
const slugify = text => strip(text).normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase().replace(/[^a-z0-9]+/g, '-').replace(/^-|-$/g, '');

function shell(title, current, content, toc = [], classInfo) {
  const prefix = path.posix.relative(path.posix.dirname(current), '.') || '.';
  const base = prefix + '/';
  const link = (file, text, extra = '') => `<a href="${base + file}" ${current === file ? 'aria-current="page" class="current"' : ''} ${extra}>${text}</a>`;
  const group = (label, items) => `<div class="nav-group"><p>${label}</p>${items.join('')}</div>`;
  const nav = [
    group('O ACERVO', [link('index.html', '<span>⌂</span> Visão geral'), link('docs/classes/index.html', '<span>✦</span> Classes iniciais')]),
    group('ESCOLHA UM CAMINHO', classes.map(c => link(`docs/classes/${c.slug}.html`, `<img src="${base}docs/assets/classes/${c.slug}/icon.png" alt="" width="27" height="27"> ${c.name}`))),
    group('ENTENDA A ÉPOCA', [link('docs/sistemas/ranks-e-circulos.html', 'Ranks e círculos'), link('docs/recorte-historico.html', 'O lançamento de 2016')]),
    group('PRESERVAR A MEMÓRIA', [link('docs/fontes.html', 'Fontes da pesquisa'), link('docs/imagens.html', 'Imagens originais'), link('ROADMAP.html', 'Próximas etapas'), link('CONTRIBUTING.html', 'Como contribuir')]),
  ].join('');
  return `<!doctype html>
<html lang="pt-BR" data-root="${base}">
<head><meta charset="utf-8"><meta name="viewport" content="width=device-width,initial-scale=1"><meta name="color-scheme" content="light">
<title>${escape(title)} · Tree of Savior — Memórias</title><meta name="description" content="Wiki histórica em português sobre as classes e o mundo de Tree of Savior no lançamento internacional de 2016, com imagens oficiais e fontes documentadas.">
<link rel="icon" href="${base}docs/assets/classes/cleric/icon.png"><link rel="stylesheet" href="${base}assets/style.css">
<script src="${base}search-index.js" defer></script><script src="${base}assets/app.js" defer></script></head>
<body><a class="skip-link" href="#conteudo">Ir para o conteúdo</a>
<aside id="sidebar" class="sidebar"><a class="brand" href="${base}index.html"><img src="${base}docs/assets/classes/cleric/icon.png" width="45" height="45" alt=""><span>TREE OF SAVIOR<small>MEMÓRIAS DO LANÇAMENTO</small></span></a>
<div class="edition"><span class="status-dot"></span> iToS · Edição 2016 <span>v0.2</span></div>
<nav aria-label="Navegação principal">${nav}</nav><div class="sidebar-bottom"><span>Um mundo que vale guardar.</span><a href="${repoURL}" target="_blank" rel="noopener noreferrer">Projeto no GitHub ↗</a></div></aside>
<button id="menu-shade" tabindex="-1" aria-label="Fechar menu"></button>
<div class="page-shell"><header class="topbar"><div class="topbar-left"><button id="menu-toggle" class="icon-button" aria-label="Abrir menu" aria-controls="sidebar" aria-expanded="false">☰</button><span class="breadcrumb">O acervo <span>/</span> <strong>${escape(title)}</strong></span></div>
<button class="search-button" data-search-open><span aria-hidden="true">⌕</span> Buscar no acervo <kbd>/</kbd></button></header>
<main id="conteudo" tabindex="-1" class="${current === 'index.html' ? 'home' : 'document-layout'}">
${current !== 'index.html' ? `<div class="document-top"><span class="eyebrow">${classInfo ? 'CLASSES DO LANÇAMENTO · RANK 1' : 'BIBLIOTECA · EDIÇÃO HISTÓRICA'}</span><span class="document-badge">${classInfo ? 'Ficha inicial' : 'Acervo em construção'}</span></div>` : ''}
${current !== 'index.html' ? `<div class="article-grid"><article class="prose">${content}</article><aside class="toc" aria-label="Nesta página"><p>NESTA PÁGINA</p>${toc.map(item => `<a href="#${item.id}">${escape(item.title)}</a>`).join('')}<div class="toc-note">História com fontes.<br>Incertezas à vista.</div></aside></div>` : content}
</main><footer class="footer"><span>Tree of Savior · Memórias do lançamento</span><span>Projeto de fãs · Imagens © IMC Games</span><a href="${base}docs/versao-navegavel.html">Sobre esta versão</a><button id="back-top">Voltar ao topo ↑</button></footer></div>
<dialog id="search-dialog" aria-labelledby="search-title"><div class="search-dialog-heading"><h2 id="search-title">Explore o acervo</h2><button class="icon-button" data-search-close aria-label="Fechar busca">×</button></div><label class="sr-only" for="search-input">Buscar uma classe, habilidade ou assunto</label><input id="search-input" type="search" placeholder="Classe, habilidade, círculo…" autocomplete="off"><p id="search-status" role="status" aria-live="polite">Procure uma classe, habilidade ou assunto da wiki.</p><ul id="search-results"></ul><p class="search-hint">Busca nos documentos desta edição · <kbd>Esc</kbd> para fechar</p></dialog>
</body></html>`;
}

function home() {
  return `<section class="hero"><div class="hero-copy"><div class="eyebrow"><span class="tiny-leaf">❧</span> ARQUIVO HISTÓRICO · 2016</div><h1>Um mundo inteiro.<br><em>Um novo começo.</em></h1><p>Reencontre o Tree of Savior do lançamento.<br>Os primeiros caminhos, as antigas escolhas e a vontade<br class="wide-break"> de descobrir o que havia no próximo mapa.</p><div class="hero-actions"><a class="button primary" href="docs/classes/index.html">Explorar as classes <span>↗</span></a><a class="text-link" href="docs/recorte-historico.html">Conheça esta edição <span>→</span></a></div><div class="hero-caption"><span class="status-dot"></span> Uma memória de cada vez. Uma fonte para cada descoberta.</div></div>
<div class="hero-art" aria-label="Personagens oficiais de Cleric e Swordsman"><div class="art-ring"></div><span class="art-star star-one">✦</span><span class="art-star star-two">✧</span><div class="art-land"></div><img class="hero-cleric" src="docs/assets/classes/cleric/female.gif" alt="Cleric, personagem oficial"><img class="hero-swordsman" src="docs/assets/classes/swordsman/female.gif" alt="Swordsman, personagem oficial"><div class="art-label"><span>AS PRIMEIRAS ESCOLHAS</span><strong>É aqui que tudo começa.</strong></div></div></section>
<section class="archive-strip" aria-label="Cobertura da edição"><div><strong>04</strong><span>classes iniciais</span></div><div><strong>30</strong><span>habilidades catalogadas</span></div><div><strong>09</strong><span>fontes documentadas</span></div><a href="docs/recorte-historico.html"><span>MARCO DA EDIÇÃO</span><strong>29 de março, 2016 <span>↗</span></strong></a></section>
<section class="classes-section"><div class="section-heading"><div><span class="eyebrow">A PRIMEIRA ESCOLHA</span><h2>Qual será o seu caminho?</h2></div><span class="section-note">Quatro árvores. Muitas histórias.</span></div><div class="class-grid">${classes.map((c, i) => `<a class="class-card ${c.color}" href="docs/classes/${c.slug}.html"><div class="class-card-top"><span>0${i + 1} / RANK 1</span><img src="docs/assets/classes/${c.slug}/icon.png" alt="Emblema de ${c.name}" width="54" height="54"></div><div class="card-character"><span class="character-halo"></span><img src="docs/assets/classes/${c.slug}/${c.slug === 'wizard' ? 'male' : 'female'}.gif" alt="Visual oficial de ${c.name}" loading="lazy"></div><span class="class-role">${c.role}</span><h3>${c.name}</h3><p>${c.note}</p><div class="class-card-bottom"><span>C1 – C3 <span>·</span> ${c.count} habilidades</span><span class="card-arrow">↗</span></div></a>`).join('')}</div></section>
<section class="reading-grid"><a class="reading-card" href="docs/sistemas/ranks-e-circulos.html"><span class="reading-number">I.</span><div><span class="eyebrow">AS REGRAS DE OUTRO TEMPO</span><h3>Ranks, círculos e possibilidades</h3><p>Entenda como cada avanço construía um personagem diferente.</p></div><span>→</span></a><a class="reading-card" href="docs/fontes.html"><span class="reading-number">II.</span><div><span class="eyebrow">PRESERVAR COM CUIDADO</span><h3>De onde vêm as lembranças?</h3><p>Guias da época, imagens oficiais e dúvidas que continuam abertas.</p></div><span>→</span></a></section>
<aside class="archive-note"><span class="note-symbol">✧</span><div><strong>Um acervo vivo, com a história no lugar certo.</strong><p>Esta primeira edição cobre as quatro classes iniciais. Dados do beta, patches posteriores e adaptações solo são identificados separadamente. <a href="ROADMAP.html">Veja os próximos capítulos →</a></p></div></aside>`;
}

await rm(dist, { recursive: true, force: true });
await mkdir(path.join(dist, 'assets'), { recursive: true });
await cp(path.join(root, 'docs/assets'), path.join(dist, 'docs/assets'), { recursive: true });
await cp(path.join(root, 'data'), path.join(dist, 'data'), { recursive: true });
await cp(path.join(root, 'web/style.css'), path.join(dist, 'assets/style.css'));
const searchCode = (await readFile(path.join(root, 'web/search.mjs'), 'utf8')).replaceAll('export function', 'function');
const appCode = (await readFile(path.join(root, 'web/app.js'), 'utf8')).replace(/^import .*;\n/, '');
await writeFile(path.join(dist, 'assets/app.js'), searchCode + '\n' + appCode);
await writeFile(path.join(dist, '.nojekyll'), '');
const searchIndex = [];

for (const [source, title] of documents) {
  const current = outPath(source);
  const markdown = await readFile(path.join(root, source), 'utf8');
  const parser = new Marked({ gfm: true, breaks: false });
  parser.use({ walkTokens(token) {
    const key = token.type === 'link' ? 'href' : token.type === 'image' ? 'href' : null;
    if (!key || !token[key] || /^(?:[a-z][a-z0-9+.-]*:|\/\/|#)/i.test(token[key])) return;
    const [local, anchor] = token[key].split('#');
    const resolved = path.posix.normalize(path.posix.join(path.posix.dirname(source), local));
    let target = local.endsWith('/') ? resolved.replace(/\/$/, '') + '/index.html' : outPath(resolved);
    token[key] = (path.posix.relative(path.posix.dirname(current), target) || path.posix.basename(target)) + (anchor ? '#' + anchor : '');
  }});
  let content = parser.parse(markdown);
  const toc = [];
  const ids = new Set();
  content = content.replace(/<h([1-6])>([\s\S]*?)<\/h\1>/g, (_, level, text) => {
    let id = slugify(text);
    while (ids.has(id)) id += '-2';
    ids.add(id);
    if (level === '2') toc.push({ id, title: strip(text) });
    return `<h${level} id="${id}">${text}</h${level}>`;
  });
  content = content.replace(/<table>/g, '<div class="table-scroll" tabindex="0" role="region" aria-label="Tabela de referência"><table>').replace(/<\/table>/g, '</table></div>');
  const classInfo = classes.find(c => source === `docs/classes/${c.slug}.md`);
  if (classInfo) content = content.replace('<p><img', '<p class="class-emblem"><img');
  const destination = path.join(dist, current);
  await mkdir(path.dirname(destination), { recursive: true });
  await writeFile(destination, shell(title, current, current === 'index.html' ? home() : content, toc, classInfo));
  searchIndex.push({ title, path: current, text: strip(content) });
}

const manifest = JSON.parse(await readFile(path.join(root, 'data/assets.json'), 'utf8'));
for (const c of classes) {
  const current = `docs/assets/classes/${c.slug}/index.html`;
  const assets = manifest.filter(asset => asset.class_id === c.slug);
  const content = `<h1>Acervo visual · ${c.name}</h1><p>Arquivos originais da IMC Games preservados nesta edição. A identidade dos arquivos com a versão de 2016 ainda não foi comprovada.</p><p><a href="../../../classes/${c.slug}.html">Voltar à ficha de ${c.name}</a> · <a href="../../../imagens.html">Procedência das imagens</a></p><div class="asset-gallery">${assets.map(asset => `<figure><img src="${path.posix.basename(asset.path)}" alt="${escape(c.name + ' · ' + asset.role)}"><figcaption>${escape(asset.role)} · ${asset.bytes.toLocaleString('pt-BR')} bytes<br><a href="${escape(asset.source_url)}">Origem oficial ↗</a></figcaption></figure>`).join('')}</div>`;
  await writeFile(path.join(dist, current), shell(`Imagens de ${c.name}`, current, content));
}
await writeFile(path.join(dist, 'search-index.js'), 'window.WIKI_SEARCH=' + JSON.stringify(searchIndex).replaceAll('<', '\\u003c') + ';\n');
console.log(`Site gerado em dist/: ${documents.length + classes.length} páginas, ${manifest.length} imagens originais, busca local.`);
