using BepInEx.Logging;
using BunnyLibs;
using CCU.Localization;
using HarmonyLib;
using System.Collections.Generic;

namespace CCU.Patches
{
	[HarmonyPatch(typeof(GameController))]
	public static class P_GameController
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch("Awake")]
		public static void Awake_Postfix()
		{
			List<string> removals = new List<string>();

			foreach (string mutator in GC.sessionDataBig.challenges)
				if (Legacy.MutatorConversions.ContainsKey(mutator))
					removals.Add(mutator);

			foreach (string removal in removals)
			{
				GC.sessionDataBig.challenges.Remove(removal);
				string replacement = Legacy.MutatorConversions[removal].Name;
				GC.sessionDataBig.challenges.Add(replacement);
			}
		}
	}
}