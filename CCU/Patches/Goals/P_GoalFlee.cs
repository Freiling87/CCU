using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
			MethodInfo upperCrusterMagicString = AccessTools.DeclaredMethod(typeof(P_GoalFlee), nameof(P_GoalFlee.UpperCrusterMagicString));

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
					new CodeInstruction(OpCodes.Call, upperCrusterMagicString),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, "UpperCruster")
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static string UpperCrusterMagicString(Agent agent) =>
			agent.agentName == VanillaAgents.UpperCruster
			|| agent.HasTrait<Crusty>()
				? "UpperCruster"
				: "Nope";

	}
}
