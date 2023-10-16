using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(typeof(Computer))]
	public static class P_ComputerShutdown
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPriority(1000)]
		[HarmonyTranspiler, HarmonyPatch(nameof(Computer.MakeNonFunctional))]
		private static IEnumerable<CodeInstruction> ShutdownObjects(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo interactingAgent = AccessTools.DeclaredField(typeof(PlayfieldObject), nameof(PlayfieldObject.interactingAgent));
			MethodInfo switchLinkOperate = AccessTools.DeclaredMethod(typeof(ObjectReal), nameof(ObjectReal.SwitchLinkOperate));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldstr, "FlameGrates"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldstr, "Tubes"),
					new CodeInstruction(OpCodes.Ldstr, "Off"),
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldfld, interactingAgent),
					new CodeInstruction(OpCodes.Call, switchLinkOperate),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(ObjectReal))]
	public static class P_ObjectReal_SwitchLinkOperate
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(ObjectReal.SwitchLinkOperate))]
		private static IEnumerable<CodeInstruction> ShutdownObjects(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo switchOffObject = AccessTools.DeclaredMethod(typeof(P_ObjectReal_SwitchLinkOperate), nameof(P_ObjectReal_SwitchLinkOperate.SwitchOffObject));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Stloc_1),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Ldarg_1),
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Ldarg_3),
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Call, switchOffObject),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static void SwitchOffObject(ObjectReal instance, string type, string state, Agent myAgent, ObjectReal otherObject)
		{
			if (type == "Tubes"
				&& otherObject is Tube tube
				&& (tube.startingChunk == instance.startingChunk
					|| (tube.startingSector == instance.startingSector && instance.startingSector != 0)))
			{
				if (state == null)
					tube.ToggleFunctional();
				else if (state == "On")
					tube.MakeFunctional();
				else if (state == "Off")
					tube.MakeNonFunctional(null);
			}
		}
	}
}