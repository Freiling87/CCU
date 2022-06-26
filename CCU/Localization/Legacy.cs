using CCU.Traits;
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
		public static Dictionary<string, Type> TraitConversions = new Dictionary<string, Type>()
		{
			//	0.1.1
			//		Vanilla agent overlap
			{ VanillaAgents.Hacker,					typeof(Cyber_Intruder) },
			{ VanillaAgents.Shopkeeper,				typeof(General_Store) },
			{ VanillaAgents.Soldier,				typeof(Muscle) },
			{ VanillaAgents.Thief,					typeof(Intruders_Warehouse) },
			{ VanillaAgents.Vampire,				typeof(Bloodsuckers_Bazaar) },
			//		Relationship Refactor
			{ "Bashable",							typeof(Faction_Blahd_Aligned) },
			{ "Crushable",							typeof(Faction_Crepe_Aligned) },
			{ "Hostile_To_Soldier",					typeof(Faction_Cannibal_Aligned) },
			{ "Specistist",							typeof(Faction_Gorilla_Aligned) },
			{ "Hostile_To_Cannibal",				typeof(Faction_Soldier_Aligned) },
			//		Capitalization OCD
			{ "Hostile_To_Vampire",					typeof(Hostile_to_Vampire) },
			{ "Hostile_To_Werewolf",				typeof(Hostile_to_Werewolf) },
		};
	}
}