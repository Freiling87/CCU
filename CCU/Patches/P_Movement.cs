using BepInEx.Logging;
using CCU.Traits.Passive;
using CCU.Traits.Player.Melee_Combat;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(Movement))]
    public static class P_Movement
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Movement.FindKnockBackStrength))]
        public static bool FindKnockBackStrength(Movement __instance, PlayfieldObject ___playfieldObject, Agent ___agent, ref float __result)
        {
            if (___agent is null)
                return true;

            if (___agent.HasTrait<Immovable>())
            {
                __result = 0f;
                return false;
            }

            if (___playfieldObject.knockedByObject.playfieldObjectAgent?.HasTrait<Knockback_Peon>() ?? false)
            {
                __result /= 1.1f;
                return false;
            }

            return true;
        }
    }
}
