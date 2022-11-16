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
			__instance.agent.weaponCooldown = T_RateOfFire.WeaponCooldown(__instance.agent, __instance.agent.weaponCooldown);
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