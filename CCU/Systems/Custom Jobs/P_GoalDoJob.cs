using BepInEx.Logging;
using BunnyLibs;
using CCU.Content;
using HarmonyLib;

namespace CCU.Patches.Goals
{
	[HarmonyPatch(declaringType: typeof(GoalDoJob))]
	public static class P_GoalDoJob
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
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
				//GoalTamperSomething subGoal2 = new GoalTamperSomething();
				//__instance.brain.AddSubgoal(__instance, subGoal2);

				return;
			}
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(GoalDoJob.Terminate))]
		public static bool Terminate_Prefix(GoalDoJob __instance)
		{
			// This method is blank anyway. Leaving it activated breaks the hired AI. No clue why.
			//MethodInfo terminate_base = AccessTools.DeclaredMethod(typeof(GoalDoJob).BaseType, nameof(GoalDoJob.Terminate)); 
			//terminate_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

			__instance.brain.RemoveAllSubgoals(__instance);

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

			if (__instance.agent.following != null && __instance.agent.job == "")
			{
				__instance.agent.job = "Follow";
				__instance.agent.jobCode = jobType.Follow;
			}
			else
			{
				__instance.agent.job = "";
				__instance.agent.jobCode = jobType.None;
			}

			if (__instance.curJob == "Attack" && __instance.agent.curTraversable != "Normal")
				__instance.agent.SetTraversable("Normal");

			return false;
		}

	}
}
