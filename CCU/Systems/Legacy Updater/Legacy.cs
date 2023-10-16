using BepInEx.Logging;
using BunnyLibs;
using CCU.Mutators.Followers;
using CCU.Traits.Hire_Type;
using CCU.Traits.Loadout_Chunk_Items;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Rel_Faction;
using CCU.Traits.Trait_Gate;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Localization
{
	public static class Legacy
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//	Use CLASS name, not display name!

		public static Dictionary<string, Type> MutatorConversions = new Dictionary<string, Type>()
		{
			//	0.1.1
			//		Update to standard format, elimination of CMutator lists
			{ "[CCU] Homesickness Disabled",        typeof(Homesickness_Disabled) },
			{ "[CCU] Homesickness Mandatory",       typeof(Homesickness_Mandatory) },
		};

		public static Dictionary<string, Type[]> TraitConversions = new Dictionary<string, Type[]>()
		{
			//	0.1.1
			//		Vanilla agent overlap - These were shop names given agent names, which breaks in vanilla.
			{ VanillaAgents.Hacker,                 new[] { typeof(Cyber_Intruder) } },
			{ VanillaAgents.Shopkeeper,             new[] { typeof(General_Store) } },
			{ VanillaAgents.Soldier,                new[] { typeof(Muscle) } },
			{ VanillaAgents.Thief,                  new[] { typeof(Intruders_Outlet) } },
			{ VanillaAgents.Vampire,                new[] { typeof(Bloodsuckers_Bazaar) } },

			//		Relationship Refactor
			//			Later amended in 1.1.0
			//{ "Bashable",                           new[] { typeof(Faction_Blahd_Aligned) } },
			//{ "Crushable",                          new[] { typeof(Faction_Crepe_Aligned) } },
			//{ "Hostile_To_Soldier",                 new[] { typeof(Faction_Cannibal_Aligned) } },
			//{ "Specistist",                         new[] { typeof(Faction_Gorilla_Aligned) } },
			//{ "Hostile_To_Cannibal",                new[] { typeof(Faction_Soldier_Aligned) } },

			//		Renames
			{ "Manager_Safe_Combo",                 new[] { typeof(Chunk_Safe_Combo) } },
			{ "Manager_Key",                        new[] { typeof(Chunk_Key) } },
			{ "Manager_Mayor_Badge",                new[] { typeof(Chunk_Mayor_Badge) } },


			//	1.1.0
			//		Relationship Refactor
			{ "Faction_Blahd_Aligned",               new[] { typeof(Blahd_Aligned), typeof(Crepe_Hostile) } },
			{ "Faction_Cannibal_Aligned",           new[] { typeof(Cannibal_Aligned), typeof(Soldier_Hostile) } },
			{ "Faction_Crepe_Aligned",              new[] { typeof(Crepe_Aligned), typeof(Blahd_Hostile) } },
			{ "Faction_Firefighter_Aligned",        new[] { typeof(Firefighter_Aligned) } },
			{ "Faction_Gorilla_Aligned",            new[] { typeof(Gorilla_Aligned), typeof(Scientist_Hostile), typeof(Specistist) } },
			{ "Faction_Soldier_Aligned",            new[] { typeof(Soldier_Aligned), typeof(Cannibal_Hostile) } },

			//		Amendments to 0.1.1 relationship refactor
			{ "Bashable",                           new[] { typeof(Blahd_Aligned), typeof(Crepe_Hostile) } },
			{ "Crushable",                          new[] { typeof(Blahd_Hostile), typeof(Crepe_Aligned) } },
			{ "Hostile_To_Soldier",                 new[] { typeof(Soldier_Hostile) } }, // Both capitalizations were used at some point
			{ "Hostile_to_Soldier",                 new[] { typeof(Soldier_Hostile) } },
			{ "Hostile_To_Cannibal",                new[] { typeof(Cannibal_Hostile) } },
			{ "Hostile_to_Cannibal",                new[] { typeof(Cannibal_Hostile) } },
			{ "Hostile_To_Vampire",                 new[] { typeof(Vampire_Hostile) } },
			{ "Hostile_to_Vampire",                 new[] { typeof(Vampire_Hostile) } },
			{ "Hostile_To_Werewolf",                new[] { typeof(Werewolf_Hostile) } },
			{ "Hostile_to_Werewolf",                new[] { typeof(Werewolf_Hostile) } },
		};

		// Refactored to recursion by BlazingTwist
		public static List<string> UpdateTraitList(List<string> traits) =>
			traits.SelectMany(UpdateTrait).ToList();

		private static List<string> UpdateTrait(string trait)
		{
			string ccuTraitName = trait.StartsWith("E_")
				? trait.Substring(2)
				: trait;

			if (!TraitConversions.ContainsKey(ccuTraitName))
				return new List<string> { trait };

			return TraitConversions[ccuTraitName]
					.SelectMany(type => UpdateTrait(type.Name))
					.ToList();
		}
	}
}