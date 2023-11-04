using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU
{
	public static class TraitUnlockManager
	{
		public static bool IsDesignerUnlock(Unlock unlock) =>
		   !(unlock.GetHook<TU_DesignerUnlock>() is null);

		public static bool IsPlayerUnlock(Unlock unlock) =>
			!(unlock.GetHook<TU_PlayerUnlock>() is null);
	}

	[HarmonyPatch(typeof(CharacterCreation))]
	public static class P_CharacterCreation_TraitUnlockManager
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		#region Player Trait Section & CCPV Totaling
		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterCreation.CreatePointTallyText))]
		private static IEnumerable<CodeInstruction> HideDesignerUnlocks(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traitsChosen = AccessTools.DeclaredField(typeof(CharacterCreation), nameof(CharacterCreation.traitsChosen));
			MethodInfo playerUnlockList = AccessTools.DeclaredMethod(typeof(P_CharacterCreation_TraitUnlockManager), nameof(P_CharacterCreation_TraitUnlockManager.FilterDesignerUnlocks));

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
					new CodeInstruction(OpCodes.Call, playerUnlockList),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Stloc_S, 12),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		public static List<Unlock> FilterDesignerUnlocks(List<Unlock> original) =>
			original.Where(u => !TraitUnlockManager.IsDesignerUnlock(u)).ToList();
		#endregion
		#region Designer Trait Section
		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterCreation.CreatePointTallyText))]
		private static IEnumerable<CodeInstruction> PrintDesignerTraitList(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo itemsChosen = AccessTools.DeclaredField(typeof(CharacterCreation), nameof(CharacterCreation.itemsChosen));
			MethodInfo printTraitList = AccessTools.DeclaredMethod(typeof(P_CharacterCreation_TraitUnlockManager), nameof(P_CharacterCreation_TraitUnlockManager.PrintDesignerTraitSection));

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
					new CodeInstruction(OpCodes.Ldarg_0),
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

		public static void PrintDesignerTraitSection(CharacterCreation CC)
		{
			if (Core.designerEdition &&
				CC.traitsChosen.Any(u => TraitUnlockManager.IsDesignerUnlock(u)))
			{
				CC.pointTallyText.text += $"\n<color=yellow>- CCU TRAITS -</color>\n";

				foreach (Unlock unlock in SortUnlocksByName(CC.traitsChosen.Where(u => TraitUnlockManager.IsDesignerUnlock(u)).ToList()))
				{
					string traitName = GC.nameDB.GetName(unlock.unlockName, NameTypes.StatusEffect).Replace($"{Core.CCUBlockTag} ", "");
					CC.pointTallyText.text += $"{traitName}\n";
				}
			}
		}

		public static List<Unlock> SortUnlocksByName(List<Unlock> original) =>
			original.OrderBy(u => GC.nameDB.GetName(u.unlockName, NameTypes.StatusEffect)).ToList();
		#endregion
	}
}