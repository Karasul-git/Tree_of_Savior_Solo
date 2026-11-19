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

test('os spawns do pacote: arquivos alterados, tempos de nascimento e relatório', async () => {
  const { readdirSync, statSync } = await import('node:fs');
  const report = JSON.parse(file('spawns-report.json'));
  const walk = dir => readdirSync(dir).flatMap(n => { const p = dir + '/' + n; return statSync(p).isDirectory() ? walk(p) : [p]; });
  const root = new URL('../server/original-2016/user/scripts/zone/content/', import.meta.url).pathname;
  const mobs = walk(root).filter(p => p.endsWith('.cs'));
  assert.equal(mobs.length, report.arquivos_alterados);
  assert.ok(report.alteracoes['respawn aplicado'] > 400);
  let applied = 0, removed = 0;
  for (const p of mobs) {
    const text = readFileSync(p, 'utf8');
    for (const line of text.split('\n')) {
      if (/^\s*AddSpawner\(/.test(line)) assert.ok((line.match(/respawn:/g) || []).length <= 1, 'respawn repetido: ' + line.trim());
    }
    applied += (text.match(/respawn: Seconds\(\d+\)/g) || []).length;
    removed += (text.match(/\/\/ removido \(pacote 2016\)/g) || []).length;
  }
  assert.ok(applied >= report.alteracoes['respawn aplicado']);
  assert.equal(removed, report.alteracoes['spawner de cristal de raiz removido'] + report.alteracoes['sobrescrita de atributos por mapa removida']);
  for (const p of mobs) assert.doesNotMatch(readFileSync(p, 'utf8'), /^\s*AddPropertyOverrides\(/m, 'sobrescrita de atributos ficou ativa em ' + p);
  const west = readFileSync(root + 'r1/mobs/fields/f_siauliai_west.cs', 'utf8');
  assert.match(west, /AddSpawner\("f_siauliai_west\.Id2", MonsterId\.Onion, min: 27, max: 35, respawn: Seconds\(3\)\);/);
  assert.match(west, /\/\/ AddSpawner\("f_siauliai_west\.Id6", MonsterId\.Rootcrystal_01/);
  assert.equal(report.mapas_com_spawn_no_melia + report.mapas_fechados_ou_sem_spawn.length, report.mapas_do_tosbase_com_monstros);
  assert.ok(report.monstros_com_spawner_no_melia <= report.monstros_do_tosbase_nesses_mapas);
});

test('potencial, durabilidade e grau de 2016 estão no banco de itens do pacote', () => {
  const items = file('user/db/items.txt');
  assert.match(items, /itemId: 101101,.*grade: 1, potential: 3, durability: 36 \}/);
  assert.ok((items.match(/potential: \d+/g) || []).length >= 2000);
  assert.ok((items.match(/durability: \d+/g) || []).length >= 1800);
  assert.ok(items.trim().endsWith(']'));
});

test('as regras de refino de 2016 reproduzem as tabelas do ToSBase', () => {
  const research = JSON.parse(readFileSync(new URL('../data/research/tosbase-2016-items.json', import.meta.url), 'utf8')).items;
  const report = JSON.parse(file('refine-report.json'));
  const cs = file('user/scripts/zone/custom/original2016/refine.cs');
  assert.match(cs, /public static class Original2016Refine/);
  assert.match(file('user/scripts/zone/scripts_custom.txt'), /^custom\/original2016\/refine\.cs$/m);
  // reimplementa a regra e compara com cada item do ToSBase que o pacote lista
  const bonus = (stat, grade, lv) => {
    const attack = stat === 'ATK' || stat === 'MATK';
    const base = attack ? grade + 2 : Math.floor(grade / 2) + 1, limit = attack ? 5 : 3;
    return base * (lv <= limit ? lv : limit + (lv - limit) * 2);
  };
  const ids = [...cs.matchAll(/\{ (\d+), new RefineInfo\(RefineStat\.(\w+), (\d+), (-?\d+), (-?\d+),/g)];
  assert.equal(ids.length, report.itens_com_tabela_de_refino);
  let standard = 0;
  const statName = { Atk: 'ATK', MAtk: 'MATK', Def: 'DEF', MDef: 'MDEF', Accuracy: 'HR', Evasion: 'DR', MagicAmp: 'MHR' };
  for (const [, id, stat, grade, bKind, rKind] of ids) {
    const src = research[id].enhancement;
    assert.equal(src.length, 15, id);
    if (Number(bKind) < 0) {
      standard++;
      src.forEach(e => assert.equal(e.bonus, bonus(statName[stat], Number(grade), e.level), `bônus de ${id} no nível ${e.level}`));
    }
    if (Number(rKind) < 0) {
      const weapon = stat === 'Atk' || stat === 'MAtk';
      const rates = weapon ? [100, 100, 100, 100, 100, 88, 78, 68, 59, 51, 51, 51, 51, 51, 51] : [100, 100, 100, 88, 78, 68, 59, 51, 51, 51, 51, 51, 51, 51, 51];
      assert.deepEqual(src.map(e => e.success_pct), rates, `chance de ${id}`);
    }
  }
  assert.ok(standard > 1250, 'a maioria dos itens segue a fórmula padrão');
  assert.equal(report.itens_com_tabela_de_refino - standard, report.itens_com_bonus_fora_do_padrao);
  assert.ok(report.itens_com_bonus_fora_do_padrao < 250);
});
