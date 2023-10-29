using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Fixes
{
	/// <summary>
	/// This is redundant to the Community Fixes patch, but leave it since Investigateables can cause this bug.
	/// </summary>
	[HarmonyPatch(typeof(ObjectReal))]
	static class NoMoreBadDrops_DestroyMe2
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(ObjectReal), "DestroyMe2"));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> FilterInvestigationTextFromSpilledItems(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo invItemList = AccessTools.DeclaredField(typeof(InvDatabase), nameof(InvDatabase.InvItemList));
			MethodInfo filteredList = AccessTools.DeclaredMethod(typeof(NoMoreBadDrops_DestroyMe2), nameof(NoMoreBadDrops_DestroyMe2.FilteredInvItemList));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
				new CodeInstruction(OpCodes.Ldfld, invItemList),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
				new CodeInstruction(OpCodes.Call, filteredList)
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		public static List<InvItem> FilteredInvItemList(List<InvItem> invItemList) =>
			invItemList.Where(invItem => !invItem.invItemName?.Contains("E_") ?? false).ToList();
	}
}