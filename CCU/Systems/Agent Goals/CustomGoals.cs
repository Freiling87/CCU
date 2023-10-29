using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Status_Effects;
using HarmonyLib;
using RogueLibsCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Systems.CustomGoals
{
	class CustomGoals
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public const string
			//  Scene Setters
			Arrested = "Arrested",
			Burned = "Burned",
			Dead = "Dead",
			Gibbed = "Gibbed",
			KnockedOut = "Knocked Out",
			//Random_Patrol_Chunk = "Random Patrol (Chunk)",
			//Random_Patrol_Map = "Random Patrol (Map)",
			Teleport_Public = "Random Teleport (Public)",
			//Teleport_Private = "Random Teleport (Private)",
			//Teleport_Prison = "Random Teleport (Private + Prison)",
			Zombified = "Zombified",

			//  Permanent Status Effects
			Electrocuted = "Electrocuted",
			Electrocuted_Permanent = "Electrocuted (Permanent)",
			Frozen = "Frozen",
			Frozen_Fragile = "Frozen (Fragile)",
			Frozen_Permanent = "Frozen (Permanent)",

			//  Other Customs
			//Panic = "Panic",
			//WanderAgents = "Wander Between Agents",
			//WanderAgentsNonOwners = "Wander Between Agents (Non-Owner)",
			//WanderAgentsOwners = "Wander Between Agents (Owner)",
			//WanderObjects = "Wander Between Objects (Non-Owner)",
			//WanderObjectsOwned = "Wander Between Objects (Owner)",

			//  Vanilla Unlocked
			CommitArson = "Commit Arson",
			FleeDanger = "Flee Danger",
			RobotClean = "RobotClean",

			//  Legacy
			RandomTeleport = "Random Teleport", // Redirects to Teleport Public

			NoMoreSemicolon = "";

		public static List<string> SceneSetters_Active = new List<string>()
		{
			Arrested,
			Burned,
			Dead,
			Electrocuted,
			Electrocuted_Permanent,
			Frozen,
			Frozen_Fragile,
			Frozen_Permanent,
			Gibbed,
			KnockedOut,
            // RandomTeleport, // Legacy
            Teleport_Public,
			Zombified,
		};
		public static List<string> SceneSetters_All = new List<string>()
		{
			Arrested,
			Burned,
			Dead,
			Electrocuted,
			Electrocuted_Permanent,
			Frozen,
			Frozen_Fragile,
			Frozen_Permanent,
			Gibbed,
			KnockedOut,
			//Random_Patrol_Chunk,
			//Random_Patrol_Map,
			RandomTeleport, // Legacy
            Teleport_Public,
			Zombified,
		};
		public static List<string> ActualGoals_Active = new List<string>()
		{
			//      Custom
			//  Investigate             //  Wander Agents + Wander Objects
			//  WanderAgents,           
			//  WanderAgentsNonOwners,  
			//  WanderAgentsOwners,
			//  WanderObjects,
			//  WanderObjectsOwned,

			//      Vanilla
			//  CommitArson,
			//  FleeDanger, // Probably not gonna work, and not really worth trying too hard on since it's unspecific.
			//  RobotClean,
		};

		public static void RunSceneSetters()
		{
			List<Agent> sceneSetterList = GC.agentList;

			foreach (Agent agent in sceneSetterList)
			{
				if (!SceneSetters_All.Contains(agent.defaultGoal) || agent.GetOrAddHook<H_AgentInteractions>().SceneSetterFinished)
					continue;

				GC.StartCoroutine(DoSceneSetter(agent));
			}
		}

		private static IEnumerator DoSceneSetter(Agent agent)
		{
			yield return null;

			int originalOwnerID = agent.ownerID;
			agent.ownerID = 99;

			switch (agent.defaultGoal)
			{
				case Arrested:
					// Copied from AgentInteractions.ArrestAgent
					agent.knockedOut = true;
					agent.knockedOutLocal = true;
					agent.arrested = true;
					agent.gc.tileInfo.DirtyWalls();
					agent.gettingArrestedByAgent = agent;
					agent.lastHitByAgent = agent.gettingArrestedByAgent;
					agent.justHitByAgent2 = agent.gettingArrestedByAgent;
					agent.healthBeforeKnockout = agent.health;
					agent.deathMethod = "Arrested";
					agent.deathKiller = agent.gettingArrestedByAgent.agentName;
					agent.statusEffects.ChangeHealth(-200f);
					agent.gettingArrestedByAgent.SetArrestingAgent(null);
					agent.SetGettingArrestedByAgent(null);
					agent.agentHitboxScript.SetWBSprites();
					agent.StopInteraction();
					break;

				case Burned:
					agent.deathMethod = "Fire";
					KillEmSoftly(agent);
					break;

				case Dead:
					agent.deathMethod = "Killed to Death";
					KillEmSoftly(agent);
					break;

				case Electrocuted:
					agent.statusEffects.AddStatusEffect(VanillaEffects.Electrocuted, 9999);
					break;

				case Electrocuted_Permanent:
					agent.AddEffect<Electrocuted_Permanent>(9999);
					break;

				case Frozen:
					agent.statusEffects.AddStatusEffect(VanillaEffects.Frozen, 9999);
					break;

				case Frozen_Fragile:
					agent.AddEffect<Frozen_Fragile>(9999);
					break;

				case Frozen_Permanent:
					agent.AddEffect<Frozen_Permanent>(9999);
					break;

				case Gibbed:
					agent.deathMethod = "Killed to Death";
					agent.statusEffects.ChangeHealth(-200f);
					break;

				case KnockedOut:
					agent.deathMethod = "Sleeby";
					agent.statusEffects.AddStatusEffect(VStatusEffect.Tranquilized);
					agent.tranqTime = 1000;
					break;

				case RandomTeleport: // Legacy, Pre-Release
					goto case Teleport_Public;

				case Teleport_Public:
					DoRandomTeleport(agent, false, true);
					agent.SetDefaultGoal("WanderFar");
					break;

				case Zombified:
					agent.zombieWhenDead = true;
					KillEmSoftly(agent);
					break;
			}

			agent.ownerID = originalOwnerID;
		}

		private static void DoRandomTeleport(Agent agent, bool allowPrivate, bool allowPublic)
		{
			Vector3 targetLoc;
			int attempts = 0;

			do
			{
				targetLoc = GC.tileInfo.FindRandLocation(agent, allowPrivate, true);
				attempts++;
			}
			while (Vector2.Distance(targetLoc, agent.tr.position) < 8f && attempts < 50);

			if (targetLoc == Vector3.zero)
				targetLoc = agent.tr.position;

			agent.Teleport(targetLoc, false, true);
			try { agent.agentCamera.fastLerpTime = 1f; }
			catch { }
		}

		private static void KillEmSoftly(Agent agent)
		{
			// Kills without gibbing
			agent.statusEffects.ChangeHealth(-(agent.currentHealth - 1));
			agent.statusEffects.ChangeHealth(-1);
		}

		public static List<string> CustomGoalList(List<string> vanillaList) =>
			vanillaList
				.Concat(SceneSetters_Active)
				.Concat(ActualGoals_Active)
				.ToList();
	}

	[HarmonyPatch(typeof(LevelEditor))]
	public static class P_LevelEditor
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTranspiler, HarmonyPatch(nameof(LevelEditor.CreateGoalList))]
		private static IEnumerable<CodeInstruction> CreateGoalList_ShowExtendedOptions(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo updateList = AccessTools.DeclaredMethod(typeof(CustomGoals), nameof(CustomGoals.CustomGoalList));
			MethodInfo activateLoadMenu = AccessTools.DeclaredMethod(typeof(LevelEditor), nameof(LevelEditor.ActivateLoadMenu));

			CodeReplacementPatch patch = new CodeReplacementPatch(
				expectedMatches: 1,
				prefixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldstr, "WanderFar"),
					new CodeInstruction(OpCodes.Callvirt),
				},
				insertInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Call, updateList),
					new CodeInstruction(OpCodes.Stloc_1),
				},
				postfixInstructionSequence: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldarg_0),
					new CodeInstruction(OpCodes.Call, activateLoadMenu),
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
	}

	[HarmonyPatch(typeof(LoadLevel))]
	public static class P_LoadLevel
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(LoadLevel.SetupMore5))]
		public static void SetupMore5_Postfix()
		{
			CustomGoals.RunSceneSetters();
		}
	}
}