using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Senses.Vision
{
	internal abstract class T_VisionBeam : T_Senses, ISetupAgentStats, IRefreshAtLevelStart
    {
		internal abstract string ParticleEffectType { get; }

        public override void OnAdded() { }
        public override void OnRemoved() { }

		public void RefreshAtLevelStart(Agent agent)
		{
			SetupAgentStats(agent);
		}
		public void SetupAgentStats(Agent agent)
		{
			agent.agentSecurityBeams.enabled = true;
			agent.tr.Find("AgentSecurityBeams").gameObject.SetActive(true); //agent.agentSecurityBeams.gameObject.SetActive(true); // Maybe cleaner not sure if works tho
			agent.agentSecurityBeams.SetupParticleStuff();
			agent.agentSecurityBeams.SpawnParticles(false);
		}
	}

	[HarmonyPatch(typeof(Agent))]
	internal static class P_Agent_VisionBeams
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(Agent.StartReal3))]
		private static IEnumerable<CodeInstruction> GateVisionBeams(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			MethodInfo visionBeamSoftcode = AccessTools.DeclaredMethod(typeof(P_Agent_VisionBeams), nameof(P_Agent_VisionBeams.VisionBeamSoftcode));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call),
					new CodeInstruction(OpCodes.Ldstr, VanillaAgents.CopBot),
					new CodeInstruction(OpCodes.Call),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, visionBeamSoftcode)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool VisionBeamSoftcode(Agent agent)
		{
			return agent.agentName == VanillaAgents.CopBot
				|| agent.HasTrait<T_VisionBeam>();
		}
	}

	[HarmonyPatch(typeof(AgentSecurityBeams))]
	internal static class P_AgentSecurityBeams_VisionBeams
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		/* Attempted:
		 * ParticleUpdate		Transpiler		Re-gate IsPlayer to set particlesOn
		 */
	}
}