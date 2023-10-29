using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Traits.Player.Ranged_Combat
{
	//	TODO: Move to BunnyLibs
	public abstract class T_RateOfFire : T_PlayerTrait
	{
		public T_RateOfFire() : base() { }

		public abstract float CooldownMultiplier { get; }
	}

	[HarmonyPatch(typeof(Combat))]
	public static class P_Combat_RateOfFire
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(Combat.DoRapidFire))]
		private static void DoRapidFire_TriggerHappy(Combat __instance, ref Agent ___agent)
		{
			foreach (T_RateOfFire trait in ___agent.GetTraits<T_RateOfFire>())
				__instance.rapidFireTime /= trait.CooldownMultiplier;
		}

		// I believe this is only called for Rapid Fire ranged attacks
		[HarmonyPostfix, HarmonyPatch(nameof(Combat.SetPersonalCooldown))]
		private static void SetPersonalCooldown_TriggerHappy(Combat __instance, ref Agent ___agent)
		{
			foreach (T_RateOfFire trait in ___agent.GetTraits<T_RateOfFire>())
				__instance.personalCooldown *= trait.CooldownMultiplier;
		}
	}

	[HarmonyPatch(typeof(Gun))]
	public static class P_Gun_RateOfFire
	{
		[HarmonyPostfix, HarmonyPatch(nameof(Gun.SetWeaponCooldown))]
		public static void SetWeaponCooldown_Postfix(Gun __instance)
		{
			foreach (T_RateOfFire trait in __instance.agent.GetTraits<T_RateOfFire>())
				__instance.agent.weaponCooldown *= trait.CooldownMultiplier;
		}
	}
}