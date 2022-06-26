using BepInEx.Logging;
using CCU.Localization;
using HarmonyLib;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(Unlocks))]
	class P_Unlocks
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// Legacy Trait Updater
		/// </summary>
		/// <param name="unlockName"></param>
		/// <returns></returns>
        [HarmonyPrefix, HarmonyPatch(methodName: nameof(Unlocks.GetUnlock), new[] { typeof(string), typeof(string) } )]
		public static bool GetUnlock_Prefix(ref string unlockName)
        {
			if (Legacy.TraitConversions.ContainsKey(unlockName))
				unlockName = Legacy.TraitConversions[unlockName].Name;

			return true;
        }
	}
}