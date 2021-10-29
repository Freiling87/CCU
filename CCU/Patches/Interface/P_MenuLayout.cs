using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(MenuLayout))]
	public static class P_MenuLayout
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(MenuLayout.DoLayout))]
		public static void DoLayout_Postfix(MenuLayout __instance, ref int ___height, ref GUIStyle ___style, ref GUIStyle ___styleSelected, ref int ___yspace)
		{
			if (GC.challenges.Contains(CMutators.ScrollingButtonTextAlignLeft))
			{
				___style.alignment = TextAnchor.MiddleLeft;
				___styleSelected.alignment = TextAnchor.MiddleLeft;

			}

			if (GC.challenges.Contains(CMutators.ScrollingButtonHeight50))
			{
				___height /= 2;
				___yspace /= 2;
			}
			else if (GC.challenges.Contains(CMutators.ScrollingButtonHeight75))
			{
				___height = (int)(___height * .75f);
				___yspace = (int)(___yspace * .75f);
			}
		}
	}
}
