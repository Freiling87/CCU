using BepInEx.Logging;
using CCU.Systems.Containers;
using HarmonyLib;
using System;

namespace CCU.Patches.Objects
{
    [HarmonyPatch(declaringType: typeof(ObjectReal))]
    public static class P_ObjectReal
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: "Start", argumentTypes: new Type[0] { })]
        public static bool Start_SetupInvDatabasesForContainers(ObjectReal __instance)
        {
            Core.LogMethodCall();
            logger.LogDebug(__instance.objectName);

            if (Containers.ContainerObjects.Contains(__instance.objectName))
            {
                if (__instance.GetComponent<InvDatabase>() is null)
                    __instance.objectInvDatabase = __instance.go.AddComponent<InvDatabase>();

                logger.LogDebug("Added Inventory");
            }

            return true;
        }
    }
}