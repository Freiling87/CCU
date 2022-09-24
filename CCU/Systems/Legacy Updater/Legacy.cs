using CCU.Challenges.Followers;
using CCU.Traits.Hire_Type;
using CCU.Traits.Loadout;
using CCU.Traits.Loadout_Misc;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Rel_Faction;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Localization
{
    public static class Legacy
	{
		//	Use CLASS name, not display name!

		public static Dictionary<string, Type> ChallengeConversions = new Dictionary<string, Type>()
		{
			//	0.1.1
			//		Update to standard format, elimination of CMutator lists
			{ "[CCU] Homesickness Disabled",        typeof(Homesickness_Disabled) },
			{ "[CCU] Homesickness Mandatory",		typeof(Homesickness_Mandatory) },
		};

		public static Dictionary<string, Type> TraitConversions = new Dictionary<string, Type>()
		{
			//	0.1.1
			//		Vanilla agent overlap
			{ VanillaAgents.Hacker,					typeof(Cyber_Intruder) },
			{ VanillaAgents.Shopkeeper,				typeof(General_Store) },
			{ VanillaAgents.Soldier,				typeof(Muscle) },
			{ VanillaAgents.Thief,					typeof(Intruders_Outlet) },
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
			//		More OCD
			{ "Manager_Safe_Combo",					typeof(Chunk_Safe_Combo) },
			{ "Manager_Key",                        typeof(Chunk_Key) },
			{ "Manager_Mayor_Badge",                typeof(Chunk_Mayor_Badge) },
		};
	}
}