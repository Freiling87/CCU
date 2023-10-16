using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
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
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
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

	[HarmonyPatch(typeof(LevelEditor))]
	public static class P_LevelEditor_MutatorManager
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
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

		//	Caused an error on proceeding to level 2 in Mayor Village Voyage
		//[Info   : Unity Log] Show Scrolling Menu Traits
		//[Info: Unity Log] Playerr(Agent) - Stealth - 4
		//[Info: Unity Log] Playerr(Agent) - Social - 3
		//[Info: Unity Log] Playerr(Agent) - Trade - 3
		//[Info: Unity Log] CATEGORIES: Playerr(Agent)(2) - Stealth - Social - Trade
		//[Info   : Unity Log] TRAITNUM(Normal): 0
		//[Error: Unity Log] NullReferenceException: Object reference not set to an instance of an object
		//Stack trace:
		//CCU.Mutators.P_ScrollingMenu_MutatorManager.FilterTraitCancellations(Unlock myUnlock, System.Boolean& __result) (at<5860ef8c738c4dc4a3284e520ed7dfb2>:0)
		//ScrollingMenu.CanHaveTrait(Unlock myUnlock) (at<c91d003c54a541caabaa8c305d5e31e5>:0)
		//ScrollingMenu.TraitOK(Unlock myTrait) (at<c91d003c54a541caabaa8c305d5e31e5>:0)
		//ScrollingMenu.FindTrait(System.Int32 traitNum) (at<c91d003c54a541caabaa8c305d5e31e5>:0)
		//ScrollingMenu.OpenScrollingMenu() (at<c91d003c54a541caabaa8c305d5e31e5>:0)
		//MainGUI.ShowScrollingMenu(System.String type, PlayfieldObject otherObject, Agent myAgent) (at<c91d003c54a541caabaa8c305d5e31e5>:0)
		//StatsScreen.NextStep() (at<c91d003c54a541caabaa8c305d5e31e5>:0)
		//StatsScreen.Continue() (at<c91d003c54a541caabaa8c305d5e31e5>:0)
		//PlayerControl.Update() (at<c91d003c54a541caabaa8c305d5e31e5>:0)
		// Tried with different character (Diplomat) but the bug didn't happen. Issue might be specific to Francois character trait list.

		private static List<string> FilterMutatorList(List<string> vanilla)
		{
			logger.LogDebug("===FilterMutatorList");
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
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
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