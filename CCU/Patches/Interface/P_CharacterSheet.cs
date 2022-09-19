﻿using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(CharacterSheet))]
	public static class P_CharacterSheet
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(CharacterSheet.UpdateStats))]
		private static IEnumerable<CodeInstruction> UpdateStats_HideCCUTraits(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo traitList = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.TraitList));
			MethodInfo displayableTraits = AccessTools.DeclaredMethod(typeof(T_CCU), nameof(T_CCU.PlayerTraitList));

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
	}
}
