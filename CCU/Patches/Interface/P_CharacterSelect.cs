using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(CharacterSelect))]
	public static class P_CharacterSelect
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(CharacterSelect.SetupSlotAgent))]
		private static IEnumerable<CodeInstruction> SetupSlotAgent_HideCCUTraits(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo dummyAgent = AccessTools.DeclaredField(typeof(CharacterSelect), nameof(CharacterSelect.dummyAgent));
			FieldInfo statusEffects = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.statusEffects));
			FieldInfo traitList = AccessTools.DeclaredField(typeof(StatusEffects), nameof(StatusEffects.TraitList));
			MethodInfo filteredTraitList = AccessTools.DeclaredMethod(typeof(T_CCU), nameof(T_CCU.DisplayableTraits));

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
					new CodeInstruction(OpCodes.Call, filteredTraitList),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}
