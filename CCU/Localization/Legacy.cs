using CCU.Traits.Hire_Type;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Rel_Faction;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Localization
{
    public static class Legacy
	{
		public static Dictionary<string, string> TraitConversions = new Dictionary<string, string>()
		{
			//	0.1.1
			//		Vanilla agent overlap
			{ VanillaAgents.Hacker, nameof(Cyber_Intruder) },
			{ VanillaAgents.Shopkeeper, nameof(General_Store) },
			{ VanillaAgents.Soldier, nameof(Muscle) },
			{ VanillaAgents.Thief, nameof(Intruders_Warehouse) },
			{ VanillaAgents.Vampire, nameof(Bloodsuckers_Bazaar) },
			//		Relationship Refactor
			{ "Bashable", nameof(Faction_Blahd_Aligned) },
			{ "Crushable", nameof(Faction_Crepe_Aligned) },
			{ "Hostile_To_Soldier", nameof(Faction_Cannibal_Aligned) },
			{ "Specistist", nameof(Faction_Gorilla_Aligned) },
			{ "Hostile_To_Cannibal", nameof(Faction_Soldier_Aligned) },
			//		Capitalization OCD
			{ "Hostile_To_Vampire", nameof(Hostile_to_Vampire) },
			{ "Hostile_To_Werewolf", nameof(Hostile_to_Werewolf) },
		};
	}
}