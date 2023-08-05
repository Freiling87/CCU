﻿using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits.Ambient_Audio
{
	internal abstract class T_AmbientAudio : T_CCU, ISetupAgentStats, IRefreshAtLevelStart
	{
		internal abstract string ambientAudioClipName { get; }

		public void RefreshAtLevelStart(Agent agent)
		{
			SetupAgentStats(agent);
		}

		public void SetupAgentStats(Agent agent)
		{
			agent.ambientAudio = ambientAudioClipName;
			agent.StartCoroutine(agent.WaitToStartAmbientAudio()); // Ensure this isn't accruing
		}
	}

	[HarmonyPatch(typeof(AgentSecurityBeams))]
	internal static class P_AgentSecurityBeams_AmbientAudio
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(AgentSecurityBeams.SpawnParticles))]
		private static IEnumerable<CodeInstruction> GateAmbientAudio(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(AgentSecurityBeams), "agent");

			MethodInfo gateAmbientAudioSoftcode = AccessTools.DeclaredMethod(typeof(P_AgentSecurityBeams_AmbientAudio), nameof(P_AgentSecurityBeams_AmbientAudio.GateAmbientAudioByTrait));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
				},
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Pop),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, gateAmbientAudioSoftcode),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static void GateAmbientAudioByTrait(Agent agent)
		{
			if (agent.GetTraits<T_AmbientAudio>().Any()
				|| agent.agentName == VanillaAgents.CopBot)
				agent.StartCoroutine(agent.WaitToStartAmbientAudio());
		}
	}
}