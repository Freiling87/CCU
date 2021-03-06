using BepInEx.Logging;
using CCU.Challenges.Followers;
using HarmonyLib;

namespace CCU.Patches.Objects
{
    [HarmonyPatch(declaringType:typeof(ExitPoint))]
	class P_ExitPoint
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(ExitPoint.EmployeesExit), argumentTypes: new[] { typeof(Agent) })]
		public static bool EmployeesExit_Prefix(Agent myAgent, ExitPoint __instance)
		{
			for (int i = 0; i < GC.agentList.Count; i++)
			{
				Agent employee = GC.agentList[i];

				if (employee != myAgent && employee.employer == myAgent)
				{
					if (myAgent.isPlayer == 0)
						employee.agentInteractions.LetGo(employee, employee.employer);
					else if (GC.challenges.Contains(nameof(Homesickness_Mandatory)))
					{
						employee.SayDialogue("CantCome");
						employee.agentInteractions.LetGo(employee, employee.employer);
					}
					else if (GC.challenges.Contains(nameof(Homesickness_Disabled)) || employee.canGoBetweenLevels || myAgent.statusEffects.hasTrait("AgentsFollowToNextLevel"))
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
	}
}
