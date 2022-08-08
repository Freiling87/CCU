using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.AgentQuests
{
    [HarmonyPatch(declaringType: typeof(Quests))]
    public static class P_Quests
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyTranspiler, HarmonyPatch(methodName: nameof(Quests.CheckIfBigQuestObject))]
		private static IEnumerable<CodeInstruction> CheckIfBigQuestObject_Scumbag(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo scumbagMagicString = AccessTools.DeclaredMethod(typeof(P_Quests), nameof(P_Quests.ScumbagMagicString));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
                {
					new CodeInstruction(OpCodes.Ldarg_1),
                },
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Ldstr, VanillaAgents.GangsterCrepe),
					new CodeInstruction(OpCodes.Call),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Brtrue),
                },
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, scumbagMagicString)
				});
			 
			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool ScumbagMagicString(PlayfieldObject pfo) =>
			((Agent)pfo).HasTrait<Scumbag>() ||
			pfo.objectName == VanillaAgents.GangsterCrepe;
	}
}