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
		AddReward(new ItemReward(645519, 1));
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
		AddReward(new ItemReward(645522, 1));
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
		AddReward(new ItemReward(645521, 1));
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
		AddReward(new ItemReward(645517, 1));
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
		AddReward(new ItemReward(644067, 1));
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
		AddReward(new ItemReward(645527, 1));
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
		AddReward(new ItemReward(645516, 1));
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
	}
}

public class Q30051Original2016QuestScript : QuestScript
{
	protected override void Load()
	{
		SetId("Original2016.3", 52);
		SetName("Owl Statue in Danger");
		SetDescription("The light beads were delivered to the Owl Sculptures on top of a slope. Turn around and follow them to Bonewide Cliff.");
		Original2016Quests.Ids.Add(this.QuestId.Value);
		AddObjective("o1", "Kill 1 Owl Sculpture", new KillObjective(1, 12081));
		SetReceive(QuestReceiveType.Auto);
		AddPrerequisite(new LevelPrerequisite(47));
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
	}
}
