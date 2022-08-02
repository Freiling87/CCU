using BepInEx.Logging;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(Movement))]
    class P_Movement
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Movement.FindKnockBackStrength))]
        public static bool FindKnockBackStrength(Agent ___agent, ref float __result)
        {
            if (___agent?.HasTrait<Immovable>() ?? false)
            {
                __result = 0;
                return false;
            }

            return true;
        }
    }
}
