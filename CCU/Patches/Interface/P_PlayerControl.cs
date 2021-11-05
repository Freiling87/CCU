using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using BepInEx.Logging;
using RogueLibsCore;
using UnityEngine;

namespace CCU.Patches.Interface
{
	[HarmonyPatch(declaringType:typeof(PlayerControl))]
	public static class P_PlayerControl
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch("Update")]
		public static bool Update_Prefix(PlayerControl __instance)
		{
			if (GC.loadCompleteReally && GC.wasLevelEditing && !GC.loadLevel.restartingGame && Input.GetKeyDown(KeyCode.F11))
				GC.levelEditor.ReturnToLevelEditor();

			return true;
		}
	}
}
