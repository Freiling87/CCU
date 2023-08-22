using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Mutators
{
	public static class MutatorManager
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static string ActiveMutatorNameFromList(List<Type> mutators)
		{
			foreach (Type mutator in mutators)
			{
				string name = MutatorName(mutator);

				if (GC.challenges.Contains(name))
					return name;
			}

			logger.LogError("GetActiveMutatorFromList: Null return not expected");
			return null;
		}
		public static Type ActiveMutatorFromList(List<Type> mutators)
		{
			foreach (Type mutator in mutators)
			{
				string name = MutatorName(mutator);

				if (GC.challenges.Contains(name))
					return mutator;
			}

			logger.LogError("GetActiveMutatorFromList: Null return not expected");
			return null;
		}
		public static bool IsMutatorFromListActive(List<Type> list)
		{
			foreach (Type mutator in list)
				if (GC.challenges.Contains(mutator.Name))
					return true;

			return false;
		}
		public static string MutatorName(Type mutator)
		{
			CustomNameProvider provider = RogueFramework.NameProviders.OfType<CustomNameProvider>().First();
			return provider.CustomNames[NameTypes.StatusEffect][mutator.Name].GetCurrent();
		}
		public static List<string> MutatorNames(List<Type> mutators)
		{
			CustomNameProvider provider = RogueFramework.NameProviders.OfType<CustomNameProvider>().First();
			List<string> result = new List<string>();

			foreach (Type mutator in mutators)
				result.Add(provider.CustomNames[NameTypes.StatusEffect][mutator.Name].GetCurrent());

			return result;
		}
	}

	[HarmonyPatch(declaringType: typeof(LevelEditor))]
	public static class P_LevelEditor_MutatorManager
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.CreateMutatorListLevel))]
		private static IEnumerable<CodeInstruction> FilterLevelMutatorList(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo activateLoadMenu = AccessTools.DeclaredMethod(typeof(LevelEditor), nameof(LevelEditor.ActivateLoadMenu));

			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(P_LevelEditor_MutatorManager), nameof(P_LevelEditor_MutatorManager.FilterMutatorList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_0), //	list
					new CodeInstruction(OpCodes.Call, customMethod),
					new CodeInstruction(OpCodes.Stloc_0),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, activateLoadMenu),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.CreateMutatorListCampaign))]
		private static IEnumerable<CodeInstruction> FilterCampaignMutatorList(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo activateLoadMenu = AccessTools.DeclaredMethod(typeof(LevelEditor), nameof(LevelEditor.ActivateLoadMenu));

			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(P_LevelEditor_MutatorManager), nameof(P_LevelEditor_MutatorManager.FilterMutatorList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_0), //	list
					new CodeInstruction(OpCodes.Call, customMethod),
					new CodeInstruction(OpCodes.Stloc_0),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, activateLoadMenu),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static List<string> FilterMutatorList(List<string> vanilla)
		{
			if (GC.levelEditing)
			{
				switch (GC.levelEditor.scrollingMenuType)
				{
					case "LoadMutatorsLevel":
						foreach (Unlock unlock in GC.sessionDataBig.challengeUnlocks)
						{
							M_CCU hook = unlock.GetHook<M_CCU>();
							if (hook is null)
								continue;

							if (hook.ShowInLevelMutatorList && !vanilla.Contains(unlock.unlockName))
								vanilla.Add(unlock.unlockName);
							else
								try { vanilla.Remove(unlock.unlockName); } catch { }
						}
						
						break;

					case "LoadMutatorsCampaign":
						foreach (Unlock unlock in GC.sessionDataBig.challengeUnlocks)
						{
							M_CCU hook = unlock.GetHook<M_CCU>();
							if (hook is null)
								continue;

							if (hook.ShowInCampaignMutatorList && !vanilla.Contains(unlock.unlockName))
								vanilla.Add(unlock.unlockName);
							else
								try { vanilla.Remove(unlock.unlockName); } catch { }
						}

						break;
				}
			}

			return vanilla;
		}
	}

	[HarmonyPatch(typeof(ScrollingMenu))]
	public static class P_ScrollingMenu_MutatorManager
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(ScrollingMenu.SortUnlocks))]
		private static void FilterHomeBaseMutatorList(ScrollingMenu __instance, string unlockType)
		{
			if (unlockType != "Challenge")
				return;

			FieldInfo listUnlocksField = AccessTools.DeclaredField(typeof(ScrollingMenu), "listUnlocks");
			List<Unlock> listUnlocks = (List<Unlock>)listUnlocksField.GetValue(__instance);
			List<Unlock> unlocksToRemove = new List<Unlock>();

			foreach (Unlock unlock in listUnlocks.Where(u => u.unlockName.Contains(Core.CCUBlockTag)))
			{
				M_CCU hook = unlock.GetHook<M_CCU>();

				if (!(hook is null) && !hook.ShowInCampaignMutatorList)
					unlocksToRemove.Add(unlock);
			}

			foreach (Unlock unlockToRemove in unlocksToRemove)
				listUnlocks.Remove(unlockToRemove);

			__instance.numButtons -= unlocksToRemove.Count;

			listUnlocksField.SetValue(__instance, listUnlocks);
		}

		[HarmonyPrefix, HarmonyPatch(nameof(ScrollingMenu.CanHaveTrait))]
		public static bool FilterTraitCancellations(Unlock myUnlock, ref bool __result)
		{
			foreach (string challenge in GC.challenges)
			{
				Unlock unlock = RogueLibs.GetUnlock(challenge, UnlockTypes.Mutator).Unlock;
				M_CCU hook = unlock.GetHook<M_CCU>();

				if (hook is null)
					continue;

				if (hook.TraitCancellations.Contains(myUnlock.unlockName))
				{
					__result = false;
					return false;
				}
			}

			// TEST REPLACEMENT BEFORE DELETING:

			//if ((GC.challenges.Contains(nameof(Homesickness_Disabled)) || GC.challenges.Contains(nameof(Homesickness_Mandatory))) &&
			//    myUnlock.unlockName == VanillaTraits.HomesicknessKiller)
			//{
			//    __result = false;
			//    return false;
			//}

			return true;
		}
	}
}