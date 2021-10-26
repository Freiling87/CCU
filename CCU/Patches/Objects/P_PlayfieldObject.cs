using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;
using CCU.Traits;
using CCU.Traits.Hire;

namespace CCU.Patches.Objects
{
	[HarmonyPatch(declaringType: typeof(PlayfieldObject))]
	class P_PlayfieldObject
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(PlayfieldObject.determineMoneyCost), argumentTypes: new[] { typeof(int), typeof(string) })]
		public static void determineMoneyCost_Postfix(int moneyAmt, string transactionType, PlayfieldObject __instance, ref int __result)
		{
			if (transactionType == "SoldierHire" || transactionType == "ThiefAssist") // These are the types used for hire traits
			{
				if (__instance.GetComponent<Agent>().HasTrait<Hire_CostLess>())
					__result = (int)((float)__result * 0.5f);
				else if (__instance.GetComponent<Agent>().HasTrait<Hire_CostMore>())
					__result = (int)((float)__result * 1.5f);
			}
			
		}

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(PlayfieldObject.SpawnNewMapMarker))]
		public static void SpawnNewMapMarker_Prefix(PlayfieldObject __instance)
		{
			if (__instance.CompareTag("Agent"))
				if (TraitManager.HasTraitFromList((Agent)__instance, TraitManager.MapMarkerTraits))
					__instance.MinimapDisplay();
		}
	}
}
