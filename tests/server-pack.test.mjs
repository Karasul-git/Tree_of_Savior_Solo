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

test('o patch do Melia e o comando de refino estão no pacote', () => {
  const patch = file('patches/0001-propriedades-de-item.patch');
  assert.match(patch, /update_2026-11-19_1\.sql/);
  assert.match(patch, /ADD `properties` VARCHAR\(512\)/);
  assert.match(patch, /public int Potential \{ get; set; \}/);
  assert.match(patch, /this\.Properties\.CopyFrom\(other\.Properties\);/);
  assert.equal((patch.match(/cmd\.Set\("properties", item\.Value\.GetSavedProperties\(\)\);/g) || []).length, 3, 'os três pontos de gravação');
  assert.equal((patch.match(/LoadSavedProperties\(ReadSavedProperties\(reader\)\)/g) || []).length, 2, 'os dois pontos de leitura');
  const refine = file('user/scripts/zone/custom/original2016/refine.cs');
  assert.match(refine, /AddChatCommand\("refine"/);
  assert.match(refine, /public static float EquipBonus\(Character character, RefineStat stat\)/);
  const calc = file('user/scripts/zone/core/calc_character.cs');
  for (const stat of ['Atk', 'MAtk', 'Def', 'MDef', 'Accuracy', 'Evasion'])
    assert.ok(calc.includes(`Original2016Refine.EquipBonus(character, RefineStat.${stat});`), stat);
  assert.equal((calc.match(/Original2016Refine\.EquipBonus/g) || []).length, 8);
});

test('receitas e coleções de 2016 no pacote', () => {
  const report = JSON.parse(file('crafting-report.json'));
  const recipes = file('user/db/recipes.txt');
  const cols = file('user/db/collections.txt');
  assert.equal((recipes.match(/^\{ recipeId: /gm) || []).length, report.receitas.trocada);
  assert.equal((cols.match(/^\{ classId: /gm) || []).length, report.colecoes.trocada);
  assert.ok(recipes.trim().endsWith(']') && cols.trim().endsWith(']'));
  assert.match(recipes, /recipeId: 10006,.*ingredients: \[\{ className: "R_SWD01_109", amount: 1 \}, \{ className: "misc_Dumaro", amount: 16 \}\]/);
  assert.match(cols, /classId: 133,.*rewardProperties: \{"RHP_BM": 5\}/);
  assert.ok(!/rewardProperties: \{[^}]*\d\.\d/.test(cols), 'o Melia lê o bônus como inteiro');
  assert.ok(report.receitas_trocadas_ids.length === report.receitas.trocada);
  // nenhuma coleção exige o próprio item de coleção
  assert.ok(!/requiredItems: \{[^}]*COLLECT_/.test(cols));
});

test('conjuntos de equipamento de 2016 no pacote', () => {
  const report = JSON.parse(file('sets-report.json'));
  const research = JSON.parse(readFileSync(new URL('../data/research/tosbase-2016-sets.json', import.meta.url), 'utf8')).sets;
  const cs = file('user/scripts/zone/custom/original2016/sets.cs');
  assert.equal(report.conjuntos, Object.keys(research).length);
  assert.equal((cs.match(/new ItemSet2016\(/g) || []).length, report.conjuntos);
  assert.equal(report.efeitos_implementados + report.efeitos_pendentes, report.efeitos_no_tosbase);
  const lines = Object.values(research).flatMap(s => Object.values(s.effects).flatMap(t => t.split('\n')));
  assert.equal(lines.length, report.efeitos_no_tosbase, 'toda linha de efeito do ToSBase foi classificada');
  assert.equal((cs.match(/\(\d+, \d+, "/g) || []).length, report.efeitos_pendentes);
  assert.deepEqual(report.itens_que_o_melia_nao_tem, {});
  assert.equal(report.por_conjunto.some(s => s.id === 6), false);
  assert.match(cs, /public const bool Cumulative = true;/);
  assert.match(file('user/scripts/zone/scripts_custom.txt'), /^custom\/original2016\/sets\.cs$/m);
  // cada conjunto traz as peças do ToSBase
  for (const s of report.por_conjunto) assert.deepEqual(s.pecas, research[String(s.id)].items.map(i => i.item_id));
  // todos os efeitos simples chegam ao cálculo de status
  const calc = file('user/scripts/zone/core/calc_character.cs');
  const used = new Set([...calc.matchAll(/Original2016Sets\.EquipBonus\(character, SetStat\.(\w+)\)/g)].map(m => m[1]));
  const wanted = new Set(report.por_conjunto.flatMap(s => s.efeitos_implementados.map(e => e.atributo)));
  for (const stat of wanted) assert.ok(used.has(stat), `atributo ${stat} sem ligação no cálculo de status`);
  assert.ok(!/Movement|Backstab/.test(JSON.stringify(report.por_conjunto.map(s => s.efeitos_implementados))));
});

test('o roteiro de teste traz os números das fórmulas e ids que existem nos dados', () => {
  const doc = file('ROTEIRO-DE-TESTE.md');
  const bonus = v => Math.floor(v < 51 ? v / 5 : v < 151 ? 10 + (v - 50) / 4 : v < 301 ? 35 + (v - 150) / 3 : v < 501 ? 85 + (v - 300) / 2 : 185 + (v - 500));
  const con = 8 + bonus(8), spr = 3 + bonus(3);
  assert.equal(Math.floor(3.3 * 0 + con * 85), 765);
  assert.equal(Math.floor(0.8 * 0 + spr * 13), 39);
  assert.match(doc, /\*\*765\*\* \/ \*\*39\*\*/);
  assert.match(doc, /16\.688/);
  const research = JSON.parse(readFileSync(new URL('../data/research/tosbase-2016-items.json', import.meta.url), 'utf8')).items;
  for (const id of ['101101', '502188', '512188', '522188', '532188', '582119', '602116', '531101', '628054', '645024', '645117', '645118', '645120', '645121', '645267'])
    assert.ok(research[id] || doc.includes(id), id);
  // o comando usa o prefixo do chat do Melia
  assert.ok(!/(^|\s)\/refine/.test(doc));
  assert.match(doc, />refine info/);
  // as quests citadas existem no relatório
  const ids = JSON.parse(file('quests-report.json')).convertidas_ids;
  for (const q of [1003, 1014, 1018, 1033]) assert.ok(ids.includes(q), `quest ${q}`);
  const refine = JSON.parse(readFileSync(new URL('../data/research/tosbase-2016-items.json', import.meta.url), 'utf8')).items['101101'].enhancement;
  assert.equal(refine[0].price, 115);
  assert.deepEqual(refine.slice(0, 3).map(e => e.bonus), [3, 6, 9]);
});
