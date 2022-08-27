using BepInEx.Logging;
using HarmonyLib;

namespace CCU.Patches.Inventory
{
    [HarmonyPatch(declaringType: typeof(InvSlot))]
    public static class P_InvSlot
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(InvSlot.MoveFromChestToInventory))]
        public static void MoveFromChestToInventory_Postfix(InvSlot __instance)
        {
            __instance.item.SetupDetails(true);
        }
    }
} 