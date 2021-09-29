using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using RogueLibsCore;
using CCU.Traits.AI;
using Random = UnityEngine.Random;
using System.Reflection;
using CCU.Traits;


namespace CCU.Patches.Behaviors
{
	[HarmonyPatch(declaringType: typeof(Agent))]
	public class P_Agent
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName:nameof(Agent.SetupAgentStats), argumentTypes: new[] { typeof(string) })]
		public static void SetupAgentStats_Postfix(string transformationType, Agent __instance)
		{
			if (Behavior.HasTraitFromList(__instance, Behavior.VendorTraits))
				__instance.SetupSpecialInvDatabase();
		}
	}
}
