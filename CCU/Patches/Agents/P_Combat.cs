using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;
using HarmonyLib;
using BTHarmonyUtils;
using BepInEx;
using BepInEx.Logging;

namespace CCU.Patches.Agents
{
	[HarmonyPatch(declaringType:typeof(Combat))]
	public static class P_Combat
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;
	}
}
