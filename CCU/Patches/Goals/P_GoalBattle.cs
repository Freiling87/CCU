using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Patches.Agents;
using CCU.Traits.Combat;
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
	[HarmonyPatch(declaringType: typeof(GoalBattle))]
	public static class P_GoalBattle
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(GoalBattle.Process), argumentTypes: new Type[0] { })]
		private static IEnumerable<CodeInstruction> Process_StartCombatActions(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(Goal), nameof(GoalBattle.agent));
			FieldInfo canTakeDrugs = AccessTools.DeclaredField(typeof(Combat), nameof(Combat.canTakeDrugs));
			MethodInfo doCombatActions = AccessTools.DeclaredMethod(typeof(P_GoalBattle), nameof(DoCombatActions));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
                {
					new CodeInstruction(OpCodes.Ldc_I4_1),
					new CodeInstruction(OpCodes.Stloc_0),
                },
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Call, doCombatActions)
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldfld, canTakeDrugs),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static void DoCombatActions(Agent agent)
        {
			if (agent.HasTrait<Backed_Up>() && !agent.GetHook<P_Agent_Hook>().HasUsedWalkieTalkie)
            {
				agent.agentInteractions.UseWalkieTalkie(agent, agent.opponent); // Might be reversed, hard to tell
				agent.GetHook<P_Agent_Hook>().HasUsedWalkieTalkie = true;
            }
        }
	}
}