using BepInEx.Logging;
using BunnyLibs;
using CCU.Traits.Passive;
using HarmonyLib;
using RogueLibsCore;
using System;
using UnityEngine.Networking;

namespace CCU.Patches
{
	[HarmonyPatch(typeof(SpawnerMain))]
	public static class P_SpawnerMain
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(SpawnerMain.SpawnStatusText), argumentTypes: new[] { typeof(PlayfieldObject), typeof(string), typeof(string), typeof(string), typeof(NetworkInstanceId), typeof(string), typeof(string) })]
		public static bool SpawnStatusText_GateAV(PlayfieldObject myPlayfieldObject, string textType)
		{
			if (textType == "Buff"
				&& myPlayfieldObject is Agent agent
				&& agent.HasTrait<Suppress_Status_Text>())
				return false;

			return true;
		}
	}
}