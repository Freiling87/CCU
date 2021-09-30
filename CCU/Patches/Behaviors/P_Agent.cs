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
using CCU.Traits.AI.Behavior;

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
			if (Behavior.HasTraitFromList(__instance, Behavior.VendorTypes))
				__instance.SetupSpecialInvDatabase();

			// May want to generalize into LOSCheckTraits, but this might be the only one that's on a coin toss (done in LoadLevel.SetupMore3_3 when spawning roamers)
			if ((__instance.specialAbility == vSpecialAbility.StickyGlove && GC.percentChance(50)) ||
				__instance.specialAbility == vSpecialAbility.Bite ||
				__instance.specialAbility == vSpecialAbility.Cannibalize ||
				__instance.HasTrait<Behavior_GrabMoney>())
				__instance.losCheckAtIntervals = true;
		}
	}
}
