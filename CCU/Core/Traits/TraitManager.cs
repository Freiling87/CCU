using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU
{
	public static class TraitManager
	{
		public static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// TODO: The extra check here accommodates hybrid traits like Speaks English.
		//		Obvious code smell, meaning you should be using interfaces for some of these trait type abstracts.
		public static bool IsDesignerTrait(Trait trait) =>
			!(trait.GetHook<T_DesignerTrait>() is null)
			|| !(trait.GetHook<TU_DesignerUnlock>() is null); 

		public static bool IsPlayerTrait(Trait trait) =>
			!(trait.GetHook<T_PlayerTrait>() is null);
	}

	[HarmonyPatch(typeof(CharacterSelect))]
	public static class P_CharacterSelect_TraitManager
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterSelect.SetupSlotAgent))]
		private static IEnumerable<CodeInstruction> HideDesignerTraits(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo dummyAgent = AccessTools.DeclaredField(typeof(CharacterSelect), nameof(CharacterSelect.dummyAgent));
			FieldInfo statusEffects = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.statusEffects));
			FieldInfo traitList = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.TraitList));
			MethodInfo displayableTraits = AccessTools.DeclaredMethod(typeof(P_CharacterSelect_TraitManager), nameof(P_CharacterSelect_TraitManager.FilterDesignerTraits));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, dummyAgent),
					new CodeInstruction(OpCodes.Ldfld, statusEffects),
					new CodeInstruction(OpCodes.Ldfld, traitList),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, displayableTraits),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Stloc_S, 41),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static List<Trait> FilterDesignerTraits(List<Trait> original) =>
			original.Where(t => !TraitManager.IsDesignerTrait(t)).ToList();
	}

	[HarmonyPatch(typeof(CharacterSheet))]
	public static class P_CharacterSheet_TraitManager
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(CharacterSheet.UpdateStats))]
		private static IEnumerable<CodeInstruction> UpdateStats_HideCCUTraits(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traitList = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.TraitList));
			MethodInfo displayableTraits = AccessTools.DeclaredMethod(typeof(P_CharacterSheet_TraitManager), nameof(P_CharacterSheet_TraitManager.FilterDesignerTraits));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 2,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, " - </color>\n"),
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld, traitList),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, displayableTraits),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static List<Trait> FilterDesignerTraits(List<Trait> original) =>
			original.Where(t => !TraitManager.IsDesignerTrait(t)).ToList();
	}

	[HarmonyPatch(typeof(StatusEffectDisplay))]
	class P_StatusEffectDisplay_TraitManager
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffectDisplay.AddDisplayPiece))]
		public static bool AddDisplayPiece_Prefix(Trait myTrait)
		{
			if (myTrait is null)
				return true;

			return !TraitManager.IsDesignerTrait(myTrait);
		}
	}
}