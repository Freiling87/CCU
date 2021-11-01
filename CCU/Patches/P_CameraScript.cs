using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RogueLibsCore;
using BepInEx.Logging;
using CCU.Mutators;
using UnityEditor.Experimental;
using UnityEngine;
using RenderSettings = UnityEngine.RenderSettings;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(P_CameraScript))]
	public static class P_CameraScript
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(CameraScript.SetLighting))]
		public static void SetLighting_Postfix(CameraScript __instance)
		{
			if (MutatorManager.IsMutatorFromListActive(MutatorManager.AmbientLightMutators))
			{
				Type mutator = MutatorManager.ActiveMutatorFromList(MutatorManager.AmbientLightMutators); 
				RenderSettings.ambientLight = MutatorManager.AmbientLightColors[mutator];
			}
		}
	}
}
