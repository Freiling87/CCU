using BepInEx.Logging;
using HarmonyLib;
using System;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(BrainUpdate))]
    public class P_BrainUpdate
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(BrainUpdate.MyUpdate), argumentTypes: new Type[0] { })]
        public static bool MyUpdate_Prefix(BrainUpdate __instance)
        {
            // This has the wandering NPC code like pickpocketing, Hobo grabbing, etc.

            return true;
        }
    }
}
