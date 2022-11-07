using BepInEx.Logging;
using CCU.Traits.Hire_Duration;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches.Agents
{
    [HarmonyPatch(declaringType: typeof(ObjectMultAgent))]
    public static class P_ObjectMultAgent
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(ObjectMultAgent.cantDoMoreTasks), MethodType.Setter)]
        public static bool cantDoMoreTasks_Setter_Prefix(ref bool value, ObjectMultAgent __instance)
        {
            if (__instance.agent.HasTrait<Permanent_Hire>() || __instance.agent.HasTrait<Permanent_Hire_Only>())
                value = false;

            return true;
        }

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(ObjectMultAgent.eyesType), methodType: MethodType.Getter)]
        private static void EyesType_Getter_Prefix(ObjectMultAgent __instance, ref int __result)
        {
            if (!(__instance.agent.GetOrAddHook<P_Agent_Hook>().eyesType is null))
                __result = __instance.convertEyesTypeToInt(__instance.agent.GetOrAddHook<P_Agent_Hook>().eyesType);
        }
    }
}
