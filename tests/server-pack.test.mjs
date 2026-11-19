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

test('as quests do pacote: script, lista e relatório coerentes', () => {
  const cs = file('user/scripts/zone/custom/original2016/quests.cs');
  const report = JSON.parse(file('quests-report.json'));
  const classes = [...cs.matchAll(/public class Q(\d+)Original2016QuestScript : QuestScript/g)].map(m => Number(m[1]));
  assert.equal(classes.length, report.convertidas);
  assert.deepEqual(classes, report.convertidas_ids);
  assert.ok(report.convertidas >= 20, 'esperava ao menos 20 quests que dá para cumprir hoje');
  assert.equal(report.npcs_iniciais_distintos, report.npcs_iniciais_com_modelo_no_melia);
  assert.equal(report.convertidas + Object.values(report.ficaram_de_fora).reduce((a, b) => a + b, 0), report.quests_no_total);
  assert.match(file('user/scripts/zone/scripts_custom.txt'), /^custom\/original2016\/quests\.cs$/m);
  assert.match(cs, /class Original2016QuestHelper : GeneralScript/);
  assert.match(cs, /\[On\("SecondTick"\)\]/);
});

test('cada quest convertida tem objetivo, nível e ids que existem nos dados de 2016', () => {
  const cs = file('user/scripts/zone/custom/original2016/quests.cs');
  const research = f => JSON.parse(readFileSync(new URL('../data/research/' + f, import.meta.url), 'utf8'));
  const monsters = new Set(Object.keys(research('tosbase-2016-monsters.json').monsters).map(Number));
  const items = new Set(Object.keys(research('tosbase-2016-items.json').items).filter(k => /^\d+$/.test(k)).map(Number));
  // itens de quest nem sempre têm página própria no ToSBase: vale também o que as quests citam
  for (const q of Object.values(research('tosbase-2016-quests.json').quests))
    for (const i of [...q.task_items, ...q.reward_all, ...q.reward_choice]) items.add(i.item_id);
  for (const q of Object.values(research('tosbase-2016-quests.json').quests))
    for (const m of q.task_monsters) monsters.add(m.monster_id);
  const blocks = cs.split('public class Q').slice(1);
  assert.ok(blocks.length > 0);
  const used = new Set();
  for (const b of blocks) {
    assert.match(b, /AddObjective\(/);
    assert.match(b, /SetReceive\(QuestReceiveType\.Auto\)/);
    assert.match(b, /LevelPrerequisite\(\d+\)/);
    const id = b.match(/SetId\("(Original2016\.\d+)", (\d+)\)/);
    assert.ok(id && Number(id[2]) >= 1 && Number(id[2]) <= 65535, 'id de quest fora de 1 a 65535');
    assert.ok(!used.has(id[1] + id[2]), 'id repetido');
    used.add(id[1] + id[2]);
    for (const k of b.matchAll(/new KillObjective\((\d+), ([\d, ]+)\)/g))
      for (const mid of k[2].split(',')) assert.ok(monsters.has(Number(mid)), `monstro ${mid} fora dos dados de 2016`);
    for (const c of b.matchAll(/new CollectItemObjective\((\d+), (\d+)\)/g)) assert.ok(items.has(Number(c[1])), `item ${c[1]} fora dos dados de 2016`);
    for (const r of b.matchAll(/new ItemReward\((\d+), (\d+)\)/g)) assert.ok(items.has(Number(r[1])), `recompensa ${r[1]} fora dos dados de 2016`);
    for (const p of b.matchAll(/CompletedPrerequisite\("(Original2016\.\d+)", (\d+)\)/g))
      assert.ok(cs.includes(`SetId("${p[1]}", ${p[2]});`), 'pré-requisito aponta para quest que não existe');
  }
});

test('o relatório de quests registra por que cada quest ficou de fora', () => {
  const report = JSON.parse(file('quests-report.json'));
  for (const [motivo, ids] of Object.entries(report.fora_ids)) {
    assert.equal(ids.length, report.ficaram_de_fora[motivo], motivo);
    assert.ok(!ids.some(i => report.convertidas_ids.includes(i)), 'quest convertida e de fora ao mesmo tempo');
  }
});
