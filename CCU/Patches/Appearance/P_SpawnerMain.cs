using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueLibsCore;
using HarmonyLib;
using BTHarmonyUtils;
using BepInEx.Logging;
using UnityEngine;
using Object = UnityEngine.Object;
using CCU.Mutators;

namespace CCU.Patches.Appearance
{
	[HarmonyPatch(declaringType: typeof(SpawnerMain))]
	public static class P_SpawnerMain
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(SpawnerMain.SetLighting2), argumentTypes: new[] { typeof(PlayfieldObject) })]
		public static bool SetLighting2_Prefix(PlayfieldObject myObject, SpawnerMain __instance)
		{
			Core.LogMethodCall();

			if ((GC.challenges.Contains(CMutators.NoAgentLights) && myObject.CompareTag("Agent")) ||
				(GC.challenges.Contains(CMutators.NoItemLights) && (myObject.CompareTag("Item") || myObject.CompareTag("Wreckage"))) ||
				(GC.challenges.Contains(CMutators.NoObjectLights) && myObject.CompareTag("Object")))
				return false;

			return true;
		}
	}
}
