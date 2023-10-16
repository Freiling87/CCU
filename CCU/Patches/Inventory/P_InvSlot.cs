using BepInEx.Logging;
using BunnyLibs;
using HarmonyLib;

namespace CCU.Patches.Inventory
{
	[HarmonyPatch(typeof(InvSlot))]
	public static class P_InvSlot
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(InvSlot.MoveFromChestToInventory))]
		public static void MoveFromChestToInventory_Postfix(InvSlot __instance)
		{
			__instance.item.SetupDetails(true);
		}
	}
}