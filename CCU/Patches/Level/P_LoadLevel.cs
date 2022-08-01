using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Systems.CustomGoals;
using CCU.Traits.Loadout;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CCU.Patches.Level
{
    [HarmonyPatch(declaringType: typeof(LoadLevel))]
	public static class P_LoadLevel
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(LoadLevel.SetupMore5))]
		public static void SetupMore5_Postfix(LoadLevel __instance)
        {
			foreach (Agent agent in GC.agentList)
				CustomGoals.RunSceneSetters(agent);
        }
	}

    [HarmonyPatch(declaringType: typeof(LoadLevel))]
	static class P_LoadLevel_loadStuff2
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "loadStuff2", new Type[] { }));

		// This is what causes the Papparazzo bug. Feature is out of scope for now anyway.
		//[HarmonyTranspiler, UsedImplicitly]
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

					new CodeInstruction(OpCodes.Ldloc_S, 14),			// [n]
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
					new CodeInstruction(OpCodes.Ldloc_S, 14),			//	[n]
					new CodeInstruction(OpCodes.Call, NextLevelIndex),	//	int
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		private static int NextLevelIndex(int n)
        {
			// This runs on campaign start, so this will need to be here to avoid unexpected returns.
			if (n == 0)
				return 0;

			logger.LogDebug("NextLevelIndex");
			
			List<LevelData> levelList = GC.loadLevel.customCampaign.levelList;
			int currentLevelITHINK = GC.sessionDataBig.curLevelEndless - 1;

			logger.LogDebug("Current Level, I THINK:\t" + currentLevelITHINK);

			logger.LogDebug("Campaign levelList: ");
			foreach (LevelData levelData in levelList)
				logger.LogDebug("\t" + levelList.IndexOf(levelData) + ".\t" + levelData.levelName);

			// LOG OUTPUT:
			//[Debug: CCU_P_LoadLevel_loadStuff2] NextLevelIndex
			//[Debug: CCU_P_LoadLevel_loadStuff2] Current Level, I THINK: 0
			//[Debug: CCU_P_LoadLevel_loadStuff2] Campaign levelList:
			//[Debug: CCU_P_LoadLevel_loadStuff2] 0.      !!! Test Level 1
			//[Debug: CCU_P_LoadLevel_loadStuff2]    1.      !!! Test Level 2
			//[Debug: CCU_P_LoadLevel_loadStuff2]    2.      !!! Test Level 3
			//[Debug: CCU_P_LoadLevel_loadStuff2]    3.      !!! Test Level 4
			//[Debug: CCU_P_LoadLevel_loadStuff2]    4.      !!! Test Level 5

			return n;
		}
	}

	[HarmonyPatch(declaringType: typeof(LoadLevel))]
	static class P_LoadLevel_SetupMore4_2
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "SetupMore4_2"));

		[HarmonyTranspiler, HarmonyPatch(methodName: "SetupMore4_2")]
		private static IEnumerable<CodeInstruction> SetupMore4_2_GeneralLoadouts(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo loadoutChunkAccessItems = AccessTools.DeclaredMethod(typeof(P_LoadLevel_SetupMore4_2), nameof(P_LoadLevel_SetupMore4_2.LoadoutChunkAccessItems));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldstr, "SETUPMORE4_7"),
					new CodeInstruction(OpCodes.Call),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, loadoutChunkAccessItems),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}

		/// <summary>
		/// Distributed loadout items that are affected by other agents.
		/// Any non-interacting loadouts should probably be done in a FillAgent prefix.
		/// </summary>
		public static void LoadoutChunkAccessItems()
        {
			foreach (Agent agent in GC.agentList)
            {
				if (agent.HasTrait<Chunk_Key>())
                {
					List<Door> validDoors = GC.objectRealList.Where(or => or is Door door && door.startingChunk == agent.startingChunk && door.locked && door.distributedKey is null && !door.hasDetonator && door.extraVar != 10).Cast<Door>().ToList();
					// extraVar = 10 = "PanicRoom" or something
					if (validDoors.Count > 0)
					{
						foreach (Door door in validDoors)
							door.distributedKey = agent;

						InvItem key = agent.agentInvDatabase.AddItem(vItem.Key, 1);
						key.specificChunk = agent.startingChunk;
						key.specificSector = agent.startingSector;
						key.chunks.Add(agent.startingChunk);
						key.sectors.Add(agent.startingChunk);
						key.contents.Add(
							agent.startingChunkRealDescription == VChunkType.Generic
								? VChunkType.GuardPostSeeWarning
								: agent.startingChunkRealDescription);
						agent.oma.hasKey = true;
					}
                }
				
				if (agent.HasTrait<Chunk_Safe_Combo>())
                {
					List<Safe> validSafes = GC.objectRealList.Where(or => or is Safe safe && safe.startingChunk == agent.startingChunk && safe.locked && safe.distributedKey is null).Cast<Safe>().ToList();

					if (validSafes.Count > 0)
					{
						foreach (Safe safe in validSafes)
							safe.distributedKey = agent;

						InvItem safeCombo = agent.agentInvDatabase.AddItem(vItem.SafeCombination, 1);
						safeCombo.specificChunk = agent.startingChunk;
						safeCombo.specificSector = agent.startingSector;
						safeCombo.chunks.Add(agent.startingChunk);
						safeCombo.sectors.Add(agent.startingChunk);
						safeCombo.contents.Add(agent.startingChunkRealDescription);
						agent.oma.hasKey = true;
					}
				}

				// Mayor Badge: InvDatabase.FillAgent transpiler
            }
		}

	}
}