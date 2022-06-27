using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Patches.Goals
{
    [HarmonyPatch(declaringType: typeof(GoalDoJob))]
    public static class P_GoalDoJob
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;


    }
}
