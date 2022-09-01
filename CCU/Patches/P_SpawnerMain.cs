using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(SpawnerMain))]
    public static class P_SpawnerMain
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerMain.spawnObjectReal), 
            argumentTypes: new[] { typeof(Vector3), typeof(PlayfieldObject), typeof(string), typeof(string), typeof(WorldDataObject), typeof(int) })]
        public static bool Spawn_Prefix(string objectType)
        {
            logger.LogDebug("Spawn_Prefix");

            if (objectType == "CustomFloorDecal")
                logger.LogDebug("Caught");

            return true;
        }
    }
}
