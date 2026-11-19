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
		AddReward(new ItemReward(640080, 1));
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
		AddReward(new ItemReward(640080, 1));
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
		AddReward(new ItemReward(640082, 1));
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
		AddReward(new ItemReward(640086, 2));
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
		AddReward(new ItemReward(640086, 1));
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
