import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync } from 'node:fs';

const read = file => JSON.parse(readFileSync(new URL('../' + file, import.meta.url), 'utf8'));
const tosbase = read('data/research/tosbase-2016-skills.json').skills;
const swordsman = read('data/classes/swordsman.json').skills;

test('o leitor reproduz as oito tabelas de Swordsman já conferidas no acervo', () => {
  const byName = Object.fromEntries(Object.values(tosbase).map(s => [s.name, s]));
  for (const old of swordsman) {
    const read2 = byName[old.name];
    assert.ok(read2, old.name);
    assert.equal(read2.levels.length, old.archived_levels.length, old.name);
    assert.deepEqual(read2.levels.map(l => l.sp), old.archived_levels.map(l => l.sp_cost), `SP de ${old.name}`);
    assert.deepEqual(read2.levels.map(l => l.cooldown), old.archived_levels.map(l => l.cooldown_seconds), `recarga de ${old.name}`);
    const attack = read2.levels.map(l => l.effects.Attack ?? null);
    const expected = old.archived_levels.map(l => l.effects.ataque_exibido ?? null);
    assert.deepEqual(attack.map(a => (a === null ? null : Number(a.replace(',', '')))), expected, `ataque de ${old.name}`);
  }
});

test('cada habilidade lida traz a data, o endereço e o hash da captura', () => {
  for (const s of Object.values(tosbase)) {
    assert.match(s.captured_at, /^2016\d{10}$/);
    assert.match(s.archive_url, /^https:\/\/web\.archive\.org\/web\/2016/);
    assert.match(s.html_sha256, /^[0-9a-f]{64}$/);
    assert.ok(s.name && s.levels.length === s.max_level, `${s.tosbase_id} ${s.name}`);
  }
});
