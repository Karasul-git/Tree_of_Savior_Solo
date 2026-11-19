import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url), 'utf8'));
const maps = read('data/research/tosbase-2016-maps.json').entries;
const npcs = read('data/research/tosbase-2016-npcs.json').entries;
const abilities = read('data/research/tosbase-2016-abilities.json').entries;
const collections = read('data/research/tosbase-2016-collections.json').entries;
const effects = read('data/research/tosbase-2016-status-effects.json').entries;
const index = read('data/original-2016/world2-index.json');

test('o mapa Kadumel Cliff confere com a página capturada em 07/04/2016', () => {
  const m = maps['1781'];
  assert.deepEqual([m.name, m.area, m.type, m.level], ['Kadumel Cliff', 'Kaliss Province', 'Field', 250]);
  assert.deepEqual(m.connected.map(c => c.map_id), [1771, 1791]);
  assert.deepEqual(m.monsters[0], { monster_id: 57873, name: 'White Wendigo Searcher', population: 12, respawn_seconds: 30 });
});

test('NPC, atributo, coleção e efeito de estado conferem com as páginas capturadas', () => {
  assert.deepEqual([npcs['152016'].name, npcs['152016'].locations[0].map_id], ['Skull Pile_3', 3350]);
  const a = abilities['202013'];
  assert.deepEqual([a.name, a.class, a.circle, a.max_level, a.skill], ['Hell Breath: Enhance', 'Pyromancer', 3, 100, 'Hell Breath']);
  assert.deepEqual(collections['155'].items, [640040, 645186, 649205, 645402, 645401]);
  assert.equal(effects['158'].entries[0].description, 'Reduced INT and SPR.');
});

test('todas as entradas têm captura de 2016 e página no site', () => {
  for (const group of [maps, npcs, abilities, collections, effects]) for (const v of Object.values(group)) assert.match(v.captured_at, /^2016\d{10}$/);
  const folders = { maps: 'mapas', npcs: 'npcs', abilities: 'atributos', collections: 'colecoes', effects: 'efeitos' };
  for (const [key, folder] of Object.entries(folders)) for (const e of index[key]) assert.ok(existsSync(new URL(`../dist/docs/original-2016/${folder}/${e.id}.html`, import.meta.url)), `${folder}/${e.id}`);
});

test('as páginas se ligam: monstro ao mapa, quest ao NPC e item à coleção', () => {
  const quest = readFileSync(new URL('../docs/original-2016/quests/50011.md', import.meta.url), 'utf8');
  assert.match(quest, /\(\.\.\/npcs\/20027\.md\)/);
  const mapPage = readFileSync(new URL('../docs/original-2016/mapas/1781.md', import.meta.url), 'utf8');
  assert.match(mapPage, /\(\.\.\/monstros\/57873\.md\)/);
});
