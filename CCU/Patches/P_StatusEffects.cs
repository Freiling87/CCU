using BepInEx.Logging;
using CCU.Traits.Passive;
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
	}
}
