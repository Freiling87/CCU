using BepInEx.Logging;
using CCU.Content;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Patches.Agents
{
	[HarmonyPatch(declaringType: typeof(GoalDoJob))]
	public static class P_GoalDoJob
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(GoalDoJob.Activate))]
		public static void Activate_Postfix(GoalDoJob __instance)
		{
			if (__instance.curJob == CJob.SafecrackSafe)
			{
				GoalSafecrackSafe subGoal2 = new GoalSafecrackSafe();
				__instance.brain.AddSubgoal(__instance, subGoal2);

				return;
			}
			else if (__instance.curJob == CJob.TamperSomething)
			{
				logger.LogDebug("Not implemented yet");

				//GoalTamperSomething subGoal2 = new GoalTamperSomething();
				//__instance.brain.AddSubgoal(__instance, subGoal2);

				return;
			}
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(GoalDoJob.Terminate))]
		public static bool Terminate_Prefix (GoalDoJob __instance)
		{
			Core.LogMethodCall();
			logger.LogDebug("\tCheckpoint 1");
			MethodInfo terminate_base = AccessTools.DeclaredMethod(typeof(GoalDoJob).BaseType, "Terminate"); 
			terminate_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

			logger.LogDebug("\tCheckpoint 2");
			__instance.brain.RemoveAllSubgoals(__instance);

			logger.LogDebug("\tCheckpoint 3");
			if (__instance.curJob == "Ruckus" || 
				__instance.curJob == "HackSomething" || 
				__instance.curJob == "LockpickDoor" || 
				__instance.curJob == "UseToilet" || 
				__instance.curJob == "GetSupplies" || 
				__instance.curJob == "GetDrugs" || 
				__instance.curJob == "GetDrink" || 
				__instance.curJob == "UseATM" ||
				__instance.curJob == CJob.SafecrackSafe ||
				__instance.curJob == CJob.TamperSomething)
				__instance.agent.StartCoroutine(__instance.agent.JobTransition());

			logger.LogDebug("\tCheckpoint 4");
			if (__instance.agent.following != null && __instance.agent.job == "")
			{
				logger.LogDebug("\tCheckpoint 5a");
				__instance.agent.job = "Follow";
				__instance.agent.jobCode = jobType.Follow;
			}
			else
			{
				logger.LogDebug("\tCheckpoint 5b");
				__instance.agent.job = "";
				__instance.agent.jobCode = jobType.None;
			}

			logger.LogDebug("\tCheckpoint 6");
			if (__instance.curJob == "Attack" && __instance.agent.curTraversable != "Normal")
				__instance.agent.SetTraversable("Normal");

			logger.LogDebug("\tCheckpoint 7");
			return false;
		}
	}
}
