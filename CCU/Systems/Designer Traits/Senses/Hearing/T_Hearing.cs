using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Traits.Senses
{
	internal class T_Hearing : T_Senses
	{
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}

	[HarmonyPatch(typeof(BrainUpdate))]
	internal static class P_BrainUpdate_Hearing
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;


		[HarmonyTranspiler, HarmonyPatch(nameof(BrainUpdate.GoalArbitrate))]
		internal static IEnumerable<CodeInstruction> GoalArbitrate_GateHearing(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();

			MethodInfo gateHearing = AccessTools.DeclaredMethod(typeof(P_BrainUpdate_Hearing), nameof(P_BrainUpdate_Hearing.GateHearing));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldc_I4_0),
					new CodeInstruction(OpCodes.Stloc_S, 102), // flag19
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 101), // noise3
					new CodeInstruction(OpCodes.Ldloc_S, 102), // flag19
					new CodeInstruction(OpCodes.Call, gateHearing),
					new CodeInstruction(OpCodes.Stloc_S, 102),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 102), // flag19
					new CodeInstruction(OpCodes.Brfalse),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool GateHearing(Noise noise, bool vanillaResult)
		{
			return true;
		}

		[HarmonyTranspiler, HarmonyPatch(nameof(BrainUpdate.MyUpdate))] 
		internal static IEnumerable<CodeInstruction> MyUpdate_SpawnWalkingNoise(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo agent = AccessTools.DeclaredField(typeof(BrainUpdate), "agent");
			FieldInfo loud = AccessTools.DeclaredField(typeof(Agent), "loud");

			MethodInfo spawnSpecialNoise = AccessTools.DeclaredMethod(typeof(P_BrainUpdate_Hearing), nameof(P_BrainUpdate_Hearing.SpawnWalkingNoise));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0), // In case error because injecting at top of branch
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, spawnSpecialNoise),
					new CodeInstruction(OpCodes.Ldarg_0), // Replace original in stack left in prefix
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, agent),
					new CodeInstruction(OpCodes.Ldfld, loud),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static void SpawnWalkingNoise(BrainUpdate brainUpdate)
		{
			Agent agent = (Agent)AccessTools.DeclaredField(typeof(BrainUpdate), "agent").GetValue(brainUpdate);

			if (agent.HasTrait(VanillaTraits.Loud))
				GC.spawnerMain.SpawnNoise(agent.curPosition, 1.5f, agent, "T_Hearing_Loud", agent);
			else if (!agent.HasTrait(VanillaTraits.Graceful))
				GC.spawnerMain.SpawnNoise(agent.curPosition, 0.5f, agent, "T_Hearing_Normal", agent);
		}

	}
}