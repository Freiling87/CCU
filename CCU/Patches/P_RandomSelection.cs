using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(RandomSelection))]
	class P_RandomSelection
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;
	}
}
