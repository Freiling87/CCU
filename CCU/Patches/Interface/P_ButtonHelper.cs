using BepInEx.Logging;
using BunnyLibs;
using HarmonyLib;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(typeof(ButtonHelper))]
	public static class P_ButtonHelper
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		//[HarmonyPrefix, HarmonyPatch(nameof(ButtonHelper.RefreshContent), argumentTypes: new[] { typeof(ButtonData) })]
		//public static bool RefreshContent_Prefix(ButtonData myData, ButtonHelper __instance)
		//{
		//	if (GC.challenges.Contains(CMutators.ScrollingButtonTextSizeStatic))
		//		myData.resizeTextForBestFit = false;

		//	return true;
		//}
	}
}
