using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits;
using HarmonyLib;
using RogueLibsCore;
using UnityEngine.UI;

namespace CCU.Patches.Inventory
{
	[HarmonyPatch(declaringType: typeof(CharacterCreation))]
	public static class P_CharacterCreation
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(CharacterCreation.CreatePointTallyText))]
		private static IEnumerable<CodeInstruction> CreatePointTallyText_FilterCcpCount(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traitsChosen = AccessTools.DeclaredField(typeof(CharacterCreation), nameof(CharacterCreation.traitsChosen));
			MethodInfo filterTraitList = AccessTools.DeclaredMethod(typeof(T_CCU), nameof(T_CCU.PlayerTraitList), parameters: new Type[] { typeof(List<Unlock>) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, traitsChosen)
					
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, filterTraitList),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Stloc_S, 12),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(CharacterCreation.CreatePointTallyText))]
		private static IEnumerable<CodeInstruction> CreatePointTallyText_CustomCCUSection(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo itemsChosen = AccessTools.DeclaredField(typeof(CharacterCreation), nameof(CharacterCreation.itemsChosen));
			MethodInfo printTraitList = AccessTools.DeclaredMethod(typeof(P_CharacterCreation), nameof(P_CharacterCreation.PrintTraitList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{

				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, printTraitList),
					new CodeInstruction(OpCodes.Ldarg_0), // Replace original
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, itemsChosen),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldc_I4_0),
					new CodeInstruction(OpCodes.Ble_S),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		public static void PrintTraitList(CharacterCreation characterCreation)
        {
			if (T_CCU.DesignerTraitList(characterCreation.traitsChosen).Any())
				characterCreation.pointTallyText.text += "\n<color=yellow>- CCU Traits -</color>\n";

			foreach (Unlock unlock in T_CCU.DesignerTraitList(characterCreation.traitsChosen).OrderBy(t => T_CCU.DesignerName(t.GetType())))
				characterCreation.pointTallyText.text += GC.nameDB.GetName(unlock.unlockName, "StatusEffect") + "\n";
		}

		// TODO: This was to manage trait maximums. It can be ignored for now.

		//[HarmonyPostfix, HarmonyPatch(methodName: nameof(CharacterCreation.AddToList), argumentTypes: new[] { typeof(string), typeof(string) })]
		//public static void AddToList_Postfix(string listType, string unlockName, CharacterCreation __instance)
		//{
		//	Core.LogMethodCall();	
		//	logger.LogDebug("\tlistType: " + listType + "\n\tunlockName: " + unlockName);

		//	if (listType == "Traits" && TraitManager.AllCCUTraitNamesGroup.Contains(unlockName))
		//		__instance.traitLimit += 1;
		//}

		//[HarmonyPostfix, HarmonyPatch(methodName: nameof(CharacterCreation.RemoveFromList), argumentTypes: new[] { typeof(string), typeof(string) })]
		//public static void RemoveFromList_Postfix(string listType, string unlockName, CharacterCreation __instance)
		//{
		//	Core.LogMethodCall();
		//	logger.LogDebug("\tlistType: " + listType + "\n\tunlockName: " + unlockName);

		//	if (listType == "Traits" && TraitManager.AllCCUTraitNamesGroup.Contains(unlockName))
		//		__instance.traitLimit -= 1;
		//}
	}
}