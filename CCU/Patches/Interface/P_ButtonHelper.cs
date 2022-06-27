using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(ButtonHelper))]
	public static class P_ButtonHelper
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//[HarmonyPrefix, HarmonyPatch(methodName: nameof(ButtonHelper.RefreshContent), argumentTypes: new[] { typeof(ButtonData) })]
		//public static bool RefreshContent_Prefix(ButtonData myData, ButtonHelper __instance)
		//{
		//	if (GC.challenges.Contains(CMutators.ScrollingButtonTextSizeStatic))
		//		myData.resizeTextForBestFit = false;

		//	return true;
		//}
	}
}
