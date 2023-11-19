using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;

namespace CCU.Systems.Appearance.Fixes
{

	[HarmonyPatch(typeof(AgentHitbox))]
	internal class P_AgentHitbox_Appearance
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(AgentHitbox.SetupBodyStrings))]
		private static bool CopyBody(AgentHitbox __instance)
		{
			//	Prevents Zombification from copying the same body to each copy of each NPC class.
			
			try
			{
				if (__instance.agent.isPlayer == 0
					&& __instance.agent.GetOrAddHook<H_Appearance>().bodyType.Length > 1)
					__instance.agent.customCharacterData.bodyType = __instance.agent.GetOrAddHook<H_Appearance>().bodyType;
			}
			catch { }	//	Can cause NRE with player spawn
			return true;
		}
	}
}