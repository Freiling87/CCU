using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Goals
{
	[HarmonyPatch(declaringType: typeof(GoalFlee))]
	public static class P_GoalFlee
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(GoalFlee.Activate))]
		private static IEnumerable<CodeInstruction> GoalFlee_CrustyTattle(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(Goal), nameof(Goal.agent));
			MethodInfo upperCrusterSoftcode = AccessTools.DeclaredMethod(typeof(P_GoalFlee), nameof(P_GoalFlee.UpperCrusterSoftcode));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Call, upperCrusterSoftcode),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, VanillaAgents.UpperCruster)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static string UpperCrusterSoftcode(Agent agent) =>
			agent.HasTrait<Crusty>()
				? VanillaAgents.UpperCruster
				: agent.agentName;
	}
}