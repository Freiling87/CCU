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
            MethodInfo getName = AccessTools.DeclaredMethod(typeof(ObjectMultObject), "GetName");

            try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem1, "Item"); }
            catch { __instance.chestItem1 = ""; }

            try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem2, "Item"); }
            catch { __instance.chestItem2 = ""; }

            try { getName.GetMethodWithoutOverrides<Action<string, string>>(__instance).Invoke(__instance.chestItem3, "Item"); }
            catch { __instance.chestItem3 = ""; }
        }
    }
}
