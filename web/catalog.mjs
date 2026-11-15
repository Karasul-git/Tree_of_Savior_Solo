import { normalize } from './search.mjs';

export function catalogSearchText(entry) {
  return normalize(entry.search ?? `${entry.name} ${entry.tree} ${entry.summary} ${(entry.skills ?? []).map(skill => `${skill.name} ${skill.summary}`).join(' ')}`);
}

export function matchesCatalogEntry(entry, filters = {}) {
  if (filters.tree && entry.tree !== filters.tree) return false;
  if (filters.rank && Number(entry.first_rank ?? entry.rank) !== Number(filters.rank)) return false;
  const hidden = entry.hidden === true || entry.hidden === 'true';
  if (filters.kind === 'hidden' && !hidden) return false;
  if (filters.kind === 'regular' && hidden) return false;
  const terms = normalize(filters.query ?? '').trim().split(/\s+/).filter(Boolean);
  const haystack = catalogSearchText(entry);
  return terms.every(term => haystack.includes(term));
}
