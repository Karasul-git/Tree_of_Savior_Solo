using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Melia.Zone.World.Quests;
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
    return bad == 0 && okFail && okWin && blocked ? 0 : 1;
  }
}
