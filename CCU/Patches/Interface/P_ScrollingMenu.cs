using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;
using HarmonyLib;
using BepInEx.Logging;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType: typeof(ScrollingMenu))]
	class P_ScrollingMenu
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static bool canHaveTrait_Prefix(Unlock myUnlock, ref bool ___result)
		{
			if (GC.challenges.Contains(CMutators.HomesicknessDisabled) && myUnlock.unlockName == VanillaTraits.HomesicknessKiller)
				___result = false;
			
			return false;
		}
	}
}
