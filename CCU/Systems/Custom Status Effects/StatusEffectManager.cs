using BepInEx.Logging;
using CCU.Systems.CustomGoals;
using HarmonyLib;

namespace CCU.Status_Effects
{
	[HarmonyPatch(typeof(StatusEffects))]
	public class P_StatusEffects_SEManager
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(StatusEffects.IsAntidoteEffect))]
		public static bool AllowAntidote(StatusEffects __instance, string statusEffectName, ref bool __result)
		{
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