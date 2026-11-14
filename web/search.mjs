export function normalize(text) {
  return text.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLocaleLowerCase('pt-BR');
}

export function findPages(pages, query) {
  const terms = normalize(query).trim().split(/\s+/).filter(Boolean);
  if (!terms.length) return [];
  return pages.map(page => {
    const title = normalize(page.title);
    const body = normalize(page.text);
    if (!terms.every(term => `${title} ${body}`.includes(term))) return null;
    const score = terms.reduce((sum, term) => sum + (title.includes(term) ? 10 : 1), 0);
    const position = body.indexOf(terms[0]);
    const start = Math.max(0, position - 50);
    return { ...page, score, snippet: `${start ? '…' : ''}${page.text.slice(start, start + 170)}…` };
  }).filter(Boolean).sort((a, b) => b.score - a.score).slice(0, 10);
}
