using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Patches.Behaviors
{
	[HarmonyPatch(declaringType: typeof(GoalDoJob))]
	public static class P_GoalDoJob
	{
		[HarmonyPrefix, HarmonyPatch(methodName: nameof(GoalDoJob.Terminate))]
		public static bool Terminate_Prefix (GoalDoJob __instance)
		{
			MethodInfo terminate_base = AccessTools.DeclaredMethod(typeof(GoalDoJob).BaseType, "Terminate");

			terminate_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();
			__instance.brain.RemoveAllSubgoals(__instance);

			if (__instance.curJob == "Ruckus" || 
				__instance.curJob == "HackSomething" || 
				__instance.curJob == "LockpickDoor" || 
				__instance.curJob == "UseToilet" || 
				__instance.curJob == "GetSupplies" || 
				__instance.curJob == "GetDrugs" || 
				__instance.curJob == "GetDrink" || 
				__instance.curJob == "UseATM" ||
				__instance.curJob == "HireTamperTarget" ||
				__instance.curJob == "HireSafecrackTarget")
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
