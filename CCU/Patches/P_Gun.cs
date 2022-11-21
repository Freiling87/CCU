using CCU.Traits.Inventory;
using CCU.Traits.Player.Ranged_Combat;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(Gun))]
	public static class P_Gun
	{
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(Gun.SetWeaponCooldown))]
        public static void SetWeaponCooldown_Postfix(Gun __instance)
        {
            foreach (T_RateOfFire trait in __instance.agent.GetTraits<T_RateOfFire>())
                __instance.agent.weaponCooldown *= trait.CooldownMultiplier;
        }

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Gun.SubtractBullets))]
        public static bool SubtractBullets_Prefix(Gun __instance)
        {
            if (__instance.agent.HasTrait<Infinite_Ammo>())
                return false;

            return true;
        }
	}
}