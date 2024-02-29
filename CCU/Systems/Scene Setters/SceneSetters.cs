using BepInEx.Logging;
using BTHarmonyUtils;
using BTHarmonyUtils.TranspilerUtils;
using CCU.Status_Effects;
using CCU.Systems.Appearance;
using CCU.Traits.Passive;
using HarmonyLib;
using JetBrains.Annotations;
using RogueLibsCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Systems.CustomGoals
{
	class SceneSetters
	{
		// If you want a class for other custom goals, do it separately or branch this

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
			Teleport_Duo = "Random Teleport (Duo)",
			Teleport_Gang = "Random Teleport (Gang)",
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
			Teleport_Duo,
			Teleport_Gang,
			Teleport_Public,
			Zombified,
		};
		public static List<string> SceneSetters_Legacy = new List<string>()
		{
			RandomTeleport,
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
				if (agent.GetHook<H_AgentInteractions>().SceneSetterFinished
					|| (!SceneSetters_Active.Contains(agent.defaultGoal) && !SceneSetters_Legacy.Contains(agent.defaultGoal)))
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

				case Teleport_Duo:
					Vector2 newLocation = DoRandomTeleport(agent, false, true);
					SpawnGang(agent, 1, newLocation);

					break;

				case Teleport_Gang:
					Vector2 newLocation2 = DoRandomTeleport(agent, false, true);
					SpawnGang(agent, 3, newLocation2);

					break;

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

		private static void SpawnGang(Agent leader, int followers, Vector2 location)
		{
			if (location == Vector2.zero)
				location = leader.curPosition;

			leader.gangLeader = true;
			Agent.gangCount++;
			List<Agent> gang = new List<Agent>() { leader };

			for (int i = 0; i < followers; i++)
			{
				Vector2 newPos = location + new Vector2(Random.Range(-0.32f, 0.32f), Random.Range(-0.32f, 0.32f));
				Agent follower = GC.spawnerMain.SpawnAgent(newPos, null, VanillaAgents.CustomCharacter, "", leader);
				AppearanceTools.SetupAppearance(follower.agentHitboxScript);
				gang.Add(follower);
				leader.gangMembers.Add(follower);
			}

			foreach (Agent member in gang)
			{
				member.SetDefaultGoal(VAgentGoal.WanderFar);
				member.gang = Agent.gangCount;
				member.modLeashes = 0;
				member.agentActive = true;

				foreach (Agent otherMember in gang.Where(om => om != member))
				{
					member.relationships.SetRelInitial(otherMember, nameof(relStatus.Aligned));
					otherMember.relationships.SetRelInitial(member, nameof(relStatus.Aligned));

				}
			}
		}

		private static Vector3 DoRandomTeleport(Agent agent, bool allowPrivate, bool allowPublic)
		{
			Vector3 targetLoc;
			Vector3 entryElevatorLoc = GC.elevatorDown.tr.position;
			int attempts = 0;

			do
			{
				targetLoc = GC.tileInfo.FindRandLocation(agent, allowPrivate, true);
				attempts++;
			}
			while (
				Vector2.Distance(targetLoc, agent.tr.position) < 16f 
				&& Vector2.Distance(targetLoc, entryElevatorLoc) < 32f // For some reason in practice it seems to accept >~20f but that was about my goal anyway.
				&& attempts < 50);

			if (targetLoc == Vector3.zero)
				targetLoc = agent.tr.position;

			agent.Teleport(targetLoc, false, true);
			try { agent.agentCamera.fastLerpTime = 1f; }
			catch { }

			return targetLoc;
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
			MethodInfo updateList = AccessTools.DeclaredMethod(typeof(SceneSetters), nameof(SceneSetters.CustomGoalList));
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
			SceneSetters.RunSceneSetters();
		}
	}

	[HarmonyPatch(typeof(LoadLevel))]
	public static class P_LoadLevel_SetupMore4_2
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyTargetMethod, UsedImplicitly]
		private static MethodInfo Find_MoveNext_MethodInfo() =>
			PatcherUtils.FindIEnumeratorMoveNext(AccessTools.Method(typeof(LoadLevel), "SetupMore4_2"));

		[HarmonyTranspiler, UsedImplicitly]
		private static IEnumerable<CodeInstruction> PreventShapeshifterOnSceneSetters(IEnumerable<CodeInstruction> codeInstructions)
		{
			List<CodeInstruction> instructions = codeInstructions.ToList();
			MethodInfo customMethod = AccessTools.DeclaredMethod(typeof(P_LoadLevel_SetupMore4_2), nameof(P_LoadLevel_SetupMore4_2.CanBeShapeshifter));
			FieldInfo gc = AccessTools.DeclaredField(typeof(LoadLevel), "gc");

			CodeReplacementPatch patch = new CodeReplacementPatch(
				pullNextLabelUp: false,
				expectedMatches: 1,
				prefixInstructions: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_1),
					new CodeInstruction(OpCodes.Ldfld, gc),
					new CodeInstruction(OpCodes.Ldc_I4_1),
					new CodeInstruction(OpCodes.Callvirt),		//	percentChance
				},
				targetInstructions: new List<CodeInstruction>
				{
				},
				insertInstructions: new List<CodeInstruction>
				{
					new CodeInstruction(OpCodes.Ldloc_S, 32),	//	agent11
					new CodeInstruction(OpCodes.Call, customMethod),
				},
				postfixInstructions: new List<CodeInstruction>
				{
				});

			patch.ApplySafe(instructions, logger);
			return instructions;
		}
		private static bool CanBeShapeshifter(bool vanilla, Agent agent) =>
			vanilla
				&& (agent.HasTrait<Possessed>()
					|| (!SceneSetters.SceneSetters_Active.Contains(agent.defaultGoal) && !SceneSetters.SceneSetters_Legacy.Contains(agent.defaultGoal)));
	}
}