using BepInEx.Logging;
using HarmonyLib;
using RogueLibsCore;

namespace CCU.Patches.Interface
{
    [HarmonyPatch(declaringType: typeof(ScrollingMenu))]
	class P_ScrollingMenu
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(ScrollingMenu.CanHaveTrait), argumentTypes: new[] { typeof(Unlock) })]
		public static bool canHaveTrait_Prefix(Unlock myUnlock, ref bool __result)
		{
			if ((GC.challenges.Contains(CMutators.HomesicknessDisabled) || GC.challenges.Contains(CMutators.HomesicknessMandatory)) && myUnlock.unlockName == VanillaTraits.HomesicknessKiller)
			{
				__result = false;
				return false;
			}
			
			return true;
		}
	}
}
