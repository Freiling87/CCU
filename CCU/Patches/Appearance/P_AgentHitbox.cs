using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
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
	}
}