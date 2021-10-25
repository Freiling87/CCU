using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RogueLibsCore;
using BepInEx;
using BepInEx.Logging;

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
					else if (!GC.challenges.Contains(CMutators.HomesicknessMandatory) && (employee.canGoBetweenLevels || myAgent.statusEffects.hasTrait("AgentsFollowToNextLevel") || GC.challenges.Contains(CMutators.HomesicknessDisabled)))
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
