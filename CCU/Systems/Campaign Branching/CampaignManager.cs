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

namespace CCU.Systems.Campaign_Branching
{
	internal static class CampaignManager
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		public static List<LevelData> LevelList => GC.loadLevel.customCampaign.levelList;
		public static int CurrentLevelIndex => GC.sessionDataBig.curLevelEndless - 1;
		public static LevelData CurrentLevel => LevelList[CurrentLevelIndex];
		public static List<string> CurrentLevelMutators => CurrentLevel.levelMutators;

		// TODO: Move to C_LevelGate
		/// <returns>
		/// Null return = Level does not have this Gate.
		/// </returns>
		internal static bool? IsGateUnlocked(LevelData level, string gate)
		{
			logger.LogDebug("IsGateUnlocked: " + level.levelName + ", Gate " + gate);

			// TODO: If no mutator with that gate on this level, return null

			logger.LogDebug("\tPassed Null Check");

			List<bool> switches = new List<bool> { };

			foreach (Agent agent in GC.agentList)
			{
				bool? flipped = IsSwitchFlipped(agent, gate);

				if (!(flipped is null))
					switches.Add((bool)flipped);
			}

			logger.LogDebug("\tSwitches: " + switches.Where(v => v).Count() + " / " + switches.Count() );

			// TODO:
			// Level Switches .
			// Object Switches 
			// Boolean Logic Gate Mutator elements to List<bool>

			// Default for Level Gates right now: ALL
			bool result = switches.All(v => v);
			return result;
		}

		/// <returns>
		/// Null return = Agent does not have this Switch.
		/// </returns>
		internal static bool? IsSwitchFlipped(Agent agent, string gate)
		{
			if (!agent.GetTraits<T_Switch>().Any(t => t.Gate == gate))
				return null;

			List<bool> traitTriggers = new List<bool> { };

			foreach (T_Trigger trait in agent.GetTraits<T_Trigger>())
				traitTriggers.Add(trait.GetTriggerValue());

			// Analyze list according to Boolean Modifier
			T_SwitchLogic switchConditions = agent.GetTraits<T_SwitchLogic>().FirstOrDefault();
			// This 
			string booleanOperationName = "OR"; // Default placeholder
			bool result = BoolTools.CheckValue(traitTriggers, booleanOperationName);
			logger.LogDebug("Result: " + result);
			return result;
		}

		internal static int NextLevelIndex(int n)
		{
			// Avoid unexpected returns on campaign start.
			if (n == 0)
				return 0;

			// This was good to verify that the patch works, but this variable should already be set by now.
			// At least, that's the case if there's gonna be any element of player choice at Elevator menu.

			return n;
		}
	}

	[HarmonyPatch(typeof(LoadLevel))]
	internal static class P_LoadLevel_loadStuff2_CampaignBranching
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		private static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "loadStuff2"));

		// WARNING: This causes the Papparazzo bug, whatever that is.
		// Might need to be moved up a branch, depending on what levelData.randomizeLevelContent is, since that gates this branch.
		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> SetNextLevel(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(CampaignManager), nameof(CampaignManager.NextLevelIndex));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					//	else
					//		this.customLevel = this.customCampaign.levelList[n];

					new CodeInstruction(OpCodes.Ldloc_S, 14),			// [n]
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Call, customMethod),	//	int
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
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}
}