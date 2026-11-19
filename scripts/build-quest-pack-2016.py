#!/usr/bin/env python3
"""Converte as quests de 2016 (ToSBase) em scripts de quest do servidor Melia.

Uso: python3 scripts/build-quest-pack-2016.py PASTA_DO_PACOTE PASTA_DO_MELIA
  (normalmente server/original-2016 e a cópia do Melia usada em build-server-pack-2016.py)
Lê data/research/tosbase-2016-quests.json e confere os ids de monstros e itens contra o banco do Melia
(system/db) mais as trocas do pacote (user/db). Escreve:
  user/scripts/zone/custom/original2016/quests.cs   (as quests e o auxiliar que as conclui)
  user/scripts/zone/scripts_custom.txt              (lista que carrega o arquivo acima)
  quests-report.json                                (o que foi convertido e o que ficou de fora)

Só converte quests cujos objetivos o servidor consegue acompanhar sem NPC: matar monstros
e juntar itens. Quests de conversa, de ir a um lugar ou de itens que o banco do Melia
não tem ficam no relatório com o motivo. Não inventa nada: tudo vem da captura de 2016.
"""
import collections, json, re, sys
from pathlib import Path

ROOT = Path(__file__).resolve().parent.parent
pack = Path(sys.argv[1])
quests = json.loads((ROOT / 'data/research/tosbase-2016-quests.json').read_text(encoding='utf-8'))['quests']

melia = Path(sys.argv[2])


def read_db(*paths):
    return '\n'.join(p.read_text(encoding='utf-8-sig') for p in paths if p.exists())


mon_txt = read_db(melia / 'system/db/monsters.txt', pack / 'user/db/monsters.txt')
item_txt = read_db(melia / 'system/db/items.txt', pack / 'user/db/items.txt')
monster_ids = set(map(int, re.findall(r'monsterId:\s*(\d+)', mon_txt)))
item_ids = set(map(int, re.findall(r'itemId:\s*(\d+)', item_txt)))
tosbase_monsters = json.loads((ROOT / 'data/research/tosbase-2016-monsters.json').read_text(encoding='utf-8'))['monsters']
monster_by_name = collections.defaultdict(list)
class_by_id = {int(i): c for i, c in re.findall(r'monsterId:\s*(\d+),\s*className:\s*"([^"]*)"', mon_txt)}
melia_name = {int(i): n for i, n in re.findall(r'monsterId:\s*(\d+),\s*className:\s*"[^"]*",\s*name:\s*"([^"]*)"', mon_txt)}
for mid, m in tosbase_monsters.items():  # os monstros que o ToSBase de 2016 lista
    if int(mid) in monster_ids and not class_by_id[int(mid)].startswith('Sec_'):
        name = m['name'] if m['name'].isascii() else melia_name[int(mid)]  # parte dos nomes do ToSBase ficou em coreano
        monster_by_name[name.lower()].append(int(mid))

COUNTER = re.compile(r'\((\d+) / (\d+)\)')
KILL_VERBS = ('defeat', 'hunt', 'kill', 'slay', 'eliminate', 'exterminate', 'destroy')


def variants(name):
    out = {name, name + 's', name + 'es'}
    if name.endswith('y'):
        out.add(name[:-1] + 'ies')
    if name.endswith('f'):
        out.add(name[:-1] + 'ves')
    if name.endswith('man'):
        out.add(name[:-3] + 'men')
    return out


def monster_by_text(line):
    """Monstro cujo nome aparece na linha (o nome mais longo vence); None se nenhum."""
    low, best = line.lower(), None
    for name in monster_by_name:
        if best is not None and len(name) <= len(best):
            continue
        for v in variants(name):
            if re.search(r'(?<![a-z])' + re.escape(v) + r'(?![a-z])', low):
                best = name
                break
    return best


def objective_for(q, line):
    """Devolve ('kill', [ids], n, texto) / ('item', id, n, texto) ou (None, motivo)."""
    m = COUNTER.search(line)
    if not m:
        return None, 'linha sem contador (falar com alguém ou ir a um lugar)'
    n = int(m.group(2))
    items = [t for t in q['task_items'] if t['name'] in line]
    if items:
        t = items[0]
        if t['item_id'] not in item_ids:
            return None, 'item da quest não existe no banco do Melia'
        return ('item', t['item_id'], n, f"Collect {n} {t['name']}"), None
    mons = [t for t in q['task_monsters'] if t['name'] in line]
    if mons:
        t = mons[0]
        if t['monster_id'] not in monster_ids:
            return None, 'monstro não existe no banco do Melia'
        return ('kill', [t['monster_id']], n, f"Kill {n} {t['name']}"), None
    if line.lower().startswith(KILL_VERBS):
        name = monster_by_text(line)
        if name:
            return ('kill', sorted(monster_by_name[name]), n, f'Kill {n} {name.title()}'), None
    return None, 'objetivo sem monstro ou item identificável'


def cs(text):
    return '"' + text.replace('\\', '\\\\').replace('"', '\\"').replace('\r', '').replace('\n', '{nl}') + '"'


def description(q):
    d = (q.get('dialogs', {}).get('start') or {}).get('text') or q['name']
    d = re.sub(r'\s+', ' ', d).strip()
    return d[:480].rstrip() + ('…' if len(d) > 480 else '')


def ns(qid):
    return f'Original2016.{int(qid) // 10000}', int(qid) % 10000 + 1


converted, skipped = {}, collections.Counter()
skipped_list = collections.defaultdict(list)
for qid, q in sorted(quests.items(), key=lambda kv: int(kv[0])):
    lines = [l for l in q['task'] if not l.startswith(('Write Comment', 'Comments (', 'Reward'))]
    if not lines:
        skipped['sem objetivo de contagem (só conversa)'] += 1
        skipped_list['sem objetivo de contagem (só conversa)'].append(int(qid))
        continue
    objs, reason = [], None
    for line in lines:
        o, reason = objective_for(q, line)
        if o is None:
            break
        objs.append(o)
    if reason:
        skipped[reason] += 1
        skipped_list[reason].append(int(qid))
        continue
    converted[qid] = objs

# pré-requisitos: só dá para exigir quests que também foram convertidas
chain_dropped = 0
out_cs = ['''//--- Melia Script ----------------------------------------------------------
// Quests originais de 2016 (gerado por scripts/build-quest-pack-2016.py)
//--- Description -----------------------------------------------------------
// Quests de matar monstros e juntar itens, a partir das capturas do ToSBase.
// Não editar à mão: gere de novo pelo script. As quests chegam sozinhas ao
// alcançar o nível exigido e se concluem sozinhas quando o objetivo termina,
// porque o servidor ainda não tem os NPCs (e suas coordenadas) de 2016.
//---------------------------------------------------------------------------

using System.Collections.Generic;
using Melia.Shared.Scripting;
using Melia.Zone;
using Melia.Zone.Events.Arguments;
using Melia.Zone.Scripting;
using Melia.Zone.World.Quests;
using Melia.Zone.World.Quests.Objectives;
using Melia.Zone.World.Quests.Prerequisites;
using Melia.Zone.World.Quests.Rewards;

public static class Original2016Quests
{
	public static readonly HashSet<long> Ids = new();
}

public class Original2016QuestHelper : GeneralScript
{
	[On("SecondTick")]
	public void OnSecondTick(object sender, TimeEventArgs args)
	{
		foreach (var character in ZoneServer.Instance.World.GetCharacters())
		{
			foreach (var quest in character.Quests.GetInProgress())
			{
				if (Original2016Quests.Ids.Contains(quest.Data.Id.Value) && quest.ObjectivesCompleted)
					character.Quests.Complete(quest);
			}
		}
	}
}
''']
for qid, objs in converted.items():
    q = quests[qid]
    space, num = ns(qid)
    out_cs.append(f'public class Q{qid}Original2016QuestScript : QuestScript\n{{\n\tprotected override void Load()\n\t{{')
    out_cs.append(f'\t\tSetId({cs(space)}, {num});')
    out_cs.append(f'\t\tSetName({cs(q["name"])});')
    out_cs.append(f'\t\tSetDescription({cs(description(q))});')
    out_cs.append('\t\tOriginal2016Quests.Ids.Add(this.QuestId.Value);')
    for i, o in enumerate(objs, 1):
        if o[0] == 'kill':
            ids = ', '.join(map(str, o[1]))
            out_cs.append(f'\t\tAddObjective("o{i}", {cs(o[3])}, new KillObjective({o[2]}, {ids}));')
        else:
            out_cs.append(f'\t\tAddObjective("o{i}", {cs(o[3])}, new CollectItemObjective({o[1]}, {o[2]}));')
    out_cs.append('\t\tSetReceive(QuestReceiveType.Auto);')
    level = q.get('required_level') or q.get('level') or 1
    out_cs.append(f'\t\tAddPrerequisite(new LevelPrerequisite({level}));')
    for r in q['required_quests']:
        if str(r['quest_id']) in converted:
            rs, rn = ns(r['quest_id'])
            out_cs.append(f'\t\tAddPrerequisite(new CompletedPrerequisite({cs(rs)}, {rn}));')
        else:
            chain_dropped += 1
    for r in q['reward_all']:
        if r['item_id'] in item_ids:
            out_cs.append(f'\t\tAddReward(new ItemReward({r["item_id"]}, {r["count"]}));')
    out_cs.append('\t}\n}\n')

target = pack / 'user/scripts/zone/custom/original2016/quests.cs'
target.parent.mkdir(parents=True, exist_ok=True)
target.write_text('\ufeff' + '\n'.join(out_cs), encoding='utf-8')
(pack / 'user/scripts/zone/scripts_custom.txt').write_text(
    '// Melia\n// Lista de scripts do pacote original 2016 (carrega as quests geradas)\n//---------------------------------------------------------------------------\n\ncustom/original2016/quests.cs\n',
    encoding='utf-8')

dropped_rewards = sum(1 for q in quests.values() if q['reward_choice'])
missing_rewards = sum(1 for qid in converted for r in quests[qid]['reward_all'] if r['item_id'] not in item_ids)
report = {'quests_no_total': len(quests), 'convertidas': len(converted), 'ficaram_de_fora': dict(skipped),
          'pre_requisitos_de_quests_nao_convertidas_ignorados': chain_dropped,
          'itens_de_recompensa_ausentes_no_banco_do_melia': missing_rewards,
          'quests_com_recompensa_de_escolha_no_total': dropped_rewards,
          'convertidas_ids': [int(k) for k in converted],
          'fora_ids': {k: v for k, v in skipped_list.items()}}
(pack / 'quests-report.json').write_text(json.dumps(report, ensure_ascii=False, indent=1) + '\n', encoding='utf-8')
print(f'{len(converted)} de {len(quests)} quests convertidas.')
for k, v in skipped.most_common():
    print(f'  {v:5d}  {k}')
