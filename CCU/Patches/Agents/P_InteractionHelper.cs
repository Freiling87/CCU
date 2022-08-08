using BepInEx.Logging;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches.Agents
{
    [HarmonyPatch(declaringType: typeof(InteractionHelper))]
    public static class P_InteractionHelper
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(InteractionHelper.CanInteractWithAgent))]
        public static bool CanInteractWithAgent_Prefix(Agent otherAgent, ref bool __result)
        {
            __result = !otherAgent.HasTrait<Brainless>();

            return __result;
        }
    }
}