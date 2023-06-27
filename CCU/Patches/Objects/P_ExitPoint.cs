using BepInEx.Logging;
using CCU.Challenges.Followers;
using CCU.Hooks;
using CCU.Traits.Hire_Duration;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(declaringType:typeof(ExitPoint))]
	class P_ExitPoint
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(ExitPoint.EmployeesExit))]
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
					else if ((GC.challenges.Contains(nameof(Homesickness_Mandatory)) && !employee.HasTrait<Homesickless>()) ||
						employee.HasTrait<Homesickly>())
					{
						employee.SayDialogue("CantCome");
						employee.agentInteractions.LetGo(employee, employee.employer);
					}
					else if ((GC.challenges.Contains(nameof(Homesickness_Disabled)) && !employee.HasTrait<Homesickly>()) ||
							employee.HasTrait<Homesickless>() ||
							employee.GetOrAddHook<H_Agent>().HiredPermanently ||
							employee.canGoBetweenLevels || 
							myAgent.statusEffects.hasTrait("AgentsFollowToNextLevel"))
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

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(ExitPoint.FinishLevel))]
		public static bool FinishLevel(Agent myAgent)
		{
			myAgent.GetOrAddHook<H_Agent>().mustRollAppearance = false;
			return true;
		}
	}
}