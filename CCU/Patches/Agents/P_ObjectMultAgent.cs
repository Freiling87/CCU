using BepInEx.Logging;
using BunnyLibs;

using CCU.Traits.Hire_Duration;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches.Agents
{
	[HarmonyPatch(typeof(ObjectMultAgent))]
	public static class P_ObjectMultAgent
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(nameof(ObjectMultAgent.cantDoMoreTasks), MethodType.Setter)]
		public static bool cantDoMoreTasks_Setter_Prefix(ref bool value, ObjectMultAgent __instance)
		{
			if (__instance.agent.HasTrait<Permanent_Hire>() || __instance.agent.HasTrait<Permanent_Hire_Only>())
				value = false;

			return true;
		}

		[HarmonyPostfix, HarmonyPatch(nameof(ObjectMultAgent.eyesType), methodType: MethodType.Getter)]
		private static void EyesType_Getter_Prefix(ObjectMultAgent __instance, ref int __result)
		{
			if (!(__instance.agent.GetOrAddHook<H_Appearance>().eyesType is null))
				__result = __instance.convertEyesTypeToInt(__instance.agent.GetOrAddHook<H_Appearance>().eyesType);
		}
	}
}
