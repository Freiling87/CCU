using BepInEx.Logging;
using CCU.Traits.Player.Melee_Combat;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(Melee))]
	public static class P_Melee
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPostfix,HarmonyPatch(methodName: nameof(Melee.Attack), argumentTypes: new[] { typeof(bool) })]		
        public static void Attack_Postfix(Melee __instance)
        {
            foreach (T_MeleeSpeed trait in __instance.agent.GetTraits<T_MeleeSpeed>())
                __instance.meleeContainerAnim.speed *= trait.SpeedMultiplier;
        }

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Melee.SetWeaponCooldown))]
        public static void SetWeaponCooldown_Postfix(Melee __instance)
        {
            Agent agent = __instance.agent;

            foreach (T_MeleeSpeed trait in agent.GetTraits<T_MeleeSpeed>())
                agent.weaponCooldown /= trait.SpeedMultiplier;
        }
    }
}