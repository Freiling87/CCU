using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(ScrollingMenu))]
	class P_ScrollingMenu
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(ScrollingMenu.CanHaveTrait), argumentTypes: new[] { typeof(Unlock) })]
		public static bool canHaveTrait_Prefix(Unlock myUnlock, ref bool __result)
		{
			if ((GC.challenges.Contains(CMutators.HomesicknessDisabled) || GC.challenges.Contains(CMutators.HomesicknessMandatory)) && myUnlock.unlockName == VanillaTraits.HomesicknessKiller)
			{
				__result = false;
				return false;
			}
			
			return true;
		}

		// You COULD do it this way, or just learn RegEx...

		//[HarmonyTranspiler, UsedImplicitly, HarmonyPatch(methodName: nameof(ScrollingMenu.GetTraitsChangeTraitRandom), argumentTypes: new Type[0] { })]
		//private static IEnumerable<CodeInstruction> FilterTraitsForChange(IEnumerable<CodeInstruction> codeInstructions)
		//{
		//	List<CodeInstruction> instructions = codeInstructions.ToList();
		//	FieldInfo cantSwap = AccessTools.DeclaredField(typeof(Unlock), nameof(Unlock.cantSwap));
		//	MethodInfo cantSwapOrCCU = AccessTools.DeclaredMethod(typeof(P_ScrollingMenu), nameof(P_ScrollingMenu.CantSwapOrCCU), parameters: new[] { typeof(Unlock) });

		//	CodeReplacementPatch patch = new CodeReplacementPatch(
		//		expectedMatches: 1,
		//		targetInstructionSequence: new List<CodeInstruction>
		//		{
		//			new CodeInstruction(OpCodes.Ldfld, cantSwap),
		//		},
		//		insertInstructionSequence: new List<CodeInstruction>
		//		{
		//			new CodeInstruction(OpCodes.Call, cantSwapOrCCU)
		//		});

		//	patch.ApplySafe(instructions, logger);
		//	return instructions;
		//}

		//[HarmonyTranspiler, UsedImplicitly, HarmonyPatch(methodName: nameof(ScrollingMenu.GetTraitsRemoveTrait), argumentTypes: new Type[0] { })]
		//private static IEnumerable<CodeInstruction> FilterTraitsForRemove(IEnumerable<CodeInstruction> codeInstructions)
		//{
		//	List<CodeInstruction> instructions = codeInstructions.ToList();
		//	FieldInfo cantLose = AccessTools.DeclaredField(typeof(Unlock), nameof(Unlock.cantLose));
		//	MethodInfo cantLoseOrCCU = AccessTools.DeclaredMethod(typeof(P_ScrollingMenu), nameof(P_ScrollingMenu.CantLoseOrCCU), parameters: new[] { typeof(Unlock) });

		//	CodeReplacementPatch patch = new CodeReplacementPatch(
		//		expectedMatches: 1,
		//		targetInstructionSequence: new List<CodeInstruction>
		//		{
		//			new CodeInstruction(OpCodes.Ldfld, cantLose),
		//		},
		//		insertInstructionSequence: new List<CodeInstruction>
		//		{
		//			new CodeInstruction(OpCodes.Call, cantLoseOrCCU)
		//		});

		//	patch.ApplySafe(instructions, logger);
		//	return instructions;
		//}

		//[HarmonyTranspiler, UsedImplicitly, HarmonyPatch(methodName: nameof(ScrollingMenu.GetTraitsRemoveTrait), argumentTypes: new Type[0] { })]
		//private static IEnumerable<CodeInstruction> FilterTraitsForUpgrade(IEnumerable<CodeInstruction> codeInstructions)
		//{
		//	List<CodeInstruction> instructions = codeInstructions.ToList();
		//	MethodInfo cantLoseOrCCU = AccessTools.DeclaredMethod(typeof(P_ScrollingMenu), nameof(P_ScrollingMenu.CantUpgradeOrCCU), parameters: new[] { typeof(Unlock) });

		//	CodeReplacementPatch patch = new CodeReplacementPatch(
		//		expectedMatches: 1,
		//		targetInstructionSequence: new List<CodeInstruction>
		//		{
		//			new CodeInstruction(OpCodes.Ldfld, cantLose),
		//		},
		//		insertInstructionSequence: new List<CodeInstruction>
		//		{
		//			new CodeInstruction(OpCodes.Call, cantLoseOrCCU)
		//		});

		//	patch.ApplySafe(instructions, logger);
		//	return instructions;
		//}

		//public static bool CantLoseOrCCU(Unlock unlock) =>
		//	unlock.cantLose ||
		//	unlock.unlockName.Contains("[CCU]");

		//public static bool CantSwapOrCCU(Unlock unlock) =>
		//	unlock.cantSwap ||
		//	unlock.unlockName.Contains("[CCU]");

		//public static bool CantUpgradeOrCCU(Unlock unlock) =>
		//	unlock. ||
		//	unlock.unlockName.Contains("[CCU]");

	}
}