using BepInEx.Logging;
using HarmonyLib;

namespace CCU.Patches.Objects
{
    [HarmonyPatch(declaringType: typeof(SpawnerObject))]
    public static class P_SpawnerObject
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        //[HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerObject.spawn))]
        public static bool Spawn_Prefix(string objectRealName)
        {
            logger.LogDebug("Spawn_Prefix");

            if (objectRealName == "CustomFloorDecal")
                logger.LogDebug("Caught");

            return true;
        }
    }
}
