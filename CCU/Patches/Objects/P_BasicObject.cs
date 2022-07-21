using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Systems.Readables;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CCU.Patches.Objects
{
    [HarmonyPatch(declaringType: typeof(BasicObject))]
    public static class P_BasicObject
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(methodName: nameof(BasicObject.Spawn), new[] {typeof(SpawnerBasic), typeof(string), typeof(Vector2), typeof(Vector2), typeof(Chunk))]
		public static IEnumerable<CodeInstruction> Spawn_SetupReadables(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo magicObjectName = AccessTools.DeclaredMethod(typeof(Readables), nameof(Readables.MagicObjectName));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Ldstr, "Sign"),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_2),
					new CodeInstruction(OpCodes.Call, magicObjectName),
					new CodeInstruction(OpCodes.Ldstr, "Sign"),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}
