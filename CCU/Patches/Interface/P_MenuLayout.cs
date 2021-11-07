using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;
using CCU.Mutators;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(MenuLayout))]
	public static class P_MenuLayout
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(MenuLayout.DoLayout))]
		public static bool DoLayout_Prefix(MenuLayout __instance, ref GUIStyle ___style)
		{
			if (MutatorManager.IsMutatorFromListActive(MutatorManager.FontSizeMutators))
			{
				if (GC.challenges.Contains(CMutators.ScrollingButtonHeight50))
				{
					___style.fontSize = 8;
				}
				else if (GC.challenges.Contains(CMutators.ScrollingButtonHeight75))
				{
					___style.fontSize = 12;
				}
			}

			return true;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(MenuLayout.DoLayout))]
		public static void DoLayout_Postfix(MenuLayout __instance, ref int ___height, ref GUIStyle ___style, ref GUIStyle ___styleSelected, ref int ___ySpace)
		{
			Core.LogMethodCall();

			if (GC.challenges.Contains(CMutators.ScrollingButtonTextAlignLeft))
			{
				___style.alignment = TextAnchor.MiddleLeft;
				___styleSelected.alignment = TextAnchor.MiddleLeft;
			}
		}
	}
}
