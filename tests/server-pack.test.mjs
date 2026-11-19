import { test } from 'node:test';
import assert from 'node:assert/strict';
import { readFileSync, existsSync } from 'node:fs';

const file = f => readFileSync(new URL('../server/original-2016/' + f, import.meta.url), 'utf8');

test('o pacote do servidor tem os arquivos esperados', () => {
  for (const f of ['user/db/features.txt', 'user/db/jobs.txt', 'user/db/monsters.txt', 'user/db/items.txt', 'user/db/skills.txt',
    'user/scripts/zone/core/calc_character.cs', 'user/scripts/zone/core/calc_skill.cs', 'user/conf/world/jobs.conf', 'README.md', 'tools/DataCheck/Program.cs'])
    assert.ok(existsSync(new URL('../server/original-2016/' + f, import.meta.url)), f);
});

test('os dbs trazem valores de 2016 e fecham o array', () => {
  const monsters = file('user/db/monsters.txt');
  assert.match(monsters, /monsterId: 57078,.*level: 116.*hp: 64094/);
  assert.ok(monsters.trim().endsWith(']'));
  assert.match(file('user/db/items.txt'), /itemId: 101101,.*minAtk: 4, maxAtk: 5/);
  assert.match(file('user/db/jobs.txt'), /jobId: 1001,.*hpRate: 3\.3/);
  assert.match(file('user/db/skills.txt'), /skillId: 10001,.*atkAdd: 56/);
  assert.match(file('user/db/features.txt'), /name: "ReBuild", enabled: false/);
});

test('as fórmulas de 2016 estão nos scripts', () => {
  const calc = file('user/scripts/zone/core/calc_character.cs');
  assert.match(calc, /StatInvestmentBonus2016/);
  assert.match(calc, /Math\.Floor\(\(level - 1\) \* 17\)/);
  assert.match(calc, /Math\.Floor\(stat \* 85\)/);
  assert.match(file('user/scripts/zone/core/calc_skill.cs'), /Original2016SkillTable/);
});
