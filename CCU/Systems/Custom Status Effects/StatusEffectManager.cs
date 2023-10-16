using BepInEx.Logging;
using BunnyLibs;
using CCU.Systems.CustomGoals;
using HarmonyLib;

namespace CCU.Status_Effects
{
	public class StatusEffectManager
	{
	}

	[HarmonyPatch(typeof(StatusEffects))]
	public class P_StatusEffects_SEManager
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.IsAntidoteEffect))]
		public static bool AllowAntidote(StatusEffects __instance, string statusEffectName, ref bool __result)
		{
			logger.LogDebug("AllowAntidote: " + statusEffectName);
			if (statusEffectName == CustomGoals.Electrocuted_Permanent
				|| statusEffectName == CustomGoals.Frozen_Fragile
				|| statusEffectName == CustomGoals.Frozen_Permanent)
			{
				__result = true;
				return false;
			}

			return true;
		}
	}
}
