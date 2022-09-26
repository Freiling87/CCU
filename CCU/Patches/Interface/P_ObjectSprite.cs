using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Patches.Interface
{
    public static class P_ObjectSprite
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(ObjectSprite.MouseOrTargetOver))]
        public static bool MOTO_Prefix(Agent myAgent, ObjectSprite __instance)
        {
            Core.LogMethodCall();
            logger.LogDebug("RealName   : " + myAgent.agentRealName);
            logger.LogDebug("ORRN       : " + __instance.objectReal.objectRealRealName);
            logger.LogDebug("ORRN2      : " + __instance.objectReal.objectRealRealName2);

            return true;
        }
    }
}
