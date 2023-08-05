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
	/// Prevents spilling items with E_ in the name. Conveniently, this also fixes a number of issues caused by Investigation_Text.
	/// </summary>
	[HarmonyPatch(typeof(ObjectReal))]
	static class No_More_Bad_Drops
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(ObjectReal), "DestroyMe2"));
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> FilterInvestigationTextFromSpilledItems(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			FieldInfo invItemList = AccessTools.DeclaredField(typeof(InvDatabase), nameof(InvDatabase.InvItemList));
			MethodInfo filteredList = AccessTools.DeclaredMethod(typeof(No_More_Bad_Drops), nameof(No_More_Bad_Drops.FilteredInvItemList));

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
		internal static List<InvItem> FilteredInvItemList(List<InvItem> invItemList) =>
			invItemList.Where(invItem => !invItem.invItemName.Contains("E_")).ToList();

	}
}
