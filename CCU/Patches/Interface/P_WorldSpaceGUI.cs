using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Patches.Inventory;
using CCU.Systems.Investigateables;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CCU.Patches.Interface
{
    [HarmonyPatch(typeof(WorldSpaceGUI))]
    public static class P_WorldSpaceGUI
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

		// Who knows
        //[HarmonyTranspiler, HarmonyPatch(methodName: nameof(WorldSpaceGUI.ShowChest), new[] { typeof(GameObject), typeof(InvDatabase), typeof(Agent) })]
		private static IEnumerable<CodeInstruction> ShowChest_FilterNotes(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo chestDatabase = AccessTools.DeclaredField(typeof(InvInterface), nameof(InvInterface.chestDatabase));
			MethodInfo filteredInvDatabase = AccessTools.DeclaredMethod(typeof(Investigateables), nameof(Investigateables.FilteredInvDatabase));

			CodeReplacementPatch patch = new CodeReplacementPatch(
                expectedMatches: 1,
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Stfld, chestDatabase),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, filteredInvDatabase)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}
