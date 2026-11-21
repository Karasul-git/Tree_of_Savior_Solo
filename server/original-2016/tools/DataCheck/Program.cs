using Melia.Shared.Data;
using Melia.Shared.Data.Database;
using Yggdrasil.Data;

// Carrega os dbs do Melia como o servidor faz: system primeiro, user por cima.
var data = new MeliaData();
var warnings = 0;
void Load(IDatabase db, string name)
{
    var sys = Path.Combine("system/db", name);
    var usr = Path.Combine("user/db", name);
    db.Clear();
    db.LoadFile(sys);
    foreach (var w in db.GetWarnings()) { Console.WriteLine($"AVISO {name}: {w}"); warnings++; }
    if (File.Exists(usr))
    {
        db.LoadFile(usr);
        foreach (var w in db.GetWarnings()) { Console.WriteLine($"AVISO {name} (user): {w}"); warnings++; }
    }
}
Load(data.FeatureDb, "features.txt");
Load(data.ExpDb, "exp.txt");
Load(data.JobDb, "jobs.txt");
Load(data.ItemDb, "items.txt");
Load(data.SkillDb, "skills.txt");
Load(data.MonsterDb, "monsters.txt");
Load(data.PropertiesDb, "properties.txt");
Load(data.RecipeDb, "recipes.txt");
Load(data.CollectionDb, "collections.txt");

var fails = 0;
void Check(string what, bool ok) { Console.WriteLine((ok ? "OK    " : "FALHA ") + what); if (!ok) fails++; }

Check("ReBuild desligado", !data.FeatureDb.IsEnabled("ReBuild"));
Check("NoStatByLevel desligado", !data.FeatureDb.IsEnabled("NoStatByLevel"));
Check("Release ligado", data.FeatureDb.IsEnabled("Release"));
var m = data.MonsterDb.Find(57078);
Check($"Q2 Minotaur: nível {m?.Level} HP {m?.Hp} exp {m?.Exp}/{m?.JobExp} def {m?.PhysicalDefense} atk {m?.PhysicalAttackMin}-{m?.PhysicalAttackMax}",
    m != null && m.Level == 116 && m.Hp == 64094 && m.Exp == 8520 && m.JobExp == 6150 && m.PhysicalDefense == 163 && m.PhysicalAttackMin == 331 && m.PhysicalAttackMax == 416);
Check($"Q2 Minotaur: {m?.Drops.Count} drops", m != null && m.Drops.Any(d => d.ItemId == 645117 && Math.Abs(d.DropChance - 12) < 0.01));
var item = data.ItemDb.Find(101101);
Check($"Old Gladius ataque {item?.MinAtk}-{item?.MaxAtk}", item != null && item.MinAtk == 4 && item.MaxAtk == 5);
var job = data.JobDb.Find(Melia.Shared.Game.Const.JobId.Swordsman);
Check($"Swordsman STR {job?.Str} CON {job?.Con} taxa de HP {job?.HpRate} de SP {job?.SpRate}", job != null && job.Str == 7 && job.Con == 8 && job.HpRate == 3.3f && job.SpRate == 0.8f);
var skill = data.SkillDb.Find("Swordman_Thrust");
Check($"Thrust fator {skill?.Factor} fator por nível {skill?.FactorByLevel} ataque fixo {skill?.AtkAdd}/{skill?.AtkAddByLevel} SP {skill?.BasicSp}",
    skill != null && skill.Factor == 100 && skill.FactorByLevel == 0 && skill.AtkAdd == 56);
var kris = data.RecipeDb.Find(10006);
Check($"receita do Kris: {string.Join(", ", kris?.Ingredients.Select(i => i.ClassName + " x" + i.Amount) ?? new string[0])}",
    kris != null && kris.Ingredients.Count == 2 && kris.Ingredients[1].ClassName == "misc_Dumaro" && kris.Ingredients[1].Amount == 16);
data.CollectionDb.TryFindByClassName("COLLECT_133", out var col133);
Check($"coleção 133: {string.Join(", ", col133?.RewardProperties.Select(p => p.Key + "=" + p.Value) ?? new string[0])}",
    col133 != null && col133.RewardProperties.Count == 1 && col133.RewardProperties["RHP_BM"] == 5);
Check($"exp: nível 2 = {data.ExpDb.GetNextExp(1)}, nível 3 = {data.ExpDb.GetNextExp(2)}", data.ExpDb.GetNextExp(1) == 8 && data.ExpDb.GetNextExp(2) == 272);
// Fórmulas compiladas: testa o bônus por investimento do script de 2016 por reflexão.
var asmPath = Path.GetFullPath("user/tmp/build/scripts/zone_user/bin/net8.0/ScriptsZoneUser.dll");
var asm = System.Reflection.Assembly.LoadFrom(asmPath);
var type = asm.GetType("CharacterCalculationsScript");
var bonus = type.GetMethod("StatInvestmentBonus2016", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
float B(float v) => (float)bonus.Invoke(null, new object[] { v });
Check($"bônus 50 = {B(50)}, 107 = {B(107)}, 150 = {B(150)}, 300 = {B(300)}, 500 = {B(500)}, 600 = {B(600)}", B(50) == 10 && B(107) == 24 && B(150) == 35 && B(300) == 85 && B(500) == 185 && B(600) == 285);
var table = asm.GetType("Original2016SkillTable").GetField("SpByLevel").GetValue(null) as System.Collections.IDictionary;
var thrust = (float[])table[10001];
Check($"SP da Thrust por nível: {string.Join(",", thrust)}", thrust.Length == 15 && thrust[0] == 8 && thrust[14] == 24);
Console.WriteLine($"{warnings} avisos, {fails} falhas.");
return fails == 0 ? 0 : 1;
