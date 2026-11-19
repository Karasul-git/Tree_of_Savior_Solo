//--- Melia Script ----------------------------------------------------------
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

public class Q1003Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1004);
		SetName("Talk to the Scout (1)");
		SetDescription("You were asked to tell the troops to assemble before going to Klaipeda. Tell the Scout about it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 4 Kepa", new KillObjective(4, 400001));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1014Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1015);
		SetName("Talk to the Search Scout");
		SetDescription("You were asked to inform the troops to assemble before heading to Klaipeda. Inform the Search Scout about the assembly.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Large Kepa", new KillObjective(1, 400002));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1018Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1019);
		SetName("The Road to Klaipeda (1)");
		SetDescription("Listen to Laimonas' request.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Infrorocktor", new KillObjective(7, 41319));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1019Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1020);
		SetName("The Road to Klaipeda (2)");
		SetDescription("Let's go to Klaipeda.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rocktortuga", new KillObjective(1, 41233, 57121));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 1019));
		AddReward(new ItemReward(640080, 3));
	}
}

public class Q1022Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1023);
		SetName("The Beginning of Trouble");
		SetDescription("Talk to the Battle Commander. Something dubious seems to have happened.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Golem", new KillObjective(1, 450221));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 3));
		AddReward(new ItemReward(640103, 3));
	}
}

public class Q1023Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1024);
		SetName("Something Fishy");
		SetDescription("The Search Scout seems to be in trouble. Try talking to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Large Kepa", new KillObjective(1, 57407));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1031Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1032);
		SetName("Camp Crisis");
		SetDescription("The Eastern Woods Base Camp seems to be in danger because of monster accumulation. Let's go and have a look.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Poata", new KillObjective(1, 41202, 41357, 57115, 57164, 57993));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1032Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1033);
		SetName("Threats of the Eastern Woods");
		SetDescription("Knight Commander Uska has told you to speak with Knight Aras who is in charge of the Crystal Mine. Talk to Knight Aras.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 4 Pokubu", new KillObjective(4, 401341));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1033Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1034);
		SetName("Sentinel's Favor (1)");
		SetDescription("Let's help the Sentinel exterminate Chupacabras who threaten the troops.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Chupacabra", new KillObjective(7, 400961));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1036Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1037);
		SetName("Not as Intended (3)");
		SetDescription("Listen to the information about Chupacabras from the Supply Officer.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Large Gray Chupacabra", new KillObjective(7, 400964));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddPrerequisite(new CompletedPrerequisite("Original2016.2", 132));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1037Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1038);
		SetName("Not as Intended (4)");
		SetDescription("Seems like the Supply Officer is in trouble. Talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 5 Weaver", new KillObjective(5, 41280));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 1037));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1041Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1042);
		SetName("Supply Soldier's Favor (1)");
		SetDescription("Supply Soldier's Favor (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Weaver Claw", new CollectItemObjective(650408, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
	}
}

public class Q1042Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1043);
		SetName("Supply Soldier's Favor (2)");
		SetDescription("Something is not resolved yet. Try talking to the Supply Soldier.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 12 Pokubu", new KillObjective(12, 401341));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 1042));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q1043Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1044);
		SetName("Aras' Commission (3)");
		SetDescription("The Operations Officer said that the Search Scout in the lower area found the Vubbe Fighter. Meet the Search Scout near the bridge in the lower area of Bulvjes Farm.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Vubbe Fighter", new KillObjective(1, 400201));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 2));
	}
}

public class Q1045Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1046);
		SetName("Rescue the Villagers");
		SetDescription("You arrived at Crystal Mine 3F, where the villagers may be imprisoned. Talk to Vaidotas about finding the residents.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Crystal Spider", new KillObjective(7, 41409));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(11));
		AddReward(new ItemReward(640081, 3));
	}
}

public class Q1047Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1048);
		SetName("Netherbovine's Attack");
		SetDescription("Netherbovine suddenly appeared and it looks very dangerous. Defeat it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Netherbovine", new KillObjective(1, 41237));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(11));
		AddReward(new ItemReward(640081, 3));
		AddReward(new ItemReward(502116, 1));
		AddReward(new ItemReward(644008, 1));
	}
}

public class Q1048Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1049);
		SetName("Demons of the Closed Area");
		SetDescription("Something is sealed in the Crystal Pillar. Examine the pillar.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mirtis", new KillObjective(1, 400401));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(11));
		AddReward(new ItemReward(640081, 5));
		AddReward(new ItemReward(601111, 1));
	}
}

public class Q1054Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1055);
		SetName("Treasure Map of the Stonemason's Family (1)");
		SetDescription("Treasure Map of the Stonemason's Family (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Key", new CollectItemObjective(662008, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(66));
	}
}

public class Q1059Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1060);
		SetName("Adventurer's Favor (2)");
		SetDescription("Talk to Adventurer Varkis");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Lithorex", new KillObjective(1, 57504));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(63));
		AddReward(new ItemReward(640084, 2));
	}
}

public class Q1061Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1062);
		SetName("The Eternal Adventure (2)");
		SetDescription("The Eternal Adventure (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Varkis' Research Materials (3)", new CollectItemObjective(662069, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(63));
	}
}

public class Q1064Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1065);
		SetName("In their Honor (1)");
		SetDescription("In their Honor (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Boulder to be carved into a Memorial Stone", new CollectItemObjective(650236, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(86));
	}
}

public class Q1072Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1073);
		SetName("The Keys Within the Spatial Gap");
		SetDescription("The Keys Within the Spatial Gap");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 Great King's Key", new CollectItemObjective(650244, 3));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
	}
}

public class Q1073Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1074);
		SetName("Rough Advancement [Barbarian Advancement]");
		SetDescription("Find the Barbarian Master in East Siauliai Woods.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Barbarian Master", new KillObjective(1, 57237));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q1075Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1076);
		SetName("Bothersome Mission [Psychokino Advancement]");
		SetDescription("Bothersome Mission [Psychokino Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Desmodus' Tail Pin", new CollectItemObjective(650249, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q1077Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1078);
		SetName("Non-existent Book [Linker Advancement] (1)");
		SetDescription("Non-existent Book [Linker Advancement] (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Corylus Core", new CollectItemObjective(650251, 5));
		AddObjective("o2", "Collect 1 Galok Blood", new CollectItemObjective(650252, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q1109Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1110);
		SetName("Legend of the Cold Iron [Cryomancer Advancement] (2)");
		SetDescription("Legend of the Cold Iron [Cryomancer Advancement] (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Kadumel Era Armor", new CollectItemObjective(690008, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q1112Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1113);
		SetName("Exorcism [Priest Advancement]");
		SetDescription("Find the Priest Master in Klaipeda");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Evil Spirit", new KillObjective(1, 400103));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q1122Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1123);
		SetName("Fedimian Completionist [Archer Advancement] (2)");
		SetDescription("Archer Master emphasized on the importance of helping others. Go to the Rodelero Master in Fedimian and ask if you can help with anything.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Minotaur", new KillObjective(1, 57153));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q1142Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1143);
		SetName("Lessons of Survival [Barbarian Advancement]");
		SetDescription("Find the Barbarian Master in East Siauliai Woods.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Barbarian Master", new KillObjective(1, 57237));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
	}
}

public class Q1145Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1146);
		SetName("Freedom and Courage [Highlander Advancement]");
		SetDescription("Talk to the Highlander Master in Klaipeda");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Sparnasman", new KillObjective(1, 57155));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
	}
}

public class Q1146Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1147);
		SetName("Prove You are the Owner of the Shield [Peltasta Advancement]");
		SetDescription("Talk to the Peltasta Master in Klaipeda.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Chapparition", new KillObjective(1, 57154));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
	}
}

public class Q1149Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1150);
		SetName("Test of the Blades [Swordsman Advancement]");
		SetDescription("Find the Swordsman Master in Klaipeda");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Minotaur", new KillObjective(1, 57153));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q1158Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1159);
		SetName("Recognition of Rimgaile [Scout Advancement]");
		SetDescription("Talk to the Scout Master in Gateway to Great King.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Scout Master", new KillObjective(1, 57234));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
	}
}

public class Q1159Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1160);
		SetName("Road of the Goddess [Paladin Advancement]");
		SetDescription("Talk to the Paladin Master at Nefritas Cliffs.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Sparnasman", new KillObjective(1, 57155));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
	}
}

public class Q1160Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 1161);
		SetName("Looking Through [Cataphract Advancement]");
		SetDescription("Go to the Gateway of the Great King and talk to the Cataphract Submaster.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Sparnasman", new KillObjective(1, 57155));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
		AddReward(new ItemReward(661033, 1));
	}
}

public class Q4220Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4221);
		SetName("Crystal Wall of the Closed Area");
		SetDescription("Crystal Wall of the Closed Area");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Vubbe Magic Stone", new CollectItemObjective(645002, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(11));
	}
}

public class Q4314Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4315);
		SetName("Eyes of Zachariel (2)");
		SetDescription("You were proven to be the right person by the Eye of the Great King. Talk to Archivist Jonas.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 10 Pino", new KillObjective(10, 401181));
		AddObjective("o2", "Kill 4 Geppetto", new KillObjective(4, 401101));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
		AddReward(new ItemReward(640082, 1));
	}
}

public class Q4318Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4319);
		SetName("Historian Kepeck (3)");
		SetDescription("Looks like Historian Kepeck wants some help. Talk to Historian Kepeck again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Ginklas", new KillObjective(1, 57062));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
		AddReward(new ItemReward(640082, 1));
		AddReward(new ItemReward(628023, 1));
	}
}

public class Q4374Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4375);
		SetName("Soldier's Precious Belongings (1)");
		SetDescription("Soldier's Precious Belongings (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Sickening Lump", new CollectItemObjective(650297, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(110));
	}
}

public class Q4384Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4385);
		SetName("Secure Route to the Gateway of the Great King (2)");
		SetDescription("Since you passed the order to Soldier Poulter, go back to Commander Julian and talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 8 Wood Goblin", new KillObjective(8, 41290));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(111));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q4386Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4387);
		SetName("Owl Sculpture's Little Help");
		SetDescription("There is an Owl Sculpture that seems to have something to say. Talk to the Substitute Owl Sculpture.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 13 Treegool", new KillObjective(13, 41264));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(111));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q4388Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4389);
		SetName("Off To A Journey");
		SetDescription("There is a hesitant-looking Owl Sculpture. Talk to the Settled Owl Sculpture.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Ravinelarva", new KillObjective(7, 41269));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(111));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q4390Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4391);
		SetName("Search Scouts under Attack");
		SetDescription("Soldier Dainus is trembling from fear. Talk to Soldier Dainus to look for the other soldiers.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Ironbaum", new KillObjective(1, 41354));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(112));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q4394Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4395);
		SetName("An Endless Mission (1)");
		SetDescription("Soldier Alan is staring at you. Talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rajapearl", new KillObjective(1, 41243));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q4397Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4398);
		SetName("Grave's Owner");
		SetDescription("Find the grave at the Sunset Flag Forest");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mushwort", new KillObjective(1, 57070));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(114));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q4400Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4401);
		SetName("An Endless Mission (3)");
		SetDescription("An Endless Mission (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Kepa Raider Sap", new CollectItemObjective(650310, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
	}
}

public class Q4401Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4402);
		SetName("Trivial Honor (1)");
		SetDescription("Trivial Honor (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Ducky Fat", new CollectItemObjective(650670, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
	}
}

public class Q4402Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4403);
		SetName("An Endless Mission (4)");
		SetDescription("An Endless Mission (4)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Cronewt Leather", new CollectItemObjective(650311, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
	}
}

public class Q4405Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4406);
		SetName("Old Owl Sculpture's Aid");
		SetDescription("Old Owl Sculpture's Aid");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Infro Holder Horn", new CollectItemObjective(650307, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
	}
}

public class Q4408Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4409);
		SetName("The End of Gaigalas");
		SetDescription("Completed the magic that will weaken Gaigalas. Summon Gaigalas and defeat it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gaigalas", new KillObjective(1, 57056));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q4422Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4423);
		SetName("Missing Researcher (5)");
		SetDescription("Talk to Airine. She is trembling.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Scorpio", new KillObjective(1, 41379));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(57));
		AddReward(new ItemReward(640084, 1));
		AddReward(new ItemReward(645519, 1));
	}
}

public class Q4428Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4429);
		SetName("New Researcher's Favor (1)");
		SetDescription("New Researcher's Favor (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Lost Research Document", new CollectItemObjective(650531, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
	}
}

public class Q4429Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4430);
		SetName("New Researcher's Favor (2)");
		SetDescription("New Researcher's Favor (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Royal Mausoleum Research Report", new CollectItemObjective(650319, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
	}
}

public class Q4430Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4431);
		SetName("New Researcher's Favor (3)");
		SetDescription("New Researcher's Favor (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Torn Research Record", new CollectItemObjective(650320, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
	}
}

public class Q4436Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4437);
		SetName("Gorath's Concern (3)");
		SetDescription("You made Badat to return safely. Talk to Gorath about it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Molich", new KillObjective(1, 57076));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
		AddPrerequisite(new CompletedPrerequisite("Original2016.2", 153));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q4463Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4464);
		SetName("Incomplete Purifier");
		SetDescription("Incomplete Purifier");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Purifier Cooling Parts", new CollectItemObjective(661003, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(7));
	}
}

public class Q4471Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4472);
		SetName("Cyclops' Attack in the Crystal Mine");
		SetDescription("Cyclops is rampaging in the Crystal Mine. Defeat Cyclops.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Cyclops", new KillObjective(1, 41244));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(7));
		AddReward(new ItemReward(640081, 2));
		AddReward(new ItemReward(645025, 1));
		AddReward(new ItemReward(644043, 1));
	}
}

public class Q4479Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4480);
		SetName("Activate the Passage Purifier (2)");
		SetDescription("Activate the Passage Purifier (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Passage Purifier Parts", new CollectItemObjective(661006, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(7));
	}
}

public class Q4484Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4485);
		SetName("Circulation Purifier Issues (2)");
		SetDescription("Check if the Purifier Pipe in District 2 is functioning properly.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Carapace", new KillObjective(1, 41246));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9));
		AddReward(new ItemReward(640081, 2));
		AddReward(new ItemReward(645336, 1));
	}
}

public class Q4501Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 4502);
		SetName("Destroyer of the Main Purifier (2)");
		SetDescription("Use the Mine Compass to find the parts of the Main Purifier that were destroyed and scattered by monsters.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Stone Whale", new KillObjective(1, 41209));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9));
		AddReward(new ItemReward(640081, 2));
		AddReward(new ItemReward(661010, 1));
	}
}

public class Q7018Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 7019);
		SetName("The threat on the ground");
		SetDescription("Something is hidden behind the rock. Go and examine it closely.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Iltiswort", new KillObjective(1, 57102));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
		AddReward(new ItemReward(640084, 1));
		AddReward(new ItemReward(628023, 1));
	}
}

public class Q7073Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 7074);
		SetName("Recruiting Mercenaries (1)");
		SetDescription("Mercenary Toby is looking for someone who can help him. Talk to Toby");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Desert Chupacabra", new KillObjective(7, 47329));
		AddObjective("o2", "Kill 3 Zinute", new KillObjective(3, 47327));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(51));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q8030Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8031);
		SetName("Unfinished Commission (1)");
		SetDescription("Unfinished Commission (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Relic Fragment", new CollectItemObjective(650393, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
	}
}

public class Q8034Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8035);
		SetName("The Stone Pillar Beneath Alkune Stairway");
		SetDescription("The Stone Pillar Beneath Alkune Stairway");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 Old Pillar Sculpture", new CollectItemObjective(650240, 3));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
	}
}

public class Q8036Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8037);
		SetName("The Stone Pillar at Overlong Bridge Valley");
		SetDescription("The Stone Pillar at Overlong Bridge Valley");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Small Stone Piece", new CollectItemObjective(650242, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
	}
}

public class Q8038Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8039);
		SetName("Lindt's Matters");
		SetDescription("Lindt's Matters");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Lindt's Supplies", new CollectItemObjective(650388, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
	}
}

public class Q8045Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8046);
		SetName("Information about Saugas (2)");
		SetDescription("Information about Saugas (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 Monster Poison", new CollectItemObjective(650506, 3));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
	}
}

public class Q8046Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8047);
		SetName("Information about Saugas (3)");
		SetDescription("You are all ready to lure Saugas. Talk to Eta who is confident.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 6 Sauga", new KillObjective(6, 401001));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 8046));
		AddReward(new ItemReward(640082, 1));
	}
}

public class Q8219Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8220);
		SetName("Attack of the Gray Golem");
		SetDescription("Advance to defeat the Gray Golem that suddenly appeared");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gray Golem", new KillObjective(1, 57120, 57998, 450222));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(99));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q8226Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8227);
		SetName("Last Mission (1)");
		SetDescription("Last Mission (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Legs of Solid Wood", new CollectItemObjective(650436, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
	}
}

public class Q8227Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8228);
		SetName("Last Mission (2)");
		SetDescription("Last Mission (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Rustling Skin", new CollectItemObjective(650437, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
	}
}

public class Q8229Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8230);
		SetName("Last Mission (4)");
		SetDescription("Last Mission (4)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Ellom Leaves", new CollectItemObjective(650438, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
	}
}

public class Q8231Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8232);
		SetName("Remote Area of Kateen Forest");
		SetDescription("It seems that the Scouts have differing opinions on whether Deadborn is nearby. Talk to the Scouts again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Deadborn", new KillObjective(1, 41203));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
		AddReward(new ItemReward(640085, 2));
		AddReward(new ItemReward(645026, 1));
	}
}

public class Q8243Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8244);
		SetName("The Missing Troop (2)");
		SetDescription("It seems that you found one of the scouts that were lost. Talk to the Scout.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Bite", new KillObjective(7, 400281));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(104));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q8249Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8250);
		SetName("Mission over Mission");
		SetDescription("Mission over Mission");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Puragi's Hook", new CollectItemObjective(650443, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(104));
	}
}

public class Q8253Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8254);
		SetName("Leadership of Philipas");
		SetDescription("Senior Commander Philipas is waiting for Soldier Roy at the Center Unit Camp. Talk to Philipas.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Big Blue Griba", new KillObjective(1, 400462));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(104));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 8250));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q8255Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8256);
		SetName("Recycling (2)");
		SetDescription("Various writings on how to prepare when the demons attack are engraved on the epitaph. Read the epitaph.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Achat", new KillObjective(1, 41240));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(71));
		AddReward(new ItemReward(640084, 2));
	}
}

public class Q8258Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8259);
		SetName("Troop Left Alone");
		SetDescription("Troop Left Alone");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Bloodstained Report Page", new CollectItemObjective(650445, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(104));
	}
}

public class Q8266Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8267);
		SetName("Disaster of Saknis Plains (2)");
		SetDescription("All the soldiers are participating in the battle. Talk with Senior Officer Philipas");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mushwort", new KillObjective(1, 57069));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(104));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q8268Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8269);
		SetName("Defeat Red Fisherman");
		SetDescription("There is a small herd of Gribas near the Rearguard Unit Camp. Let's go and see them carefully.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 9 Red Fisherman", new KillObjective(9, 400344));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(104));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q8274Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8275);
		SetName("Grudge of A Nameless Warrior");
		SetDescription("There are the artifacts of an unknown soldier who died at Saknis Plains due to the monsters. He might be the owner of the necklace that you picked up last time. Put the necklace on him and pray for him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Werewolf", new KillObjective(1, 41247));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(104));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q8312Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8313);
		SetName("Spiritual Poison (2)");
		SetDescription("I will talk to regret many souls");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 15 Sakmoli", new KillObjective(15, 400561));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8317Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8318);
		SetName("숲에 스며든 마족(1)");
		SetDescription("숲에 스며든 마족(1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Sacred Force Sculpture", new CollectItemObjective(650456, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8322Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8323);
		SetName("영혼 사냥꾼(4)");
		SetDescription("영혼 사냥꾼(4)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Gateway Key", new CollectItemObjective(650457, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8333Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8334);
		SetName("Sculptor's Trial (5)");
		SetDescription("Talk to the Test Instructor Owl");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Deadborn", new KillObjective(1, 41203));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8339Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8340);
		SetName("Attack of Biteregina");
		SetDescription("Please defeat Biteregina");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Biteregina", new KillObjective(1, 57086));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8347Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8348);
		SetName("Healer Lady's Worry (3)");
		SetDescription("Seems like there is still work to be done. Talk to the Healer Lady again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Chafer", new KillObjective(1, 41320, 57107, 57288));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 3));
		AddReward(new ItemReward(645025, 1));
	}
}

public class Q8358Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8359);
		SetName("Dangerous Throneweaver (3)");
		SetDescription("You've got enough Sakmoli Roots that you would use to lure Throneweavers. Talk with Sad Owl Sculpture.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Throneweaver", new KillObjective(1, 41221));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(99));
		AddReward(new ItemReward(640086, 2));
		AddReward(new ItemReward(645026, 1));
		AddReward(new ItemReward(644051, 1));
	}
}

public class Q8374Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8375);
		SetName("Requesting Support");
		SetDescription("The wounded soldier Aspas is looking for someone's help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Wood Goblin", new KillObjective(7, 41290));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(111));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q8379Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8380);
		SetName("Black-colored Honor (1)");
		SetDescription("Various writings on how to prepare when the demons attack are engraved on the epitaph. Read the epitaph.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 8 Vikaras Mage", new KillObjective(8, 57010));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(77));
		AddReward(new ItemReward(640085, 1));
	}
}

public class Q8382Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8383);
		SetName("Black Hands");
		SetDescription("Various writings on how to prepare when the demons attack are engraved on the epitaph. Read the epitaph.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Denoptic", new KillObjective(1, 41368, 58001));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(79));
		AddReward(new ItemReward(640085, 3));
	}
}

public class Q8387Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8388);
		SetName("The Last Guardian");
		SetDescription("Talk to the Guardian's Stone Statue.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Bearkaras", new KillObjective(1, 57081));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(82));
		AddReward(new ItemReward(640085, 3));
		AddReward(new ItemReward(645522, 1));
	}
}

public class Q8388Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8389);
		SetName("The Guardian's Jar (1)");
		SetDescription("The Guardian's Jar (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Royal Mausoleum's Magic Source", new CollectItemObjective(650468, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(83));
	}
}

public class Q8422Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8423);
		SetName("Empty Slate");
		SetDescription("Various writings on how to prepare when the demons attack are engraved on the epitaph. Read the epitaph.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 6 Rusrat", new KillObjective(6, 400801));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(84));
		AddReward(new ItemReward(640085, 1));
	}
}

public class Q8428Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8429);
		SetName("Light the Fire (1)");
		SetDescription("Light the Fire (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Burning Stone", new CollectItemObjective(650470, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(72));
	}
}

public class Q8429Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8430);
		SetName("Light the Fire (2)");
		SetDescription("You have collected enough Burning Stones. Find the stone lantern of the Royal Mausoleum and light it up with the Burning Stone.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Clymen", new KillObjective(1, 57111));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(77));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 8429));
		AddReward(new ItemReward(640084, 2));
		AddReward(new ItemReward(645025, 1));
	}
}

public class Q8430Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8431);
		SetName("Liberation of Magic (1)");
		SetDescription("Liberation of Magic (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Activation Stone", new CollectItemObjective(650471, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(73));
	}
}

public class Q8434Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8435);
		SetName("Hidden Place (2)");
		SetDescription("You've gained the Guardian Energy. Move to the secret location to find the secret treasure of the Royal Mausoleum.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Tomb Lord", new KillObjective(1, 57114));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
		AddReward(new ItemReward(640084, 2));
		AddReward(new ItemReward(645521, 1));
	}
}

public class Q8435Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8436);
		SetName("Emergency (1)");
		SetDescription("Emergency (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Blazing Magic Source", new CollectItemObjective(650472, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(76));
	}
}

public class Q8442Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8443);
		SetName("For the Revelator, by the Revelator");
		SetDescription("For the Revelator, by the Revelator");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Wheelen's Mark", new CollectItemObjective(650473, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(80));
	}
}

public class Q8444Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8445);
		SetName("Call the Guardian");
		SetDescription("Various writings on how to prepare when the demons attack are engraved on the epitaph. Read the epitaph.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 6 Venucelos", new KillObjective(6, 41198));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(82));
		AddReward(new ItemReward(640085, 1));
	}
}

public class Q8446Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8447);
		SetName("Royal Mausoleum's Treasure");
		SetDescription("Many things were prepared for the Revelator at the Royal Mausoleum. Find the reports and read them.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rocktortuga", new KillObjective(1, 57121));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(82));
		AddReward(new ItemReward(640085, 2));
		AddReward(new ItemReward(645025, 1));
	}
}

public class Q8452Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8453);
		SetName("Old Story (1)");
		SetDescription("Old Story (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Brother's Diary", new CollectItemObjective(650474, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
	}
}

public class Q8458Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8459);
		SetName("Old Story (7)");
		SetDescription("You may meet Coben's brother soon. Talk to Coben.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Devilglove", new KillObjective(1, 57373));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
		AddReward(new ItemReward(640085, 2));
		AddReward(new ItemReward(650660, 1));
		AddReward(new ItemReward(645528, 1));
	}
}

public class Q8459Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8460);
		SetName("New Market District (1)");
		SetDescription("You met Caroline Canolyn who seems very anxious. Talk to Caroline Canolyn.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 9 Cockatrice", new KillObjective(9, 401261));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
		AddReward(new ItemReward(640085, 1));
	}
}

public class Q8460Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8461);
		SetName("New Market District (2)");
		SetDescription("Caroline Canolyn told you that the monsters stole all the tools. Talk to Caroline Canolyn again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Moa", new KillObjective(1, 41378));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 8460));
		AddReward(new ItemReward(640085, 2));
		AddReward(new ItemReward(645529, 1));
	}
}

public class Q8461Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8462);
		SetName("Preventive Measures (1)");
		SetDescription("Tara Miles wants your help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 10 Cockatrice", new KillObjective(10, 401261));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
		AddReward(new ItemReward(640085, 1));
	}
}

public class Q8462Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8463);
		SetName("Preventive Measures (2)");
		SetDescription("Preventive Measures (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Dust Sample", new CollectItemObjective(650479, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
	}
}

public class Q8463Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8464);
		SetName("Preventive Measures (3)");
		SetDescription("Preventive Measures (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 2 Big Cockatrice Tail", new CollectItemObjective(650480, 2));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(97));
	}
}

public class Q8473Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8474);
		SetName("Goddess Gabija (3)");
		SetDescription("You finally arrived inside the Mage Tower. Talk to Grita.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 6 Rubblem", new KillObjective(6, 57040));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q8474Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8475);
		SetName("Goddess Gabija (4)");
		SetDescription("You lit up a signal to let the goddess know that help is coming. Discuss what to do next with Grita.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 6 Phyracon", new KillObjective(6, 47397));
		AddObjective("o2", "Kill 8 Drake", new KillObjective(8, 58119, 401621));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 8474));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q8478Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8479);
		SetName("Mage Tower 2nd Floor (1)");
		SetDescription("Mage Tower 2nd Floor (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Jewel of Prominence", new CollectItemObjective(650661, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(106));
	}
}

public class Q8481Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8482);
		SetName("Mage Tower 2nd Floor (4)");
		SetDescription("Mage Tower 2nd Floor (4)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 11 Flame Source", new CollectItemObjective(650486, 11));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(106));
	}
}

public class Q8487Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8488);
		SetName("Immobile Security Device");
		SetDescription("There is a Mineloader at the Mage Tower that acts as a guard. Check the immobile Mineloader.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mineloader", new KillObjective(1, 41225));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(109));
		AddReward(new ItemReward(640086, 3));
		AddReward(new ItemReward(644022, 1));
	}
}

public class Q8491Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8492);
		SetName("Mage Tower 4th Floor (4)");
		SetDescription("The Jewel of Prominence is stabilized but the explosion at the 3rd Floor has made the Mage Tower unstable. Go to the Magic Control Room to control the disrupted magic of the Tower.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 5 Minivern", new KillObjective(5, 57050));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q8492Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8493);
		SetName("Mage Tower 4th Floor (5)");
		SetDescription("Defeated all the monsters that threatened Grita. Ask Grita what to do next.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Grinender", new KillObjective(1, 41374, 58273));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 8492));
		AddReward(new ItemReward(640086, 3));
		AddReward(new ItemReward(645026, 1));
	}
}

public class Q8497Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8498);
		SetName("Helgasercle Invading the Tower");
		SetDescription("You destroyed all four Magic Suppressors that Helgasercle prepared. It is now time to liberate the Mage Tower from the demon's powers. Find Helgasercle at the Great Hall.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Helgasercle", new KillObjective(1, 41228));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(116));
		AddReward(new ItemReward(640086, 3));
		AddReward(new ItemReward(640109, 1));
	}
}

public class Q8500Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8501);
		SetName("Lizard of Fire");
		SetDescription("The Barrier Activation Device is an important piece of equipment that protects the Mage Tower. Carefully inspect it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Salamander", new KillObjective(1, 57088));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q8504Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8505);
		SetName("Flame Fusion Room Archon");
		SetDescription("&The Flame Fusion Machine in Mage Tower 2F has a function to enhance the power of the flame. Check the Flame Fusion Machine.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Archon", new KillObjective(1, 57096));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(106));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q8511Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8512);
		SetName("Church Gate (2)");
		SetDescription("The Essence of Light is ready. Talk to Follower Vaidutis.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mummyghast", new KillObjective(1, 41372));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(34));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q8513Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8514);
		SetName("Demon Sisters");
		SetDescription("Vaidutis is looking for help from somebody in the Tenet Church 1F.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 15 Pawndel", new KillObjective(15, 57028));
		AddObjective("o2", "Kill 15 Pawnd", new KillObjective(15, 57213));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(34));
		AddReward(new ItemReward(640082, 1));
	}
}

public class Q8516Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8517);
		SetName("Lunatic Wizard (4)");
		SetDescription("Lunatic Wizard (4)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Red Infrorocktor Core", new CollectItemObjective(650558, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(109));
	}
}

public class Q8518Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8519);
		SetName("Activate the Central Altar");
		SetDescription("Follower Donatas is seeking for help from somone on the first floor of the Tenet Church.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mallet Wyvern", new KillObjective(1, 47502));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(34));
		AddReward(new ItemReward(640082, 3));
	}
}

public class Q8520Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8521);
		SetName("Nibble Nibble");
		SetDescription("Follower Tomas wants help on the Tenet Church 1F.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 8 Yognome", new KillObjective(8, 57207));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(30));
		AddReward(new ItemReward(640082, 1));
	}
}

public class Q8522Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8523);
		SetName("Church Underground Passage");
		SetDescription("Follower Tomas wants help on the 1st floor of the Tenet Church.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Unknocker", new KillObjective(1, 41371));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(30));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 8610));
		AddReward(new ItemReward(640082, 3));
		AddReward(new ItemReward(645025, 1));
		AddReward(new ItemReward(644055, 1));
	}
}

public class Q8526Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8527);
		SetName("Bully");
		SetDescription("Vaidas is waiting for help at the Tenet Church B1.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 2 Glizardon", new KillObjective(2, 57021));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(30));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q8527Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8528);
		SetName("Beyond the Darkness");
		SetDescription("Follower Tomas asked you to find Vaidutis at the Tenet Church B1.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Cyclops", new KillObjective(1, 57087));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(30));
		AddPrerequisite(new CompletedPrerequisite("Original2016.0", 8523));
		AddReward(new ItemReward(640082, 3));
		AddReward(new ItemReward(582113, 1));
		AddReward(new ItemReward(644043, 1));
	}
}

public class Q8529Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8530);
		SetName("Recapture the Bell Tower");
		SetDescription("Gesti disappeared to the Central Hall. Talk to Follower Algis.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Necroventer", new KillObjective(1, 41230));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(38));
		AddReward(new ItemReward(640082, 3));
		AddReward(new ItemReward(644009, 1));
	}
}

public class Q8534Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8535);
		SetName("Cleaning the Church");
		SetDescription("Follower Algis is waiting for the Revelator in the Tenet Church 2F.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 2 Egnome", new KillObjective(2, 57026));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(38));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q8543Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8544);
		SetName("Bull Hunting");
		SetDescription("Follower Kayetonas at Nefritas Cliff is looking for someone who can fight with him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Minotaur", new KillObjective(1, 41383));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(22));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q8546Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8547);
		SetName("Foreseen Crisis (2)");
		SetDescription("The Paladin Master is waiting for your help at Nefritas Cliff.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Throneweaver", new KillObjective(1, 57094));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(22));
		AddReward(new ItemReward(644051, 1));
	}
}

public class Q8602Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8603);
		SetName("Irritating Pricks");
		SetDescription("Watcher Erra in Tenet Garden is waiting for someone's help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 8 Seedmia", new KillObjective(8, 57016));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(25));
		AddReward(new ItemReward(640082, 1));
	}
}

public class Q8609Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8610);
		SetName("Grown Apart From Hope");
		SetDescription("Follower Algis is waiting for you at Tenet Garden.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Chapparition", new KillObjective(1, 41382));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(25));
		AddReward(new ItemReward(640082, 3));
	}
}

public class Q8703Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8704);
		SetName("Sword for the People and Shield [Highlander Advancement](3)");
		SetDescription("You told him that you will help Uska on behalf of the Highlander and Peltasta Masters. Talk to Uska again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Bearkaras", new KillObjective(1, 57080, 57081, 57082, 401141));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8707Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8708);
		SetName("Disturbance of the Winged Beast [Peltasta Advancement] (3)");
		SetDescription("The Fletcher Master told you to go back, but you can't. Speak to him again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Moa", new KillObjective(1, 57157));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8709Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8710);
		SetName("Pirate on Land [Corsair Advancement]");
		SetDescription("Pirate on Land [Corsair Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Dock Permit", new CollectItemObjective(650631, 1));
		AddObjective("o2", "Collect 3 Navigator's Chart", new CollectItemObjective(650632, 3));
		AddObjective("o3", "Collect 10 Contract", new CollectItemObjective(650633, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q8710Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8711);
		SetName("Wall of Sword and Shield [Rodelero Advancement]");
		SetDescription("Meet with the Rodelero Submaster at Gateway of the Great King.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rodelero Submaster", new KillObjective(1, 103029));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q8718Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8719);
		SetName("Sound of the Wood [Fletcher Advancement]");
		SetDescription("Sound of the Wood [Fletcher Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Whistling Wood", new CollectItemObjective(650637, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q8720Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8721);
		SetName("Poisons to Use [Wugushi Advancement]");
		SetDescription("Poisons to Use [Wugushi Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Synthetic Poison", new CollectItemObjective(650640, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q8725Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8726);
		SetName("Proving Qualification [Barbarian Advancement]");
		SetDescription("Talk to the Barbarian Master in East Siauliai Woods");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Yekub", new KillObjective(1, 103000));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q8727Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8728);
		SetName("Center of the Formation [Centurion Advancement]");
		SetDescription("Go to the Centurion Master in Forest of Prayer");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Colimencia", new KillObjective(1, 57189));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8729Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8730);
		SetName("Curiosity Killed the Epigraphist");
		SetDescription("Epigraphist Schmid of Stele Road is waiting for someone's help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Magburk", new KillObjective(1, 41350));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(86));
		AddReward(new ItemReward(640085, 2));
	}
}

public class Q8731Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8732);
		SetName("Aim [Archer Advancement]");
		SetDescription("Call of the Archer Master");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Sparnashorn", new KillObjective(1, 57182));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8732Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8733);
		SetName("100 Shots 100 Hits [Ranger Advancement]");
		SetDescription("The Ranger Master in Klaipeda is looking for you");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Reaverpede", new KillObjective(1, 57169));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q8736Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8737);
		SetName("Swiftness [Wugushi Advancement]");
		SetDescription("The Wugushi Submaster at Gateway of the Great King is looking for you.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rajatoad", new KillObjective(1, 57184));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8737Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8738);
		SetName("One Will Get Caught If One's Tail Is Too Long [Scout Advancement]");
		SetDescription("The Scout Master is waiting for you in the Gateway of Great King.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Deadborn", new KillObjective(1, 57185));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8738Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8739);
		SetName("Step on Tail [Rogue Advancement]");
		SetDescription("Go to the Rogue Master in Fedimian");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rogue Master", new KillObjective(1, 57238));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8739Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8740);
		SetName("Burning Stones [Fletcher Advancement]");
		SetDescription("Burning Stones [Fletcher Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Well-Burnable Firestone", new CollectItemObjective(650694, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8740Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8741);
		SetName("Recruiting Challenger [Schwartzer Reiter Advancement]");
		SetDescription("The Schwarzer Reiter is waiting for you in Forest of Prayer.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Deadborn", new KillObjective(1, 57185));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
		AddReward(new ItemReward(301104, 1));
		AddReward(new ItemReward(661033, 1));
	}
}

public class Q8748Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8749);
		SetName("Proving Faith [Paladin Advancement]");
		SetDescription("Find the Paladin Master at Nefritas Cliff.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gaigalas", new KillObjective(1, 57188));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8749Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8750);
		SetName("Blitz [Monk Advancement]");
		SetDescription("Blitz [Monk Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Sharp Canine", new CollectItemObjective(650700, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8750Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8751);
		SetName("Price for the Lost Indulgence [Pardoner Advancement]");
		SetDescription("Price for the Lost Indulgence [Pardoner Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Lost Indulgence Offering", new CollectItemObjective(650701, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8751Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8752);
		SetName("Advisor's Choice [Oracle Advancement]");
		SetDescription("The Oracle Master is waiting for you in Klaipeda");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Oracle Master", new KillObjective(1, 57224));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q8755Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8756);
		SetName("A Dark Entity");
		SetDescription("Necroventer was summoned as a result of the necromancy experiment. But it is out of control. Defeat it before it causes too much damage.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Necroventer", new KillObjective(1, 57064));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(89));
		AddReward(new ItemReward(640085, 2));
	}
}

public class Q8804Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8805);
		SetName("Petrifying Frost Forecast (3)");
		SetDescription("Royal Army Guard Retia is waiting for someone's help at Verkti Square");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Sparnashorn", new KillObjective(1, 57290));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(174));
		AddReward(new ItemReward(640114, 2));
	}
}

public class Q8806Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8807);
		SetName("Legal Profit");
		SetDescription("Legal Profit");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Rambear Claws", new CollectItemObjective(650767, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(174));
	}
}

public class Q8810Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8811);
		SetName("White Lie (1)");
		SetDescription("White Lie (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Scent Stone Powder", new CollectItemObjective(650771, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(174));
	}
}

public class Q8812Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8813);
		SetName("White Lie (3)");
		SetDescription("Graverobber Benjaminas at Verkti Square is waiting for someone's help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Saltistter", new KillObjective(1, 57292));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(174));
		AddReward(new ItemReward(640114, 3));
	}
}

public class Q8816Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8817);
		SetName("A Chance of Recovery (1)");
		SetDescription("A Chance of Recovery (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 12 Reaction Stone", new CollectItemObjective(664035, 12));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(177));
	}
}

public class Q8827Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8828);
		SetName("Passing Time");
		SetDescription("Passing Time");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Damaged Artifacts", new CollectItemObjective(664037, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(180));
	}
}

public class Q8832Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8833);
		SetName("Great Escape Portal (3)");
		SetDescription("Chronomancer Sabina is looking for someone's help at Ruklys Street in the Petrified City.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Lepus", new KillObjective(1, 57303));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(180));
		AddReward(new ItemReward(640126, 2));
	}
}

public class Q8837Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8838);
		SetName("Small Difference (1)");
		SetDescription("Small Difference (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Hardened Explosives", new CollectItemObjective(664012, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(183));
	}
}

public class Q8839Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8840);
		SetName("Small Difference (3)");
		SetDescription("Small Difference (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Sticky Fluids", new CollectItemObjective(664010, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(183));
	}
}

public class Q8840Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8841);
		SetName("Small Difference (4)");
		SetDescription("Small Difference (4)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Frosty Core", new CollectItemObjective(664014, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(183));
	}
}

public class Q8846Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8847);
		SetName("Refreshing Work");
		SetDescription("Refreshing Work");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Cough Medicine", new CollectItemObjective(660002, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(186));
	}
}

public class Q8849Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8850);
		SetName("Magical Opinion (1)");
		SetDescription("Magical Opinion (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Dark Crystal", new CollectItemObjective(660004, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(186));
	}
}

public class Q8853Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8854);
		SetName("Interesting Copy");
		SetDescription("Interesting Copy");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Petrified Sample", new CollectItemObjective(660007, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(186));
	}
}

public class Q8854Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8855);
		SetName("Lab Destroyer");
		SetDescription("Alchemist Saliamonas is waiting for someone's help at the Petrified City.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 3 Rubabos", new KillObjective(3, 47464));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(186));
		AddReward(new ItemReward(640126, 2));
	}
}

public class Q8857Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 8858);
		SetName("Secret Trade (3)");
		SetDescription("Graverobber Amanda is waiting for someone's help in the Petrified City.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gargoyle", new KillObjective(1, 57588));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(190));
		AddReward(new ItemReward(640126, 1));
	}
}

public class Q9001Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 9002);
		SetName("Beholder of Sesija Entrance");
		SetDescription("The Royal Mausoleum has had multiple devices to be protected from the demons. Humans can just pass by but guardians can be summoned if necessary. If there are reasons to summon, then activate the summoning device.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Bearkaras", new KillObjective(1, 57080));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(68));
		AddReward(new ItemReward(640084, 3));
		AddReward(new ItemReward(602113, 1));
	}
}

public class Q9002Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 9003);
		SetName("Negotiation (2)");
		SetDescription("After you defeated the guardian of the Royal Mausoleum at Sesija Entrance as Rexipher ordered, Rexipher disappeared through the parvise. To find Cyrenia Odell, you should go through the parvise after Rexipher. He must be not far away.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rexipher", new KillObjective(1, 41229));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(68));
		AddReward(new ItemReward(640084, 3));
	}
}

public class Q9019Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.0", 9020);
		SetName("Fishing on Land (2)");
		SetDescription("Placed the bait. Hide behind the tree and watch which monster shows up.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Ravinepede", new KillObjective(1, 41236));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(59));
		AddReward(new ItemReward(640084, 3));
		AddReward(new ItemReward(512121, 1));
	}
}

public class Q12001Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 2002);
		SetName("Introduction of the Soul [Bokor Advancement]");
		SetDescription("Introduction of the Soul [Bokor Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Bokor Doll", new CollectItemObjective(650065, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q16010Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6011);
		SetName("It's the Honey (2)");
		SetDescription("It's the Honey (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Empty Beehive", new CollectItemObjective(650763, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(150));
	}
}

public class Q16020Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6021);
		SetName("Ruined Brewery");
		SetDescription("Ruined Brewery");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Cork Stopper", new CollectItemObjective(650764, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(150));
	}
}

public class Q16030Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6031);
		SetName("Brewer's Last Hope");
		SetDescription("Brewer's Last Hope");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Dorjen's Signature Mead", new CollectItemObjective(661012, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(150));
	}
}

public class Q16200Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6201);
		SetName("Vilna Forest: The Monsters' Purpose (1)");
		SetDescription("Vilna Forest: The Monsters' Purpose (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Sweet Honeycomb Piece", new CollectItemObjective(650807, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(153));
	}
}

public class Q16300Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6301);
		SetName("Vilna Forest: The Northern Altar Cyclops");
		SetDescription("Check the altar of Vilna Forest at the north side");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Cyclops", new KillObjective(1, 57286));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(153));
		AddReward(new ItemReward(640114, 3));
	}
}

public class Q16310Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6311);
		SetName("Help My Farm Recover");
		SetDescription("Help My Farm Recover");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Bag of Farming Tools", new CollectItemObjective(650816, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(153));
	}
}

public class Q16320Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6321);
		SetName("Desirable Combs");
		SetDescription("Desirable Combs");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Beehive Fragment", new CollectItemObjective(650809, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(153));
	}
}

public class Q16400Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6401);
		SetName("Secret of the Farmland (2)");
		SetDescription("Secret of the Farmland (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Bee Tree Branch", new CollectItemObjective(650817, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(156));
	}
}

public class Q16410Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6411);
		SetName("Power Within the Bee Tree Branch");
		SetDescription("Power Within the Bee Tree Branch");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Incinerated Demon Ash", new CollectItemObjective(650818, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(156));
	}
}

public class Q16500Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6501);
		SetName("The identity of the gigantic transformed plant");
		SetDescription("Priest Raeli is worrying about something. Ask her what to do.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Golem", new KillObjective(1, 450221));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
		AddReward(new ItemReward(640114, 2));
	}
}

public class Q16520Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6521);
		SetName("Scarecrow's Hand (1)");
		SetDescription("Scarecrow's Hand (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Tended Wood", new CollectItemObjective(650820, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(156));
	}
}

public class Q16600Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6601);
		SetName("Addled Revelators");
		SetDescription("Addled Revelators");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Austeja's Fragmented Symbol", new CollectItemObjective(650829, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(159));
	}
}

public class Q16610Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6611);
		SetName("Symbol of Goddess Austeja");
		SetDescription("Symbol of Goddess Austeja");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Austeja's Symbol", new CollectItemObjective(650831, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(159));
	}
}

public class Q16720Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 6721);
		SetName("Merchant's Lost Wares");
		SetDescription("Merchant's Lost Wares");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 Dulke's Package", new CollectItemObjective(650832, 3));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(159));
	}
}

public class Q17002Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7003);
		SetName("Young Magician Owyn (1)");
		SetDescription("There is a young magician standing at the marked area of the map. He seems to need some help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 10 Phyracon", new KillObjective(10, 47397));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q17003Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7004);
		SetName("Young Magician Owyn (2)");
		SetDescription("Owyn looks like he has another request. Talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 10 Drake", new KillObjective(10, 58119, 401621));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
		AddPrerequisite(new CompletedPrerequisite("Original2016.1", 7003));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q17004Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7005);
		SetName("Evidence of Bedazzlement (1)");
		SetDescription("Evidence of Bedazzlement (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Black Coal Powder", new CollectItemObjective(650547, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
	}
}

public class Q17005Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7006);
		SetName("Evidence of Bedazzlement (2)");
		SetDescription("Evidence of Bedazzlement (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Eternal Ember", new CollectItemObjective(650548, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
	}
}

public class Q17006Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7007);
		SetName("Evidence of Bedazzlement (3)");
		SetDescription("Ask Cordelier about the unpleasant vibration that you felt a while ago");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Ginklas", new KillObjective(1, 57063));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
		AddPrerequisite(new CompletedPrerequisite("Original2016.1", 7006));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q17007Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7008);
		SetName("Suspicious Voice");
		SetDescription("There's a strange sound coming from the Large Reading Room. Talk to the Sealed Stone.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 5 Blindlem", new KillObjective(5, 57042));
		AddObjective("o2", "Kill 5 Belegg", new KillObjective(5, 57051));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(106));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q17008Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7009);
		SetName("Sealed Soul (1)");
		SetDescription("The Sealed Stone is in the forum. It seems like it needs help so talk to it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Golem", new KillObjective(1, 57058));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(106));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q17010Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7011);
		SetName("Sealed Soul (3)");
		SetDescription("Sealed Soul (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Piece of the Holy Ark", new CollectItemObjective(650549, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(106));
	}
}

public class Q17013Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7014);
		SetName("What Kind of Sin (2)");
		SetDescription("What Kind of Sin (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Sealed Stone's Voice", new CollectItemObjective(650551, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(109));
	}
}

public class Q17015Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7016);
		SetName("Too Many Seals (2)");
		SetDescription("Too Many Seals (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Crystal of Restriction", new CollectItemObjective(650552, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(109));
	}
}

public class Q17019Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7020);
		SetName("Transmuter Furry Odd (3)");
		SetDescription("Met Furry Odd near the Machinery room. Talk to her.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Yonazolem", new KillObjective(1, 57420));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(113));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q17022Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7023);
		SetName("Hot-blooded Simon Shaw (1)");
		SetDescription("Hot-blooded Simon Shaw (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Flame Charm", new CollectItemObjective(650555, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(116));
	}
}

public class Q17024Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7025);
		SetName("Hot-blooded Simon Shaw (3)");
		SetDescription("Simon looks very tired. Talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Bearkaras", new KillObjective(1, 57082));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(116));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q17100Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7101);
		SetName("The Fearless Ones");
		SetDescription("Guards in Strautas are waiting for someone's help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 5 Zignuts", new KillObjective(5, 47473));
		AddObjective("o2", "Kill 4 Grummer", new KillObjective(4, 47530));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(16));
		AddReward(new ItemReward(640081, 2));
	}
}

public class Q17150Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7151);
		SetName("Tyrant of the Srautas Gorge");
		SetDescription("Guards in Strautas are waiting for someone's help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Poata", new KillObjective(1, 57115));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(16));
		AddReward(new ItemReward(640081, 2));
	}
}

public class Q17160Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7161);
		SetName("Stubborness");
		SetDescription("Watcher Molly is looking for someone brave to help her great plan.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Capria", new KillObjective(1, 57210));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(16));
		AddReward(new ItemReward(640081, 3));
		AddReward(new ItemReward(502117, 1));
	}
}

public class Q17280Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7281);
		SetName("Two Birds with One Stone");
		SetDescription("Two Birds with One Stone");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Enchanted Mane", new CollectItemObjective(650589, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(19));
	}
}

public class Q17320Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7321);
		SetName("Path of the Healer [Cleric Advancement](2)");
		SetDescription("The Cleric Master says you can reach a higher stage in life by service. Go to the Centurion Master at the Starving Demons' Road in Pilgrim's Way area to receive the medicinal herbs needed for service.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Biteregina", new KillObjective(1, 41213, 57268, 57285));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q17350Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7351);
		SetName("Eternal Worship [Krivis Advancement] (2)");
		SetDescription("The Krivis Master says you need to go on pilgrimage to reach a higher level. Get the offerings for the Goddess Statue in Fedimian from the Priest Master.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rocktortuga", new KillObjective(1, 57160));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
		AddReward(new ItemReward(650626, 1));
	}
}

public class Q17410Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7411);
		SetName("Still Needs Memory [Bokor Advancement] (4)");
		SetDescription("The Krivis Master told you the story behind the jewel. Talk to the Krivis Master regarding the memorial service.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Tutu", new KillObjective(1, 57161));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17480Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7481);
		SetName("For the Church [Paladin Advancement]");
		SetDescription("Go to the Paladin Master in Emerald Bluffs");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Paladin Master", new KillObjective(1, 57223));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17510Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7511);
		SetName("Grace of Wealth [Pardoner Advancement] (2)");
		SetDescription("Grace of Wealth [Pardoner Advancement] (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Mothstem's Fire Jar", new CollectItemObjective(650645, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17520Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7521);
		SetName("Grace of Wealth [Pardoner Advancement] (3)");
		SetDescription("Grace of Wealth [Pardoner Advancement] (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 20 Hardened Oil", new CollectItemObjective(650629, 20));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17600Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7601);
		SetName("Activities of a Wizard [Wizard Advancement]");
		SetDescription("Talk to the Wizard Master in Klaipeda.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Golem", new KillObjective(1, 57162));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q17620Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7621);
		SetName("Activities of a Cryomancer [Cryomancer Advancement]");
		SetDescription("Talk to the Cryomancer Master in Klaipeda.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mushcaria", new KillObjective(1, 57165));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q17630Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7631);
		SetName("Activities of a Psychokino [Psychokino Advancement]");
		SetDescription("Meet with the Psychokino Submaster at Miner's Village.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Red Vubbe Fighter", new KillObjective(1, 57167));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17640Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7641);
		SetName("Activities of a Linker [Linker Advancement]");
		SetDescription("Talk with the Linker Submaster at Miners' Village.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Vubbe Fighter", new KillObjective(1, 57166));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17650Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7651);
		SetName("Activities of a Thaurmaturge [Thaumaturge Advancement]");
		SetDescription("Talk to the Thaumaturge Master in Gateway to Great King.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Salamander", new KillObjective(1, 57168));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17660Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7661);
		SetName("An Important Test [Elementalist]");
		SetDescription("Go to Akmens Ridge and talk to the Elementalist Submaster.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Reaverpede", new KillObjective(1, 57169));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17670Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7671);
		SetName("Collecting Evil Spirits [Sorcerer Advancement]");
		SetDescription("Go to the Sorcerer Master in Forest of Prayer");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Devilglove", new KillObjective(1, 57170));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q17680Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7681);
		SetName("Summoning Ritual [Necromancer Advancement]");
		SetDescription("Summoning Ritual [Necromancer Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Blue Pawndel Bones", new CollectItemObjective(650646, 7));
		AddObjective("o2", "Collect 3 Black Pawnd's Entrails", new CollectItemObjective(650647, 3));
		AddObjective("o3", "Collect 1 Harpeia's Beak", new CollectItemObjective(650648, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q17700Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7701);
		SetName("Practice with an Actual Fight [Swordsman Advancement]");
		SetDescription("Go to the Swordsman Master in Klaipeda");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Honeypin", new KillObjective(1, 57187));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q17710Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7711);
		SetName("Proper Price [Peltasta Advancement]");
		SetDescription("Go to the Peltasta Master in Klaipeda");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gaigalas", new KillObjective(1, 57188));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q17720Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7721);
		SetName("Experience is an Asset [Highlander Advancement]");
		SetDescription("Find the Highlander Master in Klaipeda.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Honeypin", new KillObjective(1, 57187));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q17730Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7731);
		SetName("The Reliable [Hoplite Advancement]");
		SetDescription("Find the Hoplite Master in West Siauliai Woods");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Colimencia", new KillObjective(1, 57189));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q17740Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7741);
		SetName("Problem Solving [Barbarian Advancement]");
		SetDescription("Go to the Barbarian Master in East Siauliai Woods");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Poata", new KillObjective(1, 57164));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q17750Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7751);
		SetName("Qualifications of a Commander [Rodelero Advancement]");
		SetDescription("Meet with the Rodelero Submaster at Gateway of the Great King.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Colimencia", new KillObjective(1, 57189));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q17760Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7761);
		SetName("A Threat [Cataphract Advancement]");
		SetDescription("Meet with the Cataphract Submaster at Gateway of the Great King.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gaigalas", new KillObjective(1, 57188));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q17770Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7771);
		SetName("Honor of the Legwyn Family [Squire Advancement]");
		SetDescription("Honor of the Legwyn Family [Squire Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Legwyn Family Decoration", new CollectItemObjective(650695, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q17780Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7781);
		SetName("Ground Combat Training [Corsair Advancement]");
		SetDescription("Find the Corsair Master at Akmens Ridge.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Honeypin", new KillObjective(1, 57187));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q17790Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 7791);
		SetName("Price of Danger [Doppelsoeldner Advancement]");
		SetDescription("Meet the Doppelsoeldner Master at Fedimian.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gaigalas", new KillObjective(1, 57188));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q18002Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 8003);
		SetName("Entrapped Goddess");
		SetDescription("You are blocked by the barrier. Check the barrier at Saule Grand Shrine.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Harpeia", new KillObjective(1, 47321));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(45));
		AddReward(new ItemReward(640082, 3));
		AddReward(new ItemReward(644080, 1));
	}
}

public class Q18003Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 8004);
		SetName("Drugys Courtyard's Binding Magic Circle");
		SetDescription("There is a binding magic circle imbued with dark energy in Drugys Courtyard. Demons have prepared it. Go check it out.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mothstem", new KillObjective(1, 47320));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(45));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q18004Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 8005);
		SetName("Vapsva Vacant Lot's Binding Magic Circle");
		SetDescription("There is a binding magic circle imbued with dark energy in Vapsva Vacant Lot. Demons have prepared it. Go check it out.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Merge", new KillObjective(1, 47510));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(45));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q18150Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 8151);
		SetName("Nugria Sanctum's Moyabruka");
		SetDescription("Check the Portal at Nugria Sanctum.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Moyabruka", new KillObjective(1, 47325));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(36));
		AddReward(new ItemReward(640082, 3));
	}
}

public class Q18170Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 8171);
		SetName("Holy Pond's Merregina");
		SetDescription("The corruption in the Holy Pond seems serious. Check out Holy Pond.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Merregina", new KillObjective(1, 57031));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(36));
		AddReward(new ItemReward(640082, 3));
		AddReward(new ItemReward(602112, 1));
	}
}

public class Q19021Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9022);
		SetName("Oppotunity and Preparation");
		SetDescription("Oppotunity and Preparation");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Bloodstone", new CollectItemObjective(649209, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
	}
}

public class Q19051Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9052);
		SetName("Crafting and Materials");
		SetDescription("Crafting and Materials");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Drake Epaulet", new CollectItemObjective(645305, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
	}
}

public class Q19101Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9102);
		SetName("호기심의 긍정");
		SetDescription("호기심의 긍정");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 100 Great Cathedral Marble Parts", new CollectItemObjective(650679, 100));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q19120Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9121);
		SetName("Activities of a Chronomancer [Chronomancer Advancement]");
		SetDescription("Talk to the Chronomancer Master at Stele Road");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Reaverpede", new KillObjective(1, 57169));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q19130Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9131);
		SetName("Complete the Mission of a Wizard [Wizard Advancement]");
		SetDescription("Meet the Wizard Master in Klaipeda");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Ironbaum", new KillObjective(1, 57173));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q19150Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9151);
		SetName("Complete the Cryomancer's Mission [Cryomancer Advancement]");
		SetDescription("Meet the Cryomancer Master in Klaipeda");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Ironbaum", new KillObjective(1, 57173));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q19180Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9181);
		SetName("Complete the Mission of a Thaumaturge [Thaumaturge Advancement]");
		SetDescription("Go to the Thaumaturge Master in Gateway of Great King");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Netherbovine", new KillObjective(1, 57175));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q19200Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9201);
		SetName("Complete the Mission of a Sorcerer [Sorcerer Advancement]");
		SetDescription("Meet the Sorcerer Master in Forest of Prayer");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Necroventer", new KillObjective(1, 57152));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q19210Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9211);
		SetName("Complete the Mission of a Chronomancer [Chronomancer Advancement]");
		SetDescription("Meet the Chronomancer Master in Stele Road");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Necroventer", new KillObjective(1, 57152));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q19320Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9321);
		SetName("Unfinished Commission (3)");
		SetDescription("Unfinished Commission (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 Old Pillar Sculpture", new CollectItemObjective(650240, 3));
		AddObjective("o2", "Collect 1 Stone Pillar's Surface Powder", new CollectItemObjective(650241, 1));
		AddObjective("o3", "Collect 1 Small Stone Piece", new CollectItemObjective(650242, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
	}
}

public class Q19340Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9341);
		SetName("Epitaph at Dykyne Fork on the right");
		SetDescription("There are many epitaphs with the secret of the Royal Mausoleum in Rukas Plateau. Check the Epitaph at the left side of Dykyne Fork.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Zinutekas", new KillObjective(7, 401301));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
		AddReward(new ItemReward(640084, 1));
		AddReward(new ItemReward(650518, 1));
	}
}

public class Q19400Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9401);
		SetName("Story behind the collapse of the owl");
		SetDescription("There is an Owl Sculpture at Tomb Spider Nest. Take a look at it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Throneweaver", new KillObjective(1, 41221));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q19410Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9411);
		SetName("Goddess Statue of Amolallul Hill");
		SetDescription("There is a Goddess Statue in Amolallul Hill. This statue looks different from the other statues but worship it first anyway.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Corrupted", new KillObjective(1, 401681));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(99));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q19430Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9431);
		SetName("Threat at Poslinkis Forest (2)");
		SetDescription("The Weak Owl Sculpture seems like it is about to cry. Talk to it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mushwort", new KillObjective(1, 57069, 57070));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(102));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q19440Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9441);
		SetName("Insatiable Hunger (1)");
		SetDescription("Insatiable Hunger (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Lump of Kepo Meat", new CollectItemObjective(650772, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(111));
	}
}

public class Q19460Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9461);
		SetName("Insatiable Hunger (3)");
		SetDescription("Did Pilgrim Liliya's hunger go away? Talk to her again");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Carnivore", new KillObjective(1, 57294));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(111));
		AddReward(new ItemReward(640086, 2));
		AddReward(new ItemReward(582115, 1));
	}
}

public class Q19470Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9471);
		SetName("Endless Gluttony (1)");
		SetDescription("Endless Gluttony (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 4 Kepari Flesh", new CollectItemObjective(650776, 4));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(111));
	}
}

public class Q19510Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9511);
		SetName("Necklace of Gluttony");
		SetDescription("Julius and Liliya met Naktis around this location. Could Liliya's necklace be around here?");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Reaverpede", new KillObjective(1, 47317, 57296));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(111));
		AddReward(new ItemReward(640086, 3));
		AddReward(new ItemReward(581132, 1));
	}
}

public class Q19580Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9581);
		SetName("Curse of Sloth - Curse");
		SetDescription("There is a Spoiled Tree Root Crystal at the right side of the Pilgrim's Tomb. To remove the curse from this land, you should destroy this Tree Root Crystal as well.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Cactusvel", new KillObjective(1, 57297));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(114));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q19590Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9591);
		SetName("Curse of Sloth - Faint");
		SetDescription("Curse of Sloth - Faint");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Tree Root Crystal Core", new CollectItemObjective(650779, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(114));
	}
}

public class Q19610Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9611);
		SetName("Curse of Sloth - Misery");
		SetDescription("There is a gigantic Spoiled Tree Root Crystal at the space towards Altar Way. It looks different from the other Tree Root Crystals. Get closer to it and look closely.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Tree Root Crystal", new KillObjective(1, 45135, 45136));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(114));
		AddReward(new ItemReward(640086, 3));
		AddReward(new ItemReward(581133, 1));
	}
}

public class Q19639Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9640);
		SetName("Raging Soul Releasing Test (3)");
		SetDescription("Raging Soul Releasing Test (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Scripture", new CollectItemObjective(662010, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(117));
	}
}

public class Q19680Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9681);
		SetName("Freedom to the Enraged Souls");
		SetDescription("Freedom to the Enraged Souls");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 20 Pilgrim's Lost Scripture Page", new CollectItemObjective(650783, 20));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(117));
	}
}

public class Q19690Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9691);
		SetName("Condensed Anger");
		SetDescription("Condensed Anger");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Tetraox", new KillObjective(1, 57421, 57708));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(117));
	}
}

public class Q19700Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9701);
		SetName("Secret of the Destroyed Altar");
		SetDescription("There are many altars at Altar Way. The altar at the upper side of Rudiziu Trail is broken in half so you don't know if it's working. Get closer to it and look at it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Merge", new KillObjective(1, 57298));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(117));
		AddReward(new ItemReward(640086, 3));
		AddReward(new ItemReward(662010, 1));
	}
}

public class Q19770Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9771);
		SetName("Genuine Goddess Statue (3)");
		SetDescription("Genuine Goddess Statue (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Grey Gypsum Fragment", new CollectItemObjective(650788, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(119));
	}
}

public class Q19860Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9861);
		SetName("With Devotion");
		SetDescription("Receive the Sanctum's Blessing by reading the notice nearby the sanctum.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Ironbaum", new KillObjective(1, 57299));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(119));
		AddReward(new ItemReward(640086, 3));
		AddReward(new ItemReward(581135, 1));
	}
}

public class Q19900Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9901);
		SetName("Avoiding Infatuation (3)");
		SetDescription("Avoiding Infatuation (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Small Portrait", new CollectItemObjective(650799, 1));
		AddObjective("o2", "Collect 1 Mother's Handkerchief", new CollectItemObjective(650800, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(123));
	}
}

public class Q19910Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9911);
		SetName("Avoiding Infatuation (4)");
		SetDescription("It might get its mind back if Witas' belongings is among the things you found. Talk to Pilgrim Witas.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 King Liverwort", new KillObjective(1, 57418));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(123));
		AddPrerequisite(new CompletedPrerequisite("Original2016.1", 9901));
		AddReward(new ItemReward(640086, 3));
	}
}

public class Q19920Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9921);
		SetName("Precious to Someone");
		SetDescription("Precious to Someone");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Picture Fragment", new CollectItemObjective(662014, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(123));
	}
}

public class Q19930Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9931);
		SetName("The Abandoned Chest");
		SetDescription("The Abandoned Chest");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Old Key", new CollectItemObjective(650855, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(123));
	}
}

public class Q19950Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.1", 9951);
		SetName("Purification of the Stream, Recovery of the Tree (3)");
		SetDescription("As the Tree of Faith revived, an unknown, evil energy appeared at the Peddler Camp. Hurry to it.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Succubus", new KillObjective(1, 57445));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(123));
		AddReward(new ItemReward(640086, 3));
		AddReward(new ItemReward(650803, 1));
	}
}

public class Q20073Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 74);
		SetName("Why the Owl Sculpture Fell");
		SetDescription("There are broken Owl Sculptures at the end of Ishrai Crossroads. When you take a close look at them, you may be able to find out why.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mushwort", new KillObjective(1, 57069, 57070));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q20074Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 75);
		SetName("Eternal Sleep of the Owls");
		SetDescription("The Weak Owl is very sad after receiving the fragments of the Owls. Talk to the Weak Owl again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 High Vubbe", new KillObjective(7, 41405));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(102));
		AddPrerequisite(new CompletedPrerequisite("Original2016.1", 9431));
		AddReward(new ItemReward(640086, 1));
	}
}

public class Q20075Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 76);
		SetName("Worrying Owl at Ishrai Crossroads");
		SetDescription("An Owl Sculpture fell on the Ishrai Crossroads. Get near it and try to stand it up.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Green Woodspirit", new KillObjective(1, 400743));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(102));
		AddReward(new ItemReward(640086, 2));
	}
}

public class Q20128Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 129);
		SetName("The Road Back");
		SetDescription("You feel something suspicious when worshipping the Goddess Statue. Talk to the Statue again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mushcaria", new KillObjective(1, 41217));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 3));
		AddReward(new ItemReward(640094, 3));
	}
}

public class Q20131Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 132);
		SetName("Not As Intended (2)");
		SetDescription("You have completed retrieving the supplies. Talk to the Supply Officer again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Tutu", new KillObjective(1, 41210));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 1));
	}
}

public class Q20132Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 133);
		SetName("Saltistter's Raid");
		SetDescription("The scout at the entrance of Kateen Forest wants you to help him. Talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Saltistter", new KillObjective(1, 41207));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(96));
		AddReward(new ItemReward(640085, 2));
	}
}

public class Q20143Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 144);
		SetName("The First Mission");
		SetDescription("The First Mission");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Strangely Engraved Stone", new CollectItemObjective(650379, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(59));
	}
}

public class Q20144Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 145);
		SetName("Ardor for Study (1)");
		SetDescription("Ardor for Study (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Hogma Amulet", new CollectItemObjective(650380, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(59));
	}
}

public class Q20147Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 148);
		SetName("Ardor for Study (2)");
		SetDescription("Archaeologist Friedka is carefully looking at the map from the stone pillars. Talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Bebraspion", new KillObjective(1, 57079, 57302));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(59));
		AddReward(new ItemReward(640084, 3));
	}
}

public class Q20152Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 153);
		SetName("Gorath's Concern (2)");
		SetDescription("Gorath's Concern (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Research Material", new CollectItemObjective(650495, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
	}
}

public class Q20158Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 159);
		SetName("Mop Up the Forger (2)");
		SetDescription("Mop Up the Forger (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Hard Lime", new CollectItemObjective(650500, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(51));
	}
}

public class Q20164Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 165);
		SetName("Words of the King");
		SetDescription("Words of the King");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Broken Slate of the Royal Mausoleum", new CollectItemObjective(650469, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(74));
	}
}

public class Q20182Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 183);
		SetName("Historian Rexipher's Research (3)");
		SetDescription("It is the third ordeal that was prepared by the Great King Zachariel. Read the epitaph on the left side of Dykyne Fork to obtain the third symbol of the ordeal.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Zinutekas", new KillObjective(7, 57777));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(63));
		AddReward(new ItemReward(640084, 2));
	}
}

public class Q20185Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 186);
		SetName("Friend or Foe Error");
		SetDescription("Various writings on how to prepare when the demons attack are engraved on the epitaph. Read the epitaph.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 7 Echad", new KillObjective(7, 41275));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
		AddReward(new ItemReward(640084, 1));
	}
}

public class Q20186Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 187);
		SetName("Deviated Guardian");
		SetDescription("Various writings on how to prepare when the demons attack are engraved on the epitaph. Read the epitaph.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Shnayim", new KillObjective(1, 57093));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(76));
		AddReward(new ItemReward(640084, 2));
		AddReward(new ItemReward(512122, 1));
	}
}

public class Q20187Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 188);
		SetName("Mop Up the Forger (4)");
		SetDescription("Vincent seems to have found out. Talk to Vincent again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Yonazolem", new KillObjective(1, 41211, 57100, 57186, 57406));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(51));
		AddReward(new ItemReward(640082, 3));
		AddReward(new ItemReward(645517, 1));
	}
}

public class Q20188Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 189);
		SetName("Historian Rexipher's Research (5)");
		SetDescription("Historian Rexipher's Research (5)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Hogma Tooth", new CollectItemObjective(650536, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(63));
	}
}

public class Q20197Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 198);
		SetName("Self Sufficiency (1)");
		SetDescription("Self Sufficiency (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Stumpy Tree Branch", new CollectItemObjective(650730, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(86));
	}
}

public class Q20199Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 200);
		SetName("Fragment Finder");
		SetDescription("Fragment Finder");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Second Tombstone Fragment", new CollectItemObjective(650736, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(86));
	}
}

public class Q20201Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 202);
		SetName("Protect the Heritage (2)");
		SetDescription("Protect the Heritage (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Carbonized Tombstone Fragment", new CollectItemObjective(650737, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(86));
	}
}

public class Q20204Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 205);
		SetName("Weird Fellows");
		SetDescription("Treasure Hunter of Stele Road is waiting for someone's help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 15 Tama", new KillObjective(15, 41448));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(86));
		AddReward(new ItemReward(640085, 1));
	}
}

public class Q20205Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 206);
		SetName("Instinct of the Treasure Hunter");
		SetDescription("Instinct of the Treasure Hunter");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Fourth Tombstone Fragment", new CollectItemObjective(650738, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(86));
	}
}

public class Q20212Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 213);
		SetName("Astral Tracing (5)");
		SetDescription("Find the last gravestone of Lydia Schaffen over the Griuve Highway and be enlightened.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Chapparition", new KillObjective(1, 57104));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(89));
		AddReward(new ItemReward(640085, 2));
		AddReward(new ItemReward(644067, 1));
	}
}

public class Q20214Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 215);
		SetName("Flurry's Epitaphs (2)");
		SetDescription("Flurry's Epitaphs (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 2 Destroyed Tombstone Fragment", new CollectItemObjective(650545, 2));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(93));
	}
}

public class Q20215Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 216);
		SetName("Flurry's Epitaphs (3)");
		SetDescription("Flurry's Epitaphs (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Villager's Bag", new CollectItemObjective(650546, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(93));
	}
}

public class Q20219Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 220);
		SetName("Flurry's Epitaphs (6)");
		SetDescription("I found a gravestone dedicated to Agailla Flurry, the wizard who built the Mage Tower. To learn more of Agailla Flurry's secrets, find all of her gravestones.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Reaverpede", new KillObjective(1, 47317));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(93));
		AddReward(new ItemReward(640085, 2));
		AddReward(new ItemReward(645527, 1));
	}
}

public class Q20223Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 224);
		SetName("Lost Material (1)");
		SetDescription("Lost Material (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Experimental Ingredients", new CollectItemObjective(650751, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(89));
	}
}

public class Q20238Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 239);
		SetName("The Final Mission (2)");
		SetDescription("The Final Mission (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Zolem Magic Stone", new CollectItemObjective(650761, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(93));
	}
}

public class Q20239Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 240);
		SetName("The Final Mission (3)");
		SetDescription("The Final Mission (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Unicorn Horn", new CollectItemObjective(650762, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(93));
	}
}

public class Q20243Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 244);
		SetName("Moses' True Feelings (3)");
		SetDescription("Moses' True Feelings (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Strong Flying Frog Leather", new CollectItemObjective(650524, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(93));
	}
}

public class Q20246Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 247);
		SetName("Playing with Fire! (1)");
		SetDescription("Playing with Fire! (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Thornball Remains", new CollectItemObjective(650568, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
	}
}

public class Q20247Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 248);
		SetName("Playing with Fire! (2)");
		SetDescription("Playing with Fire! (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Coliflower Sap", new CollectItemObjective(650576, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
	}
}

public class Q20250Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 251);
		SetName("My Ring!");
		SetDescription("My Ring!");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Adele's Ring", new CollectItemObjective(650571, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
	}
}

public class Q20256Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 257);
		SetName("In the Name of Faith (2)");
		SetDescription("In the Name of Faith (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Operer's Stinger", new CollectItemObjective(650574, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(49));
	}
}

public class Q20260Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 261);
		SetName("Last Warning of the Beholder");
		SetDescription("Goddess Saule Believer Nojus is anxiously waiting for you.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Summoned Poata", new KillObjective(1, 57116));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(48));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q20262Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 263);
		SetName("Caught in the Middle");
		SetDescription("Goddess Saule Believer Raminta needs your help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rikaus", new KillObjective(1, 400661));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(51));
		AddReward(new ItemReward(640082, 3));
	}
}

public class Q20263Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 264);
		SetName("The Art of Interference");
		SetDescription("Goddess Saule Believer Evaldas needs your help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 3 Merog Shaman", new KillObjective(3, 41439));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(51));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q20273Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 274);
		SetName("Purify Kvailas Forest (2)");
		SetDescription("Goddess Saule Believer Kazis needs your help.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Honeypin", new KillObjective(1, 41242));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
		AddReward(new ItemReward(640082, 2));
	}
}

public class Q20274Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 275);
		SetName("Capturing Bramble (3)");
		SetDescription("You agreed to meet Believer Jurga in Giliaii Courtyard to plan a fight against Bramble.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Bramble", new KillObjective(1, 400901));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(54));
		AddReward(new ItemReward(640082, 3));
		AddReward(new ItemReward(645516, 1));
	}
}

public class Q20276Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 277);
		SetName("The Unopened Portal");
		SetDescription("The Unopened Portal");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Black Maize Venom", new CollectItemObjective(650615, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(39));
	}
}

public class Q20277Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 278);
		SetName("Activate the Obelisk (1)");
		SetDescription("Activate the Obelisk (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 White Oak Sap", new CollectItemObjective(650617, 3));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(39));
	}
}

public class Q20282Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 283);
		SetName("Slepingas Stream's Blue Woodspirit");
		SetDescription("Check the Obelisk in Slepingas Stream..");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Blue Woodspirit", new KillObjective(1, 400742));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(39));
		AddReward(new ItemReward(640082, 3));
	}
}

public class Q20283Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 284);
		SetName("Andale Village Priest's Favor (1)");
		SetDescription("Andale Village Priest's Favor (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Languid Herb", new CollectItemObjective(650662, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(42));
	}
}

public class Q20284Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 285);
		SetName("Andale Village Priest's Favor (2)");
		SetDescription("Andale Village Priest's Favor (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Strongly Scented Soul Flower", new CollectItemObjective(650663, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(42));
	}
}

public class Q20285Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 286);
		SetName("Create the Languid Herb Bomb");
		SetDescription("Create the Languid Herb Bomb");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Useful Bomb", new CollectItemObjective(650664, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(42));
	}
}

public class Q20288Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 289);
		SetName("Dvyni Wetland's Colimencia");
		SetDescription("Look for the Strongly Scented Soul Flower in Dvyni Wetland.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Colimencia", new KillObjective(1, 47318));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(42));
		AddReward(new ItemReward(640082, 3));
	}
}

public class Q20292Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 293);
		SetName("Bonfire of the Patrol Road (5)");
		SetDescription("Light up the Bonfires");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Honeypin", new KillObjective(1, 103003));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(96));
		AddReward(new ItemReward(640085, 2));
	}
}

public class Q20308Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 309);
		SetName("Research Reports of the Priests");
		SetDescription("Research Reports of the Priests");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Priest Yosana's Research", new CollectItemObjective(663005, 1));
		AddObjective("o2", "Collect 1 Priest Roana's Research", new CollectItemObjective(663006, 1));
		AddObjective("o3", "Collect 1 Priest Prosit's Research", new CollectItemObjective(663007, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(127));
	}
}

public class Q20318Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 319);
		SetName("Surprise Attack");
		SetDescription("Check the Karuna Altar");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Riteris", new KillObjective(1, 57311));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(132));
		AddReward(new ItemReward(640113, 1));
		AddReward(new ItemReward(644102, 1));
	}
}

public class Q20320Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 321);
		SetName("For the Pilgrim (2)");
		SetDescription("For the Pilgrim (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 15 Meat", new CollectItemObjective(650845, 15));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(133));
	}
}

public class Q20322Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 323);
		SetName("True Origin of the Curse (2)");
		SetDescription("Priest Loana found the true origin of the curse. Talk to Priest Loana.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Chapparition", new KillObjective(1, 57105));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(134));
		AddReward(new ItemReward(663006, 1));
		AddReward(new ItemReward(640113, 3));
	}
}

public class Q20337Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 338);
		SetName("Naktis' Wrath");
		SetDescription("Open the door that will lead you to Pasala Altar.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Naktis", new KillObjective(1, 41351));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(135));
		AddReward(new ItemReward(640113, 2));
		AddReward(new ItemReward(640109, 1));
	}
}

public class Q20339Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 340);
		SetName("Masquerade");
		SetDescription("Priest Goda seems to be wary about something. Talk to Priest Goda.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Linkroller", new KillObjective(1, 41232));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(135));
		AddReward(new ItemReward(640113, 2));
	}
}

public class Q20342Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 343);
		SetName("Adapting to Circumstances (2)");
		SetDescription("Adapting to Circumstances (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Naktis' Servant's Cloth Scrap", new CollectItemObjective(650847, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(135));
	}
}

public class Q20343Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.2", 344);
		SetName("Farewell, My Friend (1)");
		SetDescription("You see Orville, who seems clueless. Talk to Orville.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Glackuman", new KillObjective(1, 47508, 57293, 57497, 57563));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(134));
		AddReward(new ItemReward(640113, 3));
	}
}

public class Q30000Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 1);
		SetName("Picky Taste [Falconer Advancement]");
		SetDescription("Picky Taste [Falconer Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 20 Fresh Meat", new CollectItemObjective(661016, 20));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q30001Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 2);
		SetName("Disciple Laius' Circumstances");
		SetDescription("Disciple Laius' Circumstances");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Orb imbued with Aura", new CollectItemObjective(661017, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(178));
	}
}

public class Q30003Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 4);
		SetName("Wisdom of Guidance");
		SetDescription("Wisdom of Guidance");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Wisdom Guiding Stone", new CollectItemObjective(661019, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(178));
	}
}

public class Q30005Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 6);
		SetName("Preparations for the Dangerous Search (1)");
		SetDescription("Preparations for the Dangerous Search (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Eyes of Truth Fragment", new CollectItemObjective(661020, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(178));
	}
}

public class Q30006Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 7);
		SetName("Preparations for the Dangerous Search (2)");
		SetDescription("Preparations for the Dangerous Search (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Soul Crystal", new CollectItemObjective(661022, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(178));
	}
}

public class Q30008Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 9);
		SetName("The Spatial Magic Gem of Contract (1)");
		SetDescription("The Spatial Magic Gem of Contract (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Spatial Magic Gem Fragment of Contract", new CollectItemObjective(661024, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(181));
	}
}

public class Q30009Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 10);
		SetName("The Spatial Magic Gem of Contract (2)");
		SetDescription("The Spatial Magic Gem of Contract (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Spatial Magic Gem of Contract", new CollectItemObjective(661025, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(181));
	}
}

public class Q30011Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 12);
		SetName("The Spatial Magic Gem of Promise (2)");
		SetDescription("The Spatial Magic Gem of Promise (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Spatial Magic Gem of Promise", new CollectItemObjective(661026, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(181));
	}
}

public class Q30013Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 14);
		SetName("The Spatial Magic Gem of Sacrifice (2)");
		SetDescription("The Spatial Magic Gem of Sacrifice (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Spatial Magic Gem of Offer", new CollectItemObjective(661027, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(181));
	}
}

public class Q30021Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 22);
		SetName("No Freedom (1)");
		SetDescription("No Freedom (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Contract Binded Remains", new CollectItemObjective(661030, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(184));
	}
}

public class Q30032Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 33);
		SetName("Legwyn Family's Seal [Squire Advancement]");
		SetDescription("Legwyn Family's Seal [Squire Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Broken Legwyn Seal Fragment", new CollectItemObjective(661035, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(225));
	}
}

public class Q30034Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 35);
		SetName("Royal Army Corps Machine [Centurion Advancement]");
		SetDescription("Royal Army Corps Machine [Centurion Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Royal Army's Torn Flag", new CollectItemObjective(661037, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(225));
	}
}

public class Q30042Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 43);
		SetName("Lydia Schaffen and the Fletcher's Arrow [Fletcher Advancement]");
		SetDescription("Lydia Schaffen and the Fletcher's Arrow [Fletcher Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Arrow of the First Fletcher Master", new CollectItemObjective(661041, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(225));
	}
}

public class Q30046Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 47);
		SetName("Reserved Records of the Cleric [Pardoner Advancement]");
		SetDescription("Reserved Records of the Cleric [Pardoner Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Pardoner Master's Books", new CollectItemObjective(661044, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(225));
	}
}

public class Q30047Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 48);
		SetName("Disaster is a Great Sample [Druid Advancement]");
		SetDescription("Disaster is a Great Sample [Druid Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 100 Plant Monster Sample", new CollectItemObjective(661045, 100));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(225));
	}
}

public class Q30052Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 53);
		SetName("Recovering the Karolis Altar (1)");
		SetDescription("Recovering the Karolis Altar (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Karolis Altar Crystal", new CollectItemObjective(661048, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(47));
	}
}

public class Q30053Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 54);
		SetName("Recovering the Karolis Altar (2)");
		SetDescription("Recovering the Karolis Altar (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Karolis Altar Fragment", new CollectItemObjective(661049, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(47));
	}
}

public class Q30061Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 62);
		SetName("Sacred Tree of the Forest (1)");
		SetDescription("Sacred Tree of the Forest (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Letas Sacred Branch", new CollectItemObjective(661051, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(51));
	}
}

public class Q30086Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 87);
		SetName("Orsha Retoration Support [Ranger Advancement]");
		SetDescription("Orsha Retoration Support [Ranger Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Wall Restoration Stone", new CollectItemObjective(661058, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q30087Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 88);
		SetName("Sticky [Quarrel Shooter Advancement]");
		SetDescription("Sticky [Quarrel Shooter Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Sticky Plant Sap", new CollectItemObjective(661059, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q30088Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 89);
		SetName("Sapper Research [Sapper Advancement]");
		SetDescription("Sapper Research [Sapper Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Sturdy Research Stone", new CollectItemObjective(661060, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q30103Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 104);
		SetName("Research of Spirits and Emotions [Linker Advancement]");
		SetDescription("Research of Spirits and Emotions [Linker Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 20 Crystal of Twisted Souls", new CollectItemObjective(661066, 20));
		AddObjective("o2", "Collect 20 Shard of Chaotic Emotions", new CollectItemObjective(661067, 20));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q30128Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 129);
		SetName("Trade Route Locked Chest");
		SetDescription("Trade Route Locked Chest");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Sutatis Trade Route Key", new CollectItemObjective(661083, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(78));
	}
}

public class Q30129Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 130);
		SetName("Supporting the Royal Army at Dvasia Peak [Chaplain Advancement]");
		SetDescription("Supporting the Royal Army at Dvasia Peak [Chaplain Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 50 Lost Materials", new CollectItemObjective(661084, 50));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q30130Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 131);
		SetName("The Research at Alemeth Forest");
		SetDescription("The Research at Alemeth Forest");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Research Materials", new CollectItemObjective(661085, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(210));
	}
}

public class Q30132Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 133);
		SetName("Request for the Secret Rescue (2)");
		SetDescription("Request for the Secret Rescue (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Materials for Research", new CollectItemObjective(661087, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(210));
	}
}

public class Q30133Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 134);
		SetName("True Nature of the Research (1)");
		SetDescription("True Nature of the Research (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Green Eldigo Blood", new CollectItemObjective(661089, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(210));
	}
}

public class Q30134Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 135);
		SetName("True Nature of the Research (2)");
		SetDescription("True Nature of the Research (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Lost Research Note, Chapter 1", new CollectItemObjective(661090, 1));
		AddObjective("o2", "Collect 1 Lost Research Note, Chapter 2", new CollectItemObjective(661091, 1));
		AddObjective("o3", "Collect 1 Lost Research Note, Chapter 3", new CollectItemObjective(661092, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(210));
	}
}

public class Q30136Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 137);
		SetName("True Nature of the Research (4)");
		SetDescription("True Nature of the Research (4)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Overgrown Plant Leaves", new CollectItemObjective(661100, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(210));
	}
}

public class Q30144Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 145);
		SetName("The Rune Caster and the Rune Stone [Rune Caster Advancement]");
		SetDescription("The Rune Caster and the Rune Stone [Rune Caster Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 30 Malstatue's Magic Fragment", new CollectItemObjective(661108, 30));
		AddObjective("o2", "Collect 15 Velaphid's Cold Blood", new CollectItemObjective(661109, 15));
		AddObjective("o3", "Collect 5 Pumpleflap's Robust Horn", new CollectItemObjective(661110, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q40070Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 71);
		SetName("Working on the Landfill (1)");
		SetDescription("Working on the Landfill (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 4 Strong Rope", new CollectItemObjective(662017, 4));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(74));
	}
}

public class Q40170Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 171);
		SetName("Walking on the Seeds");
		SetDescription("Walking on the Seeds");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Seed Bag", new CollectItemObjective(662063, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(74));
	}
}

public class Q40270Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 271);
		SetName("Where is the Food");
		SetDescription("Baron Secretary Benius is looking for something desperately. Talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Manticen", new KillObjective(1, 57434));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(76));
		AddReward(new ItemReward(640085, 3));
	}
}

public class Q40320Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 321);
		SetName("The Irregular Stone Statue");
		SetDescription("The Irregular Stone Statue");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Sticky Sap", new CollectItemObjective(662046, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(79));
	}
}

public class Q40380Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 381);
		SetName("Weapons Supply (2)");
		SetDescription("Weapons Supply (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 15 Soft Leather", new CollectItemObjective(662050, 15));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(83));
	}
}

public class Q40490Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 491);
		SetName("The String of Destiny, and The Wandering (1)");
		SetDescription("The String of Destiny, and The Wandering (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 2 Loot Bag", new CollectItemObjective(662075, 2));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(158));
	}
}

public class Q40510Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 511);
		SetName("How to Eat a Kepa (1)");
		SetDescription("How to Eat a Kepa (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 4 Acidic Body Fluid", new CollectItemObjective(662078, 4));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(158));
	}
}

public class Q40550Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 551);
		SetName("Dead of the Dead (3)");
		SetDescription("Dead of the Dead (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Bone Powder", new CollectItemObjective(662084, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(158));
	}
}

public class Q40590Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 591);
		SetName("The Second Epitaph (1)");
		SetDescription("The Second Epitaph (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Tombstone Fragment", new CollectItemObjective(662097, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(162));
	}
}

public class Q40630Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 631);
		SetName("The Third Epitaph (1)");
		SetDescription("The Third Epitaph (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 15 Gooey Oil", new CollectItemObjective(662101, 15));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(162));
	}
}

public class Q40680Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 681);
		SetName("The Only Method");
		SetDescription("The Only Method");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Bone Fragments", new CollectItemObjective(662103, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(166));
	}
}

public class Q40710Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 711);
		SetName("Rust Remover (1)");
		SetDescription("Rust Remover (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Hallowventor Magician Inner Core", new CollectItemObjective(662107, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(166));
	}
}

public class Q40830Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 831);
		SetName("Check the Detector's Functionality (6)");
		SetDescription("Check the Detector's Functionality (6)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Braid", new CollectItemObjective(662125, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(194));
	}
}

public class Q40890Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 891);
		SetName("For Death (1)");
		SetDescription("For Death (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Barrier Stone Core", new CollectItemObjective(662127, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(197));
	}
}

public class Q40920Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 921);
		SetName("And, Eternal Repose (1)");
		SetDescription("And, Eternal Repose (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Svitrigaila's Orb", new CollectItemObjective(662135, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(197));
	}
}

public class Q40960Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 961);
		SetName("무언가가 새겨진 기둥");
		SetDescription("무언가가 새겨진 기둥");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 40 Mysteriously Light Emanating Crystal", new CollectItemObjective(662137, 40));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(93));
	}
}

public class Q41090Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 1091);
		SetName("Meeting the Anastospa");
		SetDescription("Meeting the Anastospa");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 50 Comprehensive Research Data", new CollectItemObjective(645633, 50));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(96));
	}
}

public class Q41100Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 1101);
		SetName("The Story of Anastos");
		SetDescription("The Story of Anastos");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 19 Flammable Powder", new CollectItemObjective(662147, 19));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(96));
	}
}

public class Q41680Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 1681);
		SetName("Educational Materials of the Kedoran Merchant Alliance (1)");
		SetDescription("Educational Materials of the Kedoran Merchant Alliance (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 17 Kedoran Merchant Alliance Educational Material", new CollectItemObjective(662156, 17));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(100));
	}
}

public class Q41700Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 1701);
		SetName("Erecting Tombstones (1)");
		SetDescription("Erecting Tombstones (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Lime Powder", new CollectItemObjective(662160, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
	}
}

public class Q41720Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 1721);
		SetName("Material for Compiling A Scripture (1)");
		SetDescription("Material for Compiling A Scripture (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 17 Kepari Inner Shell", new CollectItemObjective(662163, 17));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
	}
}

public class Q41740Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.4", 1741);
		SetName("Lost Symbol (1)");
		SetDescription("Lost Symbol (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Nestos' Sign", new CollectItemObjective(662167, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(103));
	}
}

public class Q50000Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 1);
		SetName("Disintegration (1)");
		SetDescription("Disintegration (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 12 Colifly Wing", new CollectItemObjective(663010, 12));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(128));
	}
}

public class Q50001Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 2);
		SetName("Disintegration (2)");
		SetDescription("Disintegration (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Loftlem Core", new CollectItemObjective(663011, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(128));
	}
}

public class Q50009Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 10);
		SetName("Monster Gone Wild (1)");
		SetDescription("Monster Gone Wild (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Pink Chupaluka Heart", new CollectItemObjective(663024, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(59));
	}
}

public class Q50019Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 20);
		SetName("Secure the Farmland");
		SetDescription("Dennis is still worried about something. Listen what's the matter.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gorgon", new KillObjective(1, 41376));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(61));
		AddReward(new ItemReward(640084, 3));
	}
}

public class Q50020Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 21);
		SetName("Louise's Farmland (1)");
		SetDescription("Louise needs your help. Talk to Louise near the Louise's Farmland.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 10 Ridimed", new KillObjective(10, 400541));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(60));
		AddReward(new ItemReward(640084, 1));
	}
}

public class Q50024Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 25);
		SetName("Unsatisfactory Crops (1)");
		SetDescription("Unsatisfactory Crops (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Tough Orange Sakmoli Stem", new CollectItemObjective(663023, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(60));
	}
}

public class Q50031Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 32);
		SetName("Designated Area (1)");
		SetDescription("Designated Area (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 35 Fisherman Eye", new CollectItemObjective(663044, 35));
		AddObjective("o2", "Collect 30 Ellom Essence", new CollectItemObjective(663045, 30));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(90));
	}
}

public class Q50060Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 61);
		SetName("Reclaim the Camp (5)");
		SetDescription("You've listened to the request of Delus. Talk to Delus again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Specter Monarch", new KillObjective(1, 103021));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(194));
		AddReward(new ItemReward(640126, 3));
	}
}

public class Q50062Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 63);
		SetName("The seal which the monster possesses");
		SetDescription("The seal which the monster possesses");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Ruklys Army Seal", new CollectItemObjective(663062, 6));
		AddObjective("o2", "Collect 6 Parchment", new CollectItemObjective(663063, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(194));
	}
}

public class Q50067Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 68);
		SetName("To the Storage Quarter (1)");
		SetDescription("To the Storage Quarter (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Earth Crystal", new CollectItemObjective(663068, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(197));
	}
}

public class Q50073Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 74);
		SetName("The Past of the Spirits (2)");
		SetDescription("The Past of the Spirits (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Demon Bones", new CollectItemObjective(663074, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(201));
	}
}

public class Q50081Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 82);
		SetName("Preparation (1)");
		SetDescription("Preparation (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 13 Demon's Blood", new CollectItemObjective(663079, 13));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(204));
	}
}

public class Q50083Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 84);
		SetName("Eminent's Identity");
		SetDescription("The defensive magic circle is active. Go back to Premier Eminent before he gets suspicious.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mandara", new KillObjective(1, 58322));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(204));
		AddReward(new ItemReward(640127, 3));
	}
}

public class Q50086Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 87);
		SetName("Traitor's Diary");
		SetDescription("Traitor's Diary");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Rebel Army Diary, Chapter 1", new CollectItemObjective(663081, 1));
		AddObjective("o2", "Collect 1 Rebel Army Diary, Chapter 2", new CollectItemObjective(663082, 1));
		AddObjective("o3", "Collect 1 Rebel Army Diary, Chapter 3", new CollectItemObjective(663083, 1));
		AddObjective("o4", "Collect 1 Rebel Army Diary, Chapter 4", new CollectItemObjective(663084, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(204));
	}
}

public class Q50106Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 107);
		SetName("Demon Herbalist (2)");
		SetDescription("Demon Herbalist (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 15 Loktanun Fluid", new CollectItemObjective(663088, 15));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(22));
	}
}

public class Q50112Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 113);
		SetName("To Novaha Monastery");
		SetDescription("The device attached to the giant bracken seems to be out of the way now. Meet Rose at the Novaha Monastery Entrance.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gremlin", new KillObjective(1, 57455));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(26));
		AddReward(new ItemReward(640082, 4));
		AddReward(new ItemReward(900011, 400));
	}
}

public class Q50119Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 120);
		SetName("The Rescue (3)");
		SetDescription("The Rescue (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Green Apparition Essence", new CollectItemObjective(663100, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q50121Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 122);
		SetName("The Rescue (5)");
		SetDescription("The door to the Special Reading Room is finally open. Talk to Traveling Merchant Rose.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Mummyghast", new KillObjective(1, 103020));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
		AddReward(new ItemReward(640082, 5));
		AddReward(new ItemReward(644026, 1));
	}
}

public class Q50137Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.5", 138);
		SetName("Rescue Rose (4)");
		SetDescription("Edmundas took a lift to the Medie State Apartments. Follow Edmundas.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Deathweaver", new KillObjective(1, 41380));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(43));
		AddReward(new ItemReward(640082, 5));
	}
}

public class Q60004Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 5);
		SetName("The Teeth of Revenge (2)");
		SetDescription("The Teeth of Revenge (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Blut's Mark", new CollectItemObjective(664016, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(141));
	}
}

public class Q60008Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 9);
		SetName("Ridding the Traitor (2)");
		SetDescription("Ridding the Traitor (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Hauberk's Mark", new CollectItemObjective(664017, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(144));
	}
}

public class Q60010Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 11);
		SetName("Ridding the Traitor (4)");
		SetDescription("Figured out the plans of Nuaele. Talk to Kupole Arune on what to do next.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 2 Harugal", new KillObjective(2, 57714));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(144));
		AddReward(new ItemReward(640113, 2));
	}
}

public class Q60011Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 12);
		SetName("Lodged Stone");
		SetDescription("You're prepared to defeat the Demon Lord Nuaele. Talk to Kupole Arune.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Demon Lord Nuaele", new KillObjective(1, 57412));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(144));
		AddPrerequisite(new CompletedPrerequisite("Original2016.6", 11));
		AddPrerequisite(new CompletedPrerequisite("Original2016.6", 9));
		AddReward(new ItemReward(640113, 3));
		AddReward(new ItemReward(644103, 1));
	}
}

public class Q60016Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 17);
		SetName("The Evening Star at Night");
		SetDescription("Ready to remove the strong power from Dionys. Talk to Kupole Aldona.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Dionys", new KillObjective(1, 103013));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(147));
		AddReward(new ItemReward(640113, 3));
	}
}

public class Q60019Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 20);
		SetName("Hauberk in the Maze (2)");
		SetDescription("Hauberk in the Maze (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Hauberk's Soul Fragment", new CollectItemObjective(664021, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(150));
	}
}

public class Q60024Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 25);
		SetName("The Dimensional Crack (2)");
		SetDescription("The Dimensional Crack (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Trace of Transference", new CollectItemObjective(664027, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(153));
	}
}

public class Q60027Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 28);
		SetName("The Dimensional Crack (5)");
		SetDescription("The Dimensional Crack (5)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Sealing Token", new CollectItemObjective(664024, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(153));
	}
}

public class Q60030Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 31);
		SetName("According To One's Duty");
		SetDescription("According To One's Duty");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Expedition Memento", new CollectItemObjective(664029, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(141));
	}
}

public class Q60032Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 33);
		SetName("Recovering Prestige");
		SetDescription("Recovering Prestige");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Nuaele's Fragment", new CollectItemObjective(664026, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(144));
	}
}

public class Q60034Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 35);
		SetName("Dionys' Claws");
		SetDescription("Dionys' Claws");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Dionys' Claws", new CollectItemObjective(664031, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(147));
	}
}

public class Q60038Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 39);
		SetName("Demons at the Crossroads");
		SetDescription("Demons at the Crossroads");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Demon Teeth", new CollectItemObjective(664032, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(150));
	}
}

public class Q60040Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 41);
		SetName("Food for Dionys");
		SetDescription("Food for Dionys");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Empty Soul", new CollectItemObjective(664028, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(153));
	}
}

public class Q60050Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 51);
		SetName("At Dead End (4)");
		SetDescription("Alina's injury worsened and she collapsed. Talk to Alina again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Werewolf", new KillObjective(1, 41247, 57101, 57174, 57416));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(72));
		AddReward(new ItemReward(640084, 3));
		AddReward(new ItemReward(900011, 900));
	}
}

public class Q60053Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 54);
		SetName("나를 찾는 여정(2)");
		SetDescription("나를 찾는 여정(2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 11 Shadow Essence", new CollectItemObjective(664049, 11));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(78));
	}
}

public class Q60058Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 59);
		SetName("끝없는 거래(1)");
		SetDescription("끝없는 거래(1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Blue Paint", new CollectItemObjective(664052, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
	}
}

public class Q60062Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 63);
		SetName("르귄가의 비극(1)");
		SetDescription("르귄가의 비극(1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Corrupt Fangs", new CollectItemObjective(664053, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(75));
	}
}

public class Q60071Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 72);
		SetName("여정의 시작(2)");
		SetDescription("Press the M key to view the map and go find Settler Brophen.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 5 Kepa", new KillObjective(5, 400001));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 2));
		AddReward(new ItemReward(900011, 10));
	}
}

public class Q60075Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 76);
		SetName("오르샤(2)");
		SetDescription("Officer Lutas is processing the settlers' entrance into Orsha but the task feels endless. Talk to Officer Lutas again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Poata", new KillObjective(1, 41202, 41357, 57115, 57164, 57993));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(1));
		AddReward(new ItemReward(640080, 3));
		AddReward(new ItemReward(900011, 25));
	}
}

public class Q60099Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 100);
		SetName("Large-Scale Search Operation (1)");
		SetDescription("Large-Scale Search Operation (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 4 Priest Gelija's Memo", new CollectItemObjective(664069, 4));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(3));
	}
}

public class Q60108Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 109);
		SetName("사전 조사(2)");
		SetDescription("Agent Notres has asked you to check whether the Deer Hooves Lot can be used to make a temporary settler camp. Go there and see whether it is safe.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gray Golem", new KillObjective(1, 57998));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(3));
		AddReward(new ItemReward(640081, 2));
		AddReward(new ItemReward(900011, 40));
		AddReward(new ItemReward(645025, 1));
	}
}

public class Q60124Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 125);
		SetName("Unidentified Package (1)");
		SetDescription("Unidentified Package (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Unidentified Package", new CollectItemObjective(664077, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(7));
	}
}

public class Q60138Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.6", 139);
		SetName("더 깊은 곳에서(3)");
		SetDescription("Priest Irma can feel that the curse of the idol is being dispelled. Keep talking to Priest Irma.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Denoptic", new KillObjective(1, 58001));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(15));
		AddReward(new ItemReward(640081, 3));
	}
}

public class Q70000Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 1);
		SetName("The Mysterious Tree");
		SetDescription("The Mysterious Tree");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Lizardman Leather", new CollectItemObjective(666000, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(139));
	}
}

public class Q70001Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 2);
		SetName("Unknown Power");
		SetDescription("Unknown Power");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Second Corrupted Soil Sample", new CollectItemObjective(666001, 1));
		AddObjective("o2", "Collect 1 First Corrupted Soil Sample", new CollectItemObjective(666002, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(139));
	}
}

public class Q70003Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 4);
		SetName("Preparing for Large-Scale Purification");
		SetDescription("Preparing for Large-Scale Purification");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 20 Tama Body Fluid", new CollectItemObjective(666003, 20));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(139));
	}
}

public class Q70006Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 7);
		SetName("It's All Over Now");
		SetDescription("Farmer Darcy needs your help. Talk to Farmer Darcy.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 15 Orange Lizardman", new KillObjective(15, 57351));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(139));
		AddReward(new ItemReward(640113, 1));
	}
}

public class Q70008Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 9);
		SetName("Unreliable Folk");
		SetDescription("Farmer Miren seems to have lots of complaints. Talk to Farmer Miren");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 12 Orange Lizardman", new KillObjective(12, 57351));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(139));
		AddReward(new ItemReward(640113, 1));
	}
}

public class Q70009Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 10);
		SetName("Big Inconvenience");
		SetDescription("Big Inconvenience");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Miren's Old Bag", new CollectItemObjective(666006, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(139));
	}
}

public class Q70010Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 11);
		SetName("The Klaipeda Investigation");
		SetDescription("The Klaipeda Investigation");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Rambear Fur", new CollectItemObjective(666007, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(139));
	}
}

public class Q70020Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 21);
		SetName("The Supervisor's Tyranny");
		SetDescription("The Supervisor's Tyranny");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 15 Stumpy Tree Horn Fragment", new CollectItemObjective(666010, 15));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(142));
	}
}

public class Q70021Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 22);
		SetName("You Can't Fool Me");
		SetDescription("You Can't Fool Me");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Stolen Grain Bag", new CollectItemObjective(666011, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(142));
	}
}

public class Q70025Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 26);
		SetName("Old Seeds (2)");
		SetDescription("Old Seeds (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Water-Filled Stone Bowl", new CollectItemObjective(666015, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(142));
	}
}

public class Q70027Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 28);
		SetName("Steam Boiled");
		SetDescription("Farm Soldier Gedson is mad at you. Talk to him to find out.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 12 Cyst", new KillObjective(12, 41265));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(142));
		AddPrerequisite(new CompletedPrerequisite("Original2016.7", 21));
		AddReward(new ItemReward(640113, 1));
	}
}

public class Q70028Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 29);
		SetName("The Disturbing Monster");
		SetDescription("Farm Soldier Gedson still has some tasks to request of you. Talk to Gedson again.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 20 Orange Stumpy Tree", new KillObjective(20, 57357));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(142));
		AddPrerequisite(new CompletedPrerequisite("Original2016.7", 28));
		AddReward(new ItemReward(640113, 1));
	}
}

public class Q70031Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 32);
		SetName("Today's Work");
		SetDescription("Tenant Weiss has been troubling in now. Talk to Tenant Weiss.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 12 Cyst", new KillObjective(12, 41265));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(142));
		AddReward(new ItemReward(640113, 1));
	}
}

public class Q70032Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 33);
		SetName("Hidden Seeds");
		SetDescription("Hidden Seeds");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Weiss' Seed Pouch", new CollectItemObjective(666016, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(142));
	}
}

public class Q70042Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 43);
		SetName("Revenge Shall Be Mine");
		SetDescription("Revenge Shall Be Mine");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Pink Tree Root Mole's Horn", new CollectItemObjective(666018, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(145));
	}
}

public class Q70046Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 47);
		SetName("Anything For Our Master");
		SetDescription("Steward Valen needs your help. Talk to Valen.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 12 Green Carcashu", new KillObjective(12, 57341));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(145));
		AddReward(new ItemReward(640113, 1));
	}
}

public class Q70047Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 48);
		SetName("Managing the Steward's People");
		SetDescription("Managing the Steward's People");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Valen's Medicine Bag", new CollectItemObjective(666019, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(145));
	}
}

public class Q70049Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 50);
		SetName("Shifting Responsibility");
		SetDescription("Buros seems grim. Talk to Buros.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Glass Mole", new KillObjective(1, 41367));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(145));
		AddReward(new ItemReward(640113, 3));
		AddReward(new ItemReward(645027, 1));
	}
}

public class Q70104Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 105);
		SetName("Search for the Trap");
		SetDescription("Search for the Trap");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Working Trap", new CollectItemObjective(666022, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(163));
	}
}

public class Q70106Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 107);
		SetName("The Precious Scripture");
		SetDescription("The Precious Scripture");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Scripture Sheet", new CollectItemObjective(666023, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(163));
	}
}

public class Q70108Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 109);
		SetName("Looking Back Sometimes");
		SetDescription("Looking Back Sometimes");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Tempas Mushroom", new CollectItemObjective(666024, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(163));
	}
}

public class Q70109Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 110);
		SetName("Making Arrowheads");
		SetDescription("Making Arrowheads");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 18 Rocktor's Stony Leg", new CollectItemObjective(666025, 18));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(163));
	}
}

public class Q70122Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 123);
		SetName("Unknown Poison (1)");
		SetDescription("Unknown Poison (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Chafperor Thorn Venom", new CollectItemObjective(666026, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(166));
	}
}

public class Q70125Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 126);
		SetName("The Last Material One More Time");
		SetDescription("The Last Material One More Time");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Woodluwa Leaf", new CollectItemObjective(666030, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(166));
	}
}

public class Q70128Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 129);
		SetName("A Worthy Opponent");
		SetDescription("A Worthy Opponent");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Chevas' Medicinal Herb", new CollectItemObjective(666031, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(166));
	}
}

public class Q70129Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 130);
		SetName("Sweet Ointment (1)");
		SetDescription("Sweet Ointment (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 4 Empty Beehive", new CollectItemObjective(666032, 4));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(166));
	}
}

public class Q70130Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 131);
		SetName("Sweet Ointment (2)");
		SetDescription("Sweet Ointment (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 Woodluwa Essence", new CollectItemObjective(666033, 3));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(166));
	}
}

public class Q70141Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 142);
		SetName("Proof of Strength");
		SetDescription("Proof of Strength");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 4 Liverwort Cap", new CollectItemObjective(666034, 4));
		AddObjective("o2", "Collect 12 Stonecon Fragment", new CollectItemObjective(666035, 12));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(169));
	}
}

public class Q70146Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 147);
		SetName("Creating a Quiet Environment");
		SetDescription("Monk Jones seems anxious. Ask him what his worry is.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 15 Liverwort", new KillObjective(15, 401481));
		AddObjective("o2", "Kill 15 Stonacon", new KillObjective(15, 57682));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(169));
		AddReward(new ItemReward(640114, 2));
	}
}

public class Q70147Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 148);
		SetName("Filtered Water");
		SetDescription("Filtered Water");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 12 Acylado Hydrophyte", new CollectItemObjective(666038, 12));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(169));
	}
}

public class Q70149Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 150);
		SetName("Until the last one");
		SetDescription("It seems that Jeffery still has some matters to resolve. Talk to him.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Giant Wood Goblin", new KillObjective(1, 41386));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(9989));
	}
}

public class Q70163Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 164);
		SetName("Mission to Retake the Monastery (4)");
		SetDescription("You've helped Goss release the barrier after reaching Viltis Fountain. Now talk to him and go in front of the Prayer Room.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Tetraox", new KillObjective(1, 57421, 57708));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(173));
		AddReward(new ItemReward(640114, 3));
	}
}

public class Q70172Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 173);
		SetName("Where it Belongs");
		SetDescription("Where it Belongs");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Tyla Monastery Scriptures", new CollectItemObjective(666041, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(173));
	}
}

public class Q70206Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 207);
		SetName("How to Overcome Severe Coldness");
		SetDescription("How to Overcome Severe Coldness");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Lepusbunny Assassin Fur", new CollectItemObjective(666043, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(202));
	}
}

public class Q70207Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 208);
		SetName("Necessities");
		SetDescription("Necessities");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Flint", new CollectItemObjective(666044, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(202));
	}
}

public class Q70221Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 222);
		SetName("To Eat Something");
		SetDescription("To Eat Something");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 12 Siaulav Mage's Emergency Food", new CollectItemObjective(666046, 12));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(205));
	}
}

public class Q70222Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 223);
		SetName("Demand for Food Supplies");
		SetDescription("Demand for Food Supplies");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 9 Plateau Potatoes", new CollectItemObjective(666047, 9));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(205));
	}
}

public class Q70223Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 224);
		SetName("Caltrop instead of a Caltrop");
		SetDescription("Caltrop instead of a Caltrop");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Usable Bar Fragment", new CollectItemObjective(666048, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(205));
	}
}

public class Q70224Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 225);
		SetName("Methods to Reducing Fever");
		SetDescription("Methods to Reducing Fever");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 5 Blue Lapasape Spotted Mushroom", new CollectItemObjective(666049, 5));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(205));
	}
}

public class Q70225Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 226);
		SetName("Eatables");
		SetDescription("Eatables");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Plateau Tree's Inner Barks", new CollectItemObjective(666051, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(205));
	}
}

public class Q70226Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 227);
		SetName("Priority");
		SetDescription("Mortimer feels that the supplies that are stored at the cargo at the upper side should be retrieved. Look for a way to retrieve them with Mortimer.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Sparnashorn", new KillObjective(1, 57290, 57419));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(205));
		AddReward(new ItemReward(640127, 3));
	}
}

public class Q70227Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 228);
		SetName("Honoring Allies");
		SetDescription("Honoring Allies");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Blue Siaulav Mask", new CollectItemObjective(645650, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(205));
	}
}

public class Q70300Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 301);
		SetName("The Swordsman's Qualities [Swordsman Advancement]");
		SetDescription("Talk with the Swordsman Submaster in Orsha");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 10 Red Kepa", new KillObjective(10, 400003));
		AddObjective("o2", "Kill 10 Woodin", new KillObjective(10, 41294));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(5));
	}
}

public class Q70302Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 303);
		SetName("양손검의 가능성[하이랜더 전직](2)");
		SetDescription("하이랜더로 인정받기에는 아직 충분치 않은 모양입니다. 다시 하이랜더 서브 마스터와 대화하세요.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 10 Woodin", new KillObjective(10, 41294));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(5));
	}
}

public class Q70303Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 304);
		SetName("The Birth Process of a Comrade [Peltasta Advancement]");
		SetDescription("The Birth Process of a Comrade [Peltasta Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 Dry Wood", new CollectItemObjective(666052, 3));
		AddObjective("o2", "Collect 10 Pokuborn Tusk", new CollectItemObjective(666053, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(5));
	}
}

public class Q70307Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 308);
		SetName("고난극복방법[소드맨 전직]");
		SetDescription("오르샤에서 소드맨 서브 마스터와 대화하세요.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 30 Gosaru", new KillObjective(30, 58017));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q70308Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 309);
		SetName("손에 익은 양손검[하이랜더 전직]");
		SetDescription("렘파라사 연못에서 하이랜더 서브 마스터와 대화하세요.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 30 Gosaru", new KillObjective(30, 58017));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q70310Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 311);
		SetName("형식적인 시험[하플라이트 전직]");
		SetDescription("오르샤에서 하플라이트 마스터와 대화하세요.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 30 Raffly", new KillObjective(30, 400521));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q70311Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 312);
		SetName("전사가 갖출 덕목[바바리안 전직]");
		SetDescription("파우피스 나루에서 바바리안 서브 마스터와 대화하세요.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 30 Gosaru", new KillObjective(30, 58017));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q70314Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 315);
		SetName("약초 캐기 수행[프리스트 전직]");
		SetDescription("약초 캐기 수행[프리스트 전직]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 3 Wormwood", new CollectItemObjective(666060, 3));
		AddObjective("o2", "Collect 3 Hemostasis Herb", new CollectItemObjective(666061, 3));
		AddObjective("o3", "Collect 3 Moonlight Flower", new CollectItemObjective(666062, 3));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q70315Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 316);
		SetName("보코르라면 역시...[보코르 전직]");
		SetDescription("보코르라면 역시...[보코르 전직]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Bokor Doll", new CollectItemObjective(650065, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q70317Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 318);
		SetName("창작에는 도구가 필요하지[딥디르비 전직](2)");
		SetDescription("창작에는 도구가 필요하지[딥디르비 전직](2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 20 Ore Crystal", new CollectItemObjective(666063, 20));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q70318Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 319);
		SetName("창작에는 도구가 필요하지[딥디르비 전직](3)");
		SetDescription("창작에는 도구가 필요하지[딥디르비 전직](3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Special Order Chisel", new CollectItemObjective(666064, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(35));
	}
}

public class Q70331Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 332);
		SetName("Traces of My Seniors [Hoplite Advancement]");
		SetDescription("Traces of My Seniors [Hoplite Advancement]");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 15 Hoplites' Broken Spear Parts", new CollectItemObjective(666070, 15));
		AddObjective("o2", "Collect 10 Hoplites' Broken Off Armor Pieces", new CollectItemObjective(666071, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(125));
	}
}

public class Q70340Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 341);
		SetName("The Power to Lead Others [Rodelero Advancement]");
		SetDescription("Go meet Rodelero Master in Orsha");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Colimencia", new KillObjective(1, 57189));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q70341Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 342);
		SetName("The One Who Goes Through Everything [Cataphract Advancement]");
		SetDescription("Go meet Cataphract Master in Orsha");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Gaigalas", new KillObjective(1, 57188));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(175));
	}
}

public class Q70409Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 410);
		SetName("Reutilizing Resources");
		SetDescription("Reutilizing Resources");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 16 Useful Arrow", new CollectItemObjective(666083, 16));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(66));
	}
}

public class Q70425Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 426);
		SetName("거점 방어를 위한 첫걸음");
		SetDescription("거점 방어를 위한 첫걸음");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 25 Useful Wooden Board", new CollectItemObjective(666085, 25));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(70));
	}
}

public class Q70427Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 428);
		SetName("A Sorrowful Heart");
		SetDescription("A Sorrowful Heart");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 7 Thorn Mushroom", new CollectItemObjective(666086, 7));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(70));
	}
}

public class Q70428Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 429);
		SetName("Hometown Secret");
		SetDescription("Hometown Secret");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 16 Charog Sap", new CollectItemObjective(666087, 16));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(70));
	}
}

public class Q70452Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.7", 453);
		SetName("빈틈없는 일처리");
		SetDescription("빈틈없는 일처리");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 16 Spell Amplifier", new CollectItemObjective(666090, 16));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(74));
	}
}

public class Q80006Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 7);
		SetName("The Wrong Faith");
		SetDescription("Sigis asked you to tell Bokor Juta that the spell doll was destroyed.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Abomination", new KillObjective(1, 58154));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(60));
		AddReward(new ItemReward(640084, 4));
		AddReward(new ItemReward(900011, 1050));
	}
}

public class Q80016Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 17);
		SetName("Suspicious Sanctum (2)");
		SetDescription("Suspicious Sanctum (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Evil Magic Trace", new CollectItemObjective(667006, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(60));
	}
}

public class Q80018Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 19);
		SetName("Helping Hand");
		SetDescription("Helping Hand");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Hemostasis Herb", new CollectItemObjective(667011, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(82));
	}
}

public class Q80020Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 21);
		SetName("Reclaiming Food");
		SetDescription("Reclaiming Food");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Stolen Food by Ferrets", new CollectItemObjective(667012, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(82));
	}
}

public class Q80024Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 25);
		SetName("Thirst for Drinks");
		SetDescription("Thirst for Drinks");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Gigantic Fruit Juice", new CollectItemObjective(667013, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(82));
	}
}

public class Q80025Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 26);
		SetName("Putting Spices (1)");
		SetDescription("Putting Spices (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Dirty Red Water from the Well", new CollectItemObjective(667014, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(82));
	}
}

public class Q80032Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 33);
		SetName("The Missing Girl (2)");
		SetDescription("The Missing Girl (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Drowsy Herb", new CollectItemObjective(667016, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(86));
	}
}

public class Q80044Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 45);
		SetName("Destroy Vitality Absorption Device");
		SetDescription("Destroy Vitality Absorption Device");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Demon Blood", new CollectItemObjective(667023, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(90));
	}
}

public class Q80046Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 47);
		SetName("The Thing That Should Not Let It Be");
		SetDescription("크루비나 생성 장치를 모두 파괴했습니다. 여신 라다와 대화하세요.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Zaura, Empowered by Giltine", new KillObjective(1, 58203));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(90));
		AddReward(new ItemReward(640085, 4));
		AddReward(new ItemReward(900011, 2280));
		AddReward(new ItemReward(645027, 1));
	}
}

public class Q80064Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 65);
		SetName("Soldiers' Belongings");
		SetDescription("Soldiers' Belongings");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 6 Vedas Plateau Soldier's Belongings", new CollectItemObjective(667036, 6));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(198));
	}
}

public class Q80070Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 71);
		SetName("Support for the Greater Justice (3)");
		SetDescription("Support for the Greater Justice (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 11 Spion Tree Leaves", new CollectItemObjective(667039, 11));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(213));
	}
}

public class Q80075Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 76);
		SetName("For Continuing Research");
		SetDescription("For Continuing Research");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Spion Teeth", new CollectItemObjective(667041, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(213));
	}
}

public class Q80082Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 83);
		SetName("Wrong Faith (3)");
		SetDescription("Wrong Faith (3)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 13 The red blood of the demons", new CollectItemObjective(667045, 13));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(219));
	}
}

public class Q80090Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 91);
		SetName("Hungry Faith (1)");
		SetDescription("Hungry Faith (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Edible Food Items", new CollectItemObjective(667048, 1));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(222));
	}
}

public class Q80093Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.8", 94);
		SetName("The Corrupted Spirit of the Demons (2)");
		SetDescription("The Corrupted Spirit of the Demons (2)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 10 Holy Water", new CollectItemObjective(640068, 10));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(222));
	}
}

public class Q90003Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.9", 4);
		SetName("The Corrupted Lake (4)");
		SetDescription("You have found a burnt journal at the Floating Altar. Bring it to the elder's granddaughter.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 6 Rajatadpole", new KillObjective(6, 41308));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(55));
		AddReward(new ItemReward(640084, 3));
		AddReward(new ItemReward(900011, 560));
	}
}

public class Q90004Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.9", 5);
		SetName("The Corrupted Lake (5)");
		SetDescription("The Corrupted Lake (5)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 1 Old Disc", new CollectItemObjective(668001, 1));
		AddObjective("o2", "Collect 4 Old Disc", new CollectItemObjective(668001, 4));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(55));
	}
}

public class Q90006Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.9", 7);
		SetName("Offerings to the Goddess (1)");
		SetDescription("Offerings to the Goddess (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Rajatadpole Meat", new CollectItemObjective(668003, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(55));
	}
}

public class Q90008Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.9", 9);
		SetName("일방통행");
		SetDescription("Nikodemas is looking around apprehensively. Talk to Nikodemas.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Rocksodon", new KillObjective(1, 57867));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(55));
		AddReward(new ItemReward(640084, 3));
		AddReward(new ItemReward(900011, 560));
	}
}

public class Q90013Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.9", 14);
		SetName("Clear the Corruption (4)");
		SetDescription("Clear the Corruption (4)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Bait Meat", new CollectItemObjective(668008, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(58));
	}
}

public class Q90015Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.9", 16);
		SetName("Clear the Corruption (6)");
		SetDescription("Hunter Modis seems to have finished all preparations to lure the Hydra. Talk to Hunter Modis.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Tyronas Hydra", new KillObjective(1, 58209));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(58));
		AddReward(new ItemReward(640084, 5));
		AddReward(new ItemReward(900011, 1100));
		AddReward(new ItemReward(645025, 1));
	}
}

public class Q90017Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.9", 18);
		SetName("Recurrence Prevention (1)");
		SetDescription("Recurrence Prevention (1)");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Rajapearlite Lard", new CollectItemObjective(668011, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(58));
	}
}

public class Q90029Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.9", 30);
		SetName("Emergency Treatment");
		SetDescription("Emergency Treatment");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Collect 8 Greentoshell Shell Fragment", new CollectItemObjective(668019, 8));
		AddObjective("o2", "Collect 8 Blue Lapasape Moss", new CollectItemObjective(668020, 8));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(222));
	}
}
