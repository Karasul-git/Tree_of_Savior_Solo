using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Melia.Shared.Game.Const;
using Melia.Zone;
using Melia.Zone.World.Items;
using Melia.Zone.World.Quests;
// Requer o patch patches/0001-propriedades-de-item.patch aplicado ao Melia.
// Uso: dotnet run -c Release --project tools/ScriptCheck -- CAMINHO/quests-report.json CAMINHO/tosbase-2016-items.json
public static class P {
  public static int Main(string[] a) {
    // 1) ids de quests sem colisão (a[0] = quests-report.json)
    using var report = JsonDocument.Parse(File.ReadAllText(a[0]));
    var ids = report.RootElement.GetProperty("convertidas_ids").EnumerateArray().Select(e => e.GetInt64()).ToList();
    var seen = new System.Collections.Generic.Dictionary<long,long>();
    foreach (var q in ids) {
      var id = new QuestId($"Original2016.{q/10000}", q%10000+1);
      if (seen.TryGetValue(id.Value, out var other)) { Console.WriteLine($"colisão {q} x {other}"); return 1; }
      seen[id.Value] = q;
    }
    Console.WriteLine($"{ids.Count} ids de quest sem colisão");
    // 2) refino: compara o C# com a tabela do ToSBase de cada item
    using var doc = JsonDocument.Parse(File.ReadAllText(a[1]));
    int items = 0, levels = 0, bad = 0;
    foreach (var it in doc.RootElement.GetProperty("items").EnumerateObject()) {
      if (!int.TryParse(it.Name, out var id) || !Original2016Refine.Items.ContainsKey(id)) continue;
      items++;
      foreach (var e in it.Value.GetProperty("enhancement").EnumerateArray()) {
        var lv = e.GetProperty("level").GetInt32();
        levels++;
        if (Original2016Refine.Bonus(id, lv) != (int)e.GetProperty("bonus").GetDouble() ||
            Original2016Refine.SuccessRate(id, lv) != (int)e.GetProperty("success_pct").GetDouble() ||
            Original2016Refine.Price(id, lv) != (int)e.GetProperty("price").GetDouble()) { bad++; if (bad < 5) Console.WriteLine($"diferença: item {id} nível {lv}"); }
      }
    }
    Console.WriteLine($"refino: {items} itens, {levels} níveis conferidos, {bad} diferenças");
    // 3) regra de falha e de potencial
    int level = 5, pot = 3;
    var okFail = !Original2016Refine.TryRefine(101101, ref level, ref pot, 99) ;   // nível 6 tem 88%: 99 falha
    Console.WriteLine($"falha: ok={okFail} nível={level} potencial={pot} (esperado 4 e 2)");
    level = 5; pot = 3;
    var okWin = Original2016Refine.TryRefine(101101, ref level, ref pot, 0);
    Console.WriteLine($"sucesso: ok={okWin} nível={level} potencial={pot} (esperado 6 e 3); Old Gladius +6 = {Original2016Refine.Bonus(101101, 6)} (esperado 21)");
    level = 3; pot = 0;
    var blocked = !Original2016Refine.CanRefine(101101, level, pot);
    Console.WriteLine($"potencial 0 bloqueia: {blocked}");
    // 4) patch do Melia: campos do item, propriedades salvas e cópia de item (carrega os dbs como o servidor)
    var data = ZoneServer.Instance.Data;
    data.PropertiesDb.Clear();
    data.PropertiesDb.LoadFile("system/db/properties.txt");
    Melia.Shared.Game.Properties.PropertyTable.Load(data.PropertiesDb);
    data.ItemDb.Clear();
    data.ItemDb.LoadFile("system/db/items.txt");
    data.ItemDb.LoadFile("user/db/items.txt");
    var gladius = new Item(101101);
    var fieldsOk = gladius.Data.Grade == 1 && gladius.Data.Potential == 3 && gladius.Data.Durability == 36
        && gladius.Properties.GetFloat(PropertyName.PR) == 3 && gladius.Properties.GetFloat(PropertyName.MaxDur) == 3600
        && gladius.Properties.GetFloat(PropertyName.Dur) == 3600;
    Console.WriteLine($"item novo: grau {gladius.Data.Grade}, PR {gladius.Properties.GetFloat(PropertyName.PR)}, Dur {gladius.Properties.GetFloat(PropertyName.Dur)}/{gladius.Properties.GetFloat(PropertyName.MaxDur)}: {fieldsOk}");
    gladius.Properties.SetFloat(PropertyName.Reinforce_2, 7);
    gladius.Properties.SetFloat(PropertyName.PR, 2);
    var saved = gladius.GetSavedProperties();
    var reloaded = new Item(101101);
    reloaded.LoadSavedProperties(saved);
    var copy = new Item(gladius);
    var roundTrip = reloaded.Properties.GetFloat(PropertyName.Reinforce_2) == 7 && reloaded.Properties.GetFloat(PropertyName.PR) == 2
        && copy.Properties.GetFloat(PropertyName.Reinforce_2) == 7 && copy.Properties.GetFloat(PropertyName.PR) == 2
        && gladius.Properties.GetFloat(PropertyName.Reinforce_2) == 7;
    Console.WriteLine($"propriedades salvas \"{saved}\"; ida e volta e cópia: {roundTrip}");
    var junk = new Item(101101);
    junk.LoadSavedProperties("lixo;PR=abc;MINATK=999;Reinforce_2=3");
    var junkOk = junk.Properties.GetFloat(PropertyName.Reinforce_2) == 3 && junk.Properties.GetFloat(PropertyName.PR) == 3 && junk.Properties.GetFloat(PropertyName.MINATK) == 4;
    Console.WriteLine($"entradas inválidas ignoradas: {junkOk}");
    // 5) banco de verdade (opcional): grava e lê itens pelo ZoneDb. Requer MySQL/MariaDB com o esquema do Melia e
    //    a atualização sql/updates/update_2026-11-19_1.sql; ative com a variável MELIA_TEST_DB=host:porta:usuario:senha:banco
    var dbOk = true;
    var testDb = Environment.GetEnvironmentVariable("MELIA_TEST_DB");
    if (testDb != null)
    {
        var c = testDb.Split(':');
        var zdb = new Melia.Zone.Database.ZoneDb();
        zdb.Init(c[0], int.Parse(c[1]), c[2], c[3], c[4]);
        var store = new TestStorage();
        var refined = new Item(101101);
        refined.Properties.SetFloat(PropertyName.Reinforce_2, 9);
        refined.Properties.SetFloat(PropertyName.PR, 1);
        store.AddAtPosition(refined, 0, out _);
        store.AddAtPosition(new Item(640003, 5), 1, out _);
        zdb.SaveStorage(store, "storage_personal", "characterId", 1);
        var loaded = new TestStorage();
        typeof(Melia.Zone.Database.ZoneDb).GetMethod("LoadStorage", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public)
            .Invoke(zdb, new object[] { loaded, "storage_personal", "characterId", 1L });
        var stored = loaded.GetItems();
        var sword = stored.Values.FirstOrDefault(i => i.Id == 101101);
        dbOk = stored.Count == 2 && sword != null && sword.Properties.GetFloat(PropertyName.Reinforce_2) == 9 && sword.Properties.GetFloat(PropertyName.PR) == 1
            && stored.Values.First(i => i.Id == 640003).Amount == 5;
        Console.WriteLine($"banco: {stored.Count} itens lidos; espada +{sword?.Properties.GetFloat(PropertyName.Reinforce_2)} potencial {sword?.Properties.GetFloat(PropertyName.PR)}: {dbOk}");
    }
    else
        Console.WriteLine("banco: pulado (defina MELIA_TEST_DB para testar o salvamento)");
    return bad == 0 && okFail && okWin && blocked && fieldsOk && roundTrip && junkOk && dbOk ? 0 : 1;
  }
}

// Armazém mínimo só para o teste do banco.
public class TestStorage : Melia.Zone.World.Storage.Storage
{
    public TestStorage() { this.SetStorageSize(60); }
    public override StorageResult Open() => StorageResult.Success;
    public override StorageResult Close() => StorageResult.Success;
    public override StorageResult StoreItem(long objectId, int amount) => StorageResult.Success;
    public override StorageResult RetrieveItem(long objectId, int amount) => StorageResult.Success;
}
