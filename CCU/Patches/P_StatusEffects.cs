using BepInEx.Logging;
using CCU.Traits.Explode_On_Death;
using CCU.Traits.Passive;
using CCU.Traits.Trait_Gate;
using HarmonyLib;
using RogueLibsCore;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType:typeof(StatusEffects))]
	class P_StatusEffects
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.AgentIsRival), argumentTypes: new[] { typeof(Agent) })]
		public static bool AgentIsRival_Prefix(Agent myAgent, StatusEffects __instance, ref bool __result)
        {
			if ((__instance.agent.HasTrait<Bashable>() && (myAgent.HasTrait(VanillaTraits.BlahdBasher) || myAgent.agentName == VanillaAgents.GangsterCrepe)) ||
				(__instance.agent.HasTrait<Cool_Cannibal>() && (myAgent.agentName == VanillaAgents.Soldier)) ||
				(__instance.agent.HasTrait<Crushable>() && (myAgent.HasTrait(VanillaTraits.CrepeCrusher) || myAgent.agentName == VanillaAgents.GangsterBlahd)) ||
				(__instance.agent.HasTrait<Slayable>() && myAgent.HasTrait("HatesScientist")) ||
				(__instance.agent.HasTrait<Specistist>() && myAgent.HasTrait(VanillaTraits.Specist)) )
            {
				__result = true;
				return false;
            }

			return true;
        }

		[HarmonyPostfix, HarmonyPatch(methodName:nameof(StatusEffects.ExplodeAfterDeathChecks))]
		public static void ExplodeAfterDeathChecks_Postfix(StatusEffects __instance)
		{
			if (__instance.agent.GetTraits<T_ExplodeOnDeath>().Any())
			{
				if (!__instance.agent.disappeared)
					__instance.agent.objectSprite.flashingRepeatedly = true;

				// TODO: See note 2205240553
				if (GC.serverPlayer)
					__instance.StartCoroutine("ExplodeBody");
			}
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(StatusEffects.IsInnocent), argumentTypes: new[] { typeof(Agent) })]
		public static bool IsInnocent_Prefix(Agent playerGettingPoints, StatusEffects __instance, ref bool __result)
		{
			if (__instance.agent.HasTrait<Innocent>())
			{
				__result = true;
				return false;
			}

			return true;
        }
	}
}
