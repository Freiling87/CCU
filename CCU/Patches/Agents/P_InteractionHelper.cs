using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Behavior;
using CCU.Traits.Player.Language;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Agents
{
    [HarmonyPatch(declaringType: typeof(InteractionHelper))]
    public static class P_InteractionHelper
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(InteractionHelper.CanInteractWithAgent))]
        public static bool CanInteractWithAgent_Prefix(Agent otherAgent, ref bool __result)
        {
            __result = !otherAgent.HasTrait<Brainless>();

            return __result;
        }



		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(InteractionHelper.CanInteractWithAgent))]
		private static IEnumerable<CodeInstruction> SetupAgentStats_LegacyUpdater(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(InteractionHelper), "agent");
			MethodInfo customDialogue = AccessTools.DeclaredMethod(typeof(P_InteractionHelper), nameof(P_InteractionHelper.CustomDialogue));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, "CantSpeakEnglish"),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Pop),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_1), // otherAgent
					new CodeInstruction(OpCodes.Call, customDialogue),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static void CustomDialogue(Agent agent, Agent otherAgent)
		{
			List<string> langs = otherAgent.GetTraits<T_Language>().SelectMany(t => t.LanguageNames).ToList();

			if (langs.Count > 1)
				agent.Say("I can't speak any languages they understand.");
			else if (langs.Count == 1)
				agent.Say("I can't speak " + langs[0] + ".");
			else
				agent.SayDialogue("CantSpeakEnglish");
		}
	}
}