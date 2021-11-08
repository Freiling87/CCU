using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Reflection;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(declaringType: typeof(SlimeBarrel))]
	class P_SlimeBarrel
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//[HarmonyPrefix, HarmonyPatch(methodName: "Start")]
		public static bool Start(SlimeBarrel __instance)
		{
			MethodInfo start_base = AccessTools.DeclaredMethod(typeof(SlimeBarrel).BaseType, "Start");
			start_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

			//if (GC.levelTheme != 1 && GC.levelTheme != 2 && !GC.challenges.Contains("MixedUpLevels") && GC.serverPlayer)
			//{
			//	__instance.objectName = "SlimeBarrel";
			//	__instance.RemoveMe();
			//}
			//else
			//	_ = __instance.wasSwitch; // Who fuckin' knows? But it ain't broke.

			MethodInfo waitToStart_base = AccessTools.DeclaredMethod(typeof(SlimeBarrel).BaseType, "WaitToStart");
			waitToStart_base.GetMethodWithoutOverrides<Action>(__instance).Invoke();

			return false;
		}
	}
}
