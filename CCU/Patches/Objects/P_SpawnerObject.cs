using BepInEx.Logging;
using HarmonyLib;

namespace CCU.Patches.Objects
{
    [HarmonyPatch(declaringType: typeof(SpawnerObject))]
    public static class P_SpawnerObject
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

    }
}
