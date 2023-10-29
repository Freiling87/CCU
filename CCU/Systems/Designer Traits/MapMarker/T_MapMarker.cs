using BepInEx.Logging;
using BTHarmonyUtils.TranspilerUtils;
using BunnyLibs;
using CCU.Traits.Cost_Scale;
using CCU.Traits.Map_Marker;
using HarmonyLib;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace CCU.Traits.Map_Marker
{
	public abstract class T_MapMarker : T_CCU
	{
		public T_MapMarker() : base() { }
	}

	[HarmonyPatch(typeof(PlayfieldObject))]
	class P_PlayfieldObject
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// [HarmonyPrefix, HarmonyPatch(nameof(PlayfieldObject.SpawnNewMapMarker))]
		public static void SpawnNewMapMarker_Prefix(PlayfieldObject __instance)
		{
			if (__instance.CompareTag("Agent"))
			{
				Agent agent = (Agent)__instance;

				if (agent.GetTraits<T_MapMarker>().Any())
					__instance.MinimapDisplay();
			}
		}
	}
}