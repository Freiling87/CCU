using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;
using HarmonyLib;
using BepInEx.Logging;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(ScrollingMenu))]
	class P_ScrollingMenu
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(ScrollingMenu.CanHaveTrait), argumentTypes: new[] { typeof(Unlock) })]
		public static bool canHaveTrait_Prefix(Unlock myUnlock, ref bool __result)
		{
			if (GC.challenges.Contains(CMutators.HomesicknessDisabled) && myUnlock.unlockName == VanillaTraits.HomesicknessKiller)
				__result = false;
			
			return false;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(ScrollingMenu.PushedButton), argumentTypes: new[] { typeof(ButtonHelper) })]
		public static void PushedButton_Postfix(ButtonHelper myButton, ScrollingMenu __instance)
		{
			if (__instance.menuType == "Challenges" || __instance.menuType == "MutatorConfigs")
			{
				if (GC.challenges.Contains(CMutators.DarkerDarkness))
					GC.cameraScript.lightingSystem.EnableAmbientLight = false;
				else
					GC.cameraScript.lightingSystem.EnableAmbientLight = true;
			}
		}
	}
}
