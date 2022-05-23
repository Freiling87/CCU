using BepInEx.Logging;
using CCU.Traits.Passive;
using CCU.Traits.TraitGate;
using HarmonyLib;
using RogueLibsCore;

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
			if (myAgent.HasTrait<Bashable>() && __instance.agent.HasTrait(VanillaTraits.BlahdBasher) ||
				myAgent.HasTrait<Crushable>() && __instance.agent.HasTrait(VanillaTraits.CrepeCrusher) ||
				myAgent.HasTrait<Slayable>() && __instance.agent.HasTrait("HatesScientist") ||
				myAgent.HasTrait<Specistist>() && __instance.agent.HasTrait(VanillaTraits.Specist))
            {
				__result = true;
				return false;
            }

			return true;
        }

		[HarmonyPostfix, HarmonyPatch(methodName:nameof(StatusEffects.ExplodeAfterDeathChecks))]
		public static void ExplodeAfterDeathChecks_Postfix(StatusEffects __instance)
		{
			if (__instance.agent.HasTrait<ExplodeOnDeath>())
			{
				if (!__instance.agent.disappeared)
					__instance.agent.objectSprite.flashingRepeatedly = true;

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
