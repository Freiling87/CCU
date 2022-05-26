using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using HarmonyLib;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.RenameMe
{
    [HarmonyPatch(declaringType: typeof(LoadLevel))]
	static class P_LoadLevel_loadStuff2
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "loadStuff2", new Type[] { }));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetNextLevel(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo NextLevelIndex = AccessTools.DeclaredMethod(typeof(P_LoadLevel_loadStuff2), nameof(NextLevelIndex), new[] { typeof(int) });

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				targetInstructionSequence: new List<CodeInstruction>
				{
					//	else
					//		this.customLevel = this.customCampaign.levelList[n];

					new CodeInstruction(OpCodes.Ldloca_S, 14),			// [n]
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Callvirt),
					new CodeInstruction(OpCodes.Stfld),
					new CodeInstruction(OpCodes.Ldloc_2),
					new CodeInstruction(OpCodes.Ldfld),
					new CodeInstruction(OpCodes.Ldc_I4_1),
					new CodeInstruction(OpCodes.Stfld),
					new CodeInstruction(OpCodes.Ldstr, "Loaded Custom Level: ")
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, NextLevelIndex),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static int NextLevelIndex(int n)
        {
			logger.LogDebug("NextLevelIndex");
			
			List<LevelData> levelList = GC.loadLevel.customCampaign.levelList;
			int currentLevelITHINK = GC.sessionDataBig.curLevelEndless - 1;

			logger.LogDebug("Current Level, I THINK:\t" + currentLevelITHINK);

			logger.LogDebug("Campaign levelList: ");
			foreach (LevelData levelData in levelList)
				logger.LogDebug("\t" + levelList.IndexOf(levelData) + ".\t" + levelData.levelName);

			return n; // Returning vanilla until I have more info
		}
	}
}
