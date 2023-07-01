using BepInEx.Logging;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(SpawnerMain))]
    public static class P_SpawnerMain
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerMain.SpawnStatusText), argumentTypes: new Type[] { typeof(PlayfieldObject), typeof(string), typeof(string), typeof(string), typeof(NetworkInstanceId), typeof(string), typeof(string) })]
        public static bool SpawnStatusText_GateAV(PlayfieldObject myPlayfieldObject, string textType)
		{
            if (textType == "Buff"
                && myPlayfieldObject is Agent agent
                && agent.HasTrait<Suppress_Status_Text>())
                return false;

            return true;
		}
    }
}