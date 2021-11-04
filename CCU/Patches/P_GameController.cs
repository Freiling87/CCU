using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine.UI;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(GameController))]
	public static class P_GameController
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: "Awake3")]
		public static bool Awake3_Prefix(GameController __instance)
		{
			if (GC.challenges.Contains(CMutators.NoObjectGlow))
			{
				GC.objectGlowDisabled = true;
				GC.sessionDataBig.objectGlowDisabled = true;
			}

			return true;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(GameController.SetFont), argumentTypes: new[] { typeof(Text) })]
		public static void SetFont_Postfix(Text myText)
		{
			Core.LogMethodCall();
			// TEST
			myText.color = new UnityEngine.Color(255f, 0f, 0f);

			logger.LogDebug(myText.font.name);
		}
	}
}
