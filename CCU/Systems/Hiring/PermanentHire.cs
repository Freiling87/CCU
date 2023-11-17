using BepInEx.Logging;
using CCU.Mutators.Followers;
using CCU.Traits.Hire_Duration;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Systems.PermanentHire
{
	internal class PermanentHire { }

	[HarmonyPatch(typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(Agent.SetEmployer))]
		public static bool SetEmployer_Prefix(Agent __instance, ref Agent myEmployer)
		{
			if (__instance.GetOrAddHook<H_AgentInteractions>().HiredPermanently &&
				!(__instance.employer is null) && myEmployer is null)
			{
				myEmployer = __instance.employer;
				__instance.job = "Follow";
				__instance.jobCode = jobType.Follow;
				__instance.StartCoroutine(__instance.ChangeJobBig(""));
				__instance.oma.cantDoMoreTasks = false;
			}

			return true;
		}

		[HarmonyPrefix, HarmonyPatch(nameof(Agent.SetFollowing))]
		public static bool SetFollowing_Prefix(Agent __instance, ref Agent myFollowing)
		{
			if (__instance.GetOrAddHook<H_AgentInteractions>().HiredPermanently &&
				!(__instance.following is null) && myFollowing is null)
			{
				myFollowing = __instance.employer;
				__instance.job = "Follow";
				__instance.jobCode = jobType.Follow;
				__instance.StartCoroutine(__instance.ChangeJobBig(""));
				__instance.oma.cantDoMoreTasks = false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(AgentInteractions))]
	static class P_AgentInteractions
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(AgentInteractions.LetGo))]
		public static bool LetGo_Prefix(Agent agent)
		{
			agent.GetOrAddHook<H_AgentInteractions>().HiredPermanently = false;
			return true;
		}

		// Based on AgentInteractions.QualifyHireAsProtection
		public static void HirePermanently(Agent agent, Agent interactingAgent, int buttonPrice)
		{
			int teamSize = agent.FindNumFollowing(interactingAgent);
			bool canHire = false;

			if ((interactingAgent.statusEffects.hasTrait(VanillaTraits.TeamBuildingExpert) && teamSize < 3) ||
				(interactingAgent.statusEffects.hasTrait(VanillaTraits.ArmyofFive) && teamSize < 5))
				canHire = true;
			else if (interactingAgent.statusEffects.hasTrait(VanillaTraits.Malodorous) &&
				!interactingAgent.statusEffects.hasTrait(VanillaTraits.Charismatic) &&
				!interactingAgent.statusEffects.hasTrait("Likeable2") &&
				!interactingAgent.statusEffects.hasTrait("NiceSmelling"))
			{
				agent.SayDialogue("WontJoinA");
				agent.StopInteraction();
			}
			else if (interactingAgent.statusEffects.hasTrait(VanillaTraits.Antisocial))
			{
				agent.SayDialogue("WontJoinA");
				agent.StopInteraction();
			}
			else if (teamSize < 1)
				canHire = true;
			else
			{
				agent.SayDialogue("WontJoinB");
				agent.StopInteraction();
			}

			if (!canHire)
				return;

			if (!agent.moneySuccess(buttonPrice))
			{
				agent.StopInteraction();
				return;
			}

			//agent.agentInteractions.AssistMe(agent, interactingAgent); // Replaced:
			agent.SayDialogue("Joined");
			agent.gc.audioHandler.Play(agent, "AgentJoin");
			agent.relationships.StartCoroutine(agent.relationships.joinPartyDelay(interactingAgent, "HireAsProtection"));
			//
			agent.SetChangeElectionPoints(interactingAgent);
			agent.StopInteraction();
			agent.GetOrAddHook<H_AgentInteractions>().HiredPermanently = true;
			agent.canGoBetweenLevels = true;

			return;
		}
	}

	[HarmonyPatch(typeof(ExitPoint))]
	class P_ExitPoint
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(ExitPoint.EmployeesExit))]
		public static bool DetermineHomesickness(Agent myAgent, ExitPoint __instance)
		{
			for (int i = 0; i < GC.agentList.Count; i++)
			{
				Agent employee = GC.agentList[i];

				if (employee != myAgent && employee.employer == myAgent)
				{
					// Negatives allow traits to take precedence over mutators
					if (myAgent.isPlayer == 0)
						employee.agentInteractions.LetGo(employee, employee.employer);
					else if (GC.challenges.Contains(nameof(Homesickness_Mandatory))
							&& (!employee.HasTrait<Homesickless>()) || employee.HasTrait<Homesickly>())
					{
						employee.SayDialogue("CantCome");
						employee.agentInteractions.LetGo(employee, employee.employer);
					}
					else if (GC.challenges.Contains(nameof(Homesickness_Disabled))
							&& (!employee.HasTrait<Homesickly>())
								|| employee.HasTrait<Homesickless>()
								|| employee.GetOrAddHook<H_AgentInteractions>().HiredPermanently
								|| employee.canGoBetweenLevels
								|| myAgent.statusEffects.hasTrait("AgentsFollowToNextLevel"))
						employee.wantsToExit = true;
					else
					{
						employee.SayDialogue("CantCome");
						employee.agentInteractions.LetGo(employee, employee.employer);
					}
				}
			}

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(nameof(ExitPoint.FinishLevel))]
		public static bool FinishLevel(Agent myAgent)
		{
			myAgent.GetOrAddHook<H_Appearance>().mustRollAppearance = false;
			return true;
		}
	}

	[HarmonyPatch(typeof(ObjectMultAgent))]
	public static class P_ObjectMultAgent
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(ObjectMultAgent.cantDoMoreTasks), MethodType.Setter)]
		public static bool cantDoMoreTasks_Setter_Prefix(ref bool value, ObjectMultAgent __instance)
		{
			if (__instance.agent.HasTrait<Permanent_Hire>() || __instance.agent.HasTrait<Permanent_Hire_Only>())
				value = false;

			return true;
		}
	}
}