using BepInEx.Logging;
using CCU.Localization;
using HarmonyLib;
using System.Collections.Generic;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(GameController))]
	public static class P_GameController
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

        [HarmonyPostfix, HarmonyPatch(methodName: "Awake")]
        public static void Awake_Postfix()
        {
            List<string> removals = new List<string>();

            foreach (string challenge in GC.sessionDataBig.challenges)
                if (Legacy.ChallengeConversions.ContainsKey(challenge))
                    removals.Add(challenge);

            foreach (string removal in removals)
            {
                GC.sessionDataBig.challenges.Remove(removal);
                string replacement = Legacy.ChallengeConversions[removal].Name;
                GC.sessionDataBig.challenges.Add(replacement);
            }
        }
    }
}