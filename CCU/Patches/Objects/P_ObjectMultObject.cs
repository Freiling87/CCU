using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Patches.Objects
{
    [HarmonyPatch(declaringType: typeof(ObjectMultObject))]
    public static class P_ObjectMultObject
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(ObjectMultObject.OnDeserialize))]
        public static void OnDeserialize_Postfix(ObjectMultObject __instance)
        {
            logger.LogDebug("OnDeserialize_Postfix: " + __instance.objectName);

            MethodInfo getName = AccessTools.DeclaredMethod(typeof(ObjectMultObject), "GetName");

            logger.LogDebug("Chest1: " + __instance.chestItem1);
            try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem1, "Item"); }
            catch { __instance.chestItem1 = ""; }

            logger.LogDebug("Chest2: " + __instance.chestItem2);
            try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem2, "Item"); }
            catch { __instance.chestItem2 = ""; }

            logger.LogDebug("Chest3: " + __instance.chestItem3);
            try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem3, "Item"); }
            catch { __instance.chestItem3 = ""; }

        }
    }
}
