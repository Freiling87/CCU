using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using CCU.Traits.AI.Behavior;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType:typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName:nameof(StatusEffects.ExplodeAfterDeathChecks))]
		public static void ExplodeAfterDeathChecks_Postfix(StatusEffects __instance, ref bool ___result)
		{
			if (__instance.agent.HasTrait<Behavior_ExplodeOnDeath>())
				___result = true;
		}
	}
}
