using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Traits
{
	public interface IRefreshAtLevelStart
    {
        void RefreshAtLevelStart(Agent agent);
    }

	[HarmonyPatch(declaringType: typeof(LoadLevel))]
	internal static class P_LoadLevel_SetupMore42_IRefreshAtLevelStart
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "SetupMore4_2"));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetupMore4_2_GeneralLoadouts(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo refreshAgents = AccessTools.DeclaredMethod(typeof(P_LoadLevel_SetupMore42_IRefreshAtLevelStart), nameof(P_LoadLevel_SetupMore42_IRefreshAtLevelStart.RefreshAgents));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, "SETUPMORE4_7"),
					new CodeInstruction(OpCodes.Call),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, refreshAgents),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		public static void RefreshAgents()
		{
			foreach (Agent agent in GC.agentList)
				foreach (IRefreshAtLevelStart trait in agent.GetTraits<IRefreshAtLevelStart>())
					trait.RefreshAtLevelStart(agent);
		}
	}
}