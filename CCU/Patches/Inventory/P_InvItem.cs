using BepInEx.Logging;
using BunnyLibs;
using HarmonyLib;

namespace CCU.Patches.Inventory
{
	[HarmonyPatch(typeof(InvItem))]
	public static class P_InvItem
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(InvItem.SetupDetails))]
		public static void SetupDetails_Postfix(InvItem __instance)
		{
			__instance.nonStackableInShop = true;
		}
	}
}
