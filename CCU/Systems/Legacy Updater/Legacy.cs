using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Mutators.Followers;
using CCU.Traits.Hire_Type;
using CCU.Traits.Loadout_Chunk_Items;
using CCU.Traits.Merchant_Type;
using CCU.Traits.Rel_Faction;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

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

	[HarmonyPatch(typeof(Agent))]
	internal static class P_Agent
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//	TODO: ISetupAgentStats
		[HarmonyTranspiler, HarmonyPatch(nameof(Agent.SetupAgentStats))]
		private static IEnumerable<CodeInstruction> SetupAgentStats_LegacyUpdater(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traits = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.traits));
			MethodInfo updateTraitList = AccessTools.DeclaredMethod(typeof(Legacy), nameof(Legacy.UpdateTraitList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, traits),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, updateTraitList),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(CharacterCreation))]
	public static class P_CharacterCreation
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterCreation.LoadCharacter2))]
		private static IEnumerable<CodeInstruction> ReplaceLegacyTraits(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traits = AccessTools.DeclaredField(typeof(SaveCharacterData), nameof(SaveCharacterData.traits));
			MethodInfo updateTraitList = AccessTools.DeclaredMethod(typeof(Legacy), nameof(Legacy.UpdateTraitList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, traits),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, updateTraitList),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(GameController))]
	public static class P_GameController
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch("Awake")]
		public static void Awake_Postfix()
		{
			List<string> removals = new List<string>();

			foreach (string mutator in GC.sessionDataBig.challenges)
				if (Legacy.MutatorConversions.ContainsKey(mutator))
					removals.Add(mutator);

			foreach (string removal in removals)
			{
				GC.sessionDataBig.challenges.Remove(removal);
				string replacement = Legacy.MutatorConversions[removal].Name;
				GC.sessionDataBig.challenges.Add(replacement);
			}
		}
	}

	[HarmonyPatch(typeof(NameDB))]
	public static class P_NameDB
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// This is a hacky way of displaying outdated traits correctly in character selection. 
		/// Since they're no longer in the assembly, they'll show as E_TraitName in the list.
		/// Once they open the character in the editor, the traits are replaced as described in the Legacy Trait Updater.
		/// </summary>
		/// <param name="myName"></param>
		/// <param name="type"></param>
		/// <param name="__result"></param>
		[HarmonyPostfix, HarmonyPatch(nameof(NameDB.GetName))]
		public static void GetName_Postfix(string myName, string type, ref string __result)
		{
			// TODO: Make this iterate with a while loop to be able to rename multiple generations of releases.
			if (type != "StatusEffect" || !__result.Contains("E_"))
				return;

			foreach (Type[] traitOutput in Legacy.TraitConversions.Values)
			{
				foreach (Type trait in traitOutput)
				{
					string traitName = T_DesignerTrait.DesignerName(trait);

					if (__result == "E_" + traitName)
					{
						__result = __result.Remove(0, 2);
						return;
					}
				}
			}
		}
	}
}