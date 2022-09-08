using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Appearance;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Appearance
{
    [HarmonyPatch(declaringType: typeof(AgentHitbox))]
	public static class P_AgentHitbox
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// This might need to be separate from the other custom methods, since SetupFeatures is weird.
		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentHitbox.chooseFacialHairType))]
		private static IEnumerable<CodeInstruction> ChooseFacialHairType_Custom(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo canHaveFacialHair = AccessTools.DeclaredField(typeof(AgentHitbox), nameof(AgentHitbox.canHaveFacialHair));
			MethodInfo CustomFacialHair = AccessTools.DeclaredMethod(typeof(Appearance), nameof(Appearance.RollFacialHair));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, CustomFacialHair),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, canHaveFacialHair),
					new CodeInstruction(OpCodes.Brfalse),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentHitbox.SetupFeatures))]
		private static IEnumerable<CodeInstruction> SetupFeatures_Custom(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo hasSetup = AccessTools.DeclaredField(typeof(AgentHitbox), nameof(AgentHitbox.hasSetup));
			MethodInfo setupAppearance = AccessTools.DeclaredMethod(typeof(Appearance), nameof(Appearance.SetupAppearance));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldc_I4_1),
					new CodeInstruction(OpCodes.Stfld, hasSetup),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, setupAppearance),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}