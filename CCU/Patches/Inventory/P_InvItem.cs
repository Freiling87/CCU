using BepInEx.Logging;
using HarmonyLib;

namespace CCU.Patches.Inventory
{
    [HarmonyPatch(declaringType:typeof(InvItem))]
    public static class P_InvItem
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(InvItem.SetupDetails))]
        public static void SetupDetails_Postfix(InvItem __instance)
        {
            __instance.nonStackableInShop = true;
        }
    }
}
