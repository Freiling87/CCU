using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;

namespace CCU.Patches.Agents
{
	[HarmonyPatch(declaringType:typeof(PathfindingAI))]
	public static class P_PathfindingAI
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;
	}
}
