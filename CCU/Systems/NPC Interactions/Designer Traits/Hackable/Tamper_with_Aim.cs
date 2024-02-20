using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Traits.Explode_On_Death;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Systems.Agent_Interactions.Designer_Traits.Hackable
{
	// Consult Hydrosensitive for how to adapt the below. Or start from scratch.

	//	internal class Tamper_with_Aim
	//	{
	//	}



	//[HarmonyPatch(typeof(ItemFunctions))]
	//internal static class P_ItemFunctions
	//{
	//	private static readonly ManualLogSource logger = BLLogger.GetLogger();
	//	public static GameController GC => GameController.gameController;

	//	[HarmonyTranspiler, HarmonyPatch(nameof(ItemFunctions.TargetObject))]
	//	private static IEnumerable<CodeInstruction> SoftcodeHack(IEnumerable<CodeInstruction> codeInstructions)
	//	{
	//		List<CodeInstruction> instructions = codeInstructions.ToList();

	//		CodeReplacementPatch patch = new CodeReplacementPatch(
	//			expectedMatches: 3,
	//			targetInstructionSequence: new List<CodeInstruction>
	//			{
	//				new CodeInstruction(OpCodes.Ldfld,Seek_and_Destroy.killerRobot),
	//			},
	//			insertInstructionSequence: new List<CodeInstruction>
	//			{
	//				new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
	//			});

	//		patch.ApplySafe(instructions, logger);
	//		return instructions;
	//	}
	//}

	//	[HarmonyPatch(typeof(AgentInteractions))]
	//	internal static class P_AgentInteractions
	//	{
	//		public static readonly ManualLogSource logger = BLLogger.GetLogger();
	//		public static GameController GC => GameController.gameController;

	//		[HarmonyTranspiler, HarmonyPatch(nameof(AgentInteractions.FinishedOperating))]
	//		private static IEnumerable<CodeInstruction> SoftcodeHack(IEnumerable<CodeInstruction> codeInstructions)
	//		{
	//			List<CodeInstruction> instructions = codeInstructions.ToList();

	//			CodeReplacementPatch patch = new CodeReplacementPatch(
	//				expectedMatches: 1,
	//				targetInstructionSequence: new List<CodeInstruction>
	//				{
	//					new CodeInstruction(OpCodes.Ldfld, Seek_and_Destroy.killerRobot),
	//				},
	//				insertInstructionSequence: new List<CodeInstruction>
	//				{
	//					new CodeInstruction(OpCodes.Call, Seek_and_Destroy.isVanillaKillerRobot)
	//				});

	//			patch.ApplySafe(instructions, logger);
	//			return instructions;
	//		}
	//	}
}
