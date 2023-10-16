using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using CCU.Traits.Behavior;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(typeof(Door))]
	public static class P_Door
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(Door.ObjectCollide), argumentTypes: new[] { typeof(PlayfieldObject), typeof(bool) })]
		private static IEnumerable<CodeInstruction> ObjectCollide_LimitWalkthroughToVanillaKillerRobot(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo killerRobot = AccessTools.DeclaredField(typeof(Agent), nameof(Agent.killerRobot));
			MethodInfo isVanillaKillerRobot = AccessTools.DeclaredMethod(typeof(Seek_and_Destroy), nameof(Seek_and_Destroy.IsVanillaKillerRobot));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldfld, killerRobot),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, isVanillaKillerRobot)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}