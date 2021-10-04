using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using CCU.Traits;
using HarmonyLib;

namespace CCU.Patches.Inventory
{
	[HarmonyPatch(declaringType: typeof(CharacterCreation))]
	public static class P_CharacterCreation
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		// Postfix RemoveFromList & AddToList to increase/decrease CharacterCreation.traitLimit

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(CharacterCreation.AddToList), argumentTypes: new[] { typeof(string), typeof(string) })]
		public static void AddToList_Postfix(string listType, string unlockName, CharacterCreation __instance)
		{
			Core.LogMethodCall();	
			logger.LogDebug("\tlistType: " + listType + "\n\tunlockName: " + unlockName);

			if (listType == "Traits" && TraitManager.HiddenTraitNames.Contains(unlockName))
				__instance.traitLimit += 1;
		}

		[HarmonyPostfix, HarmonyPatch(methodName: nameof(CharacterCreation.AddToList), argumentTypes: new[] { typeof(string), typeof(string) })]
		public static void RemoveFromList(string listType, string unlockName, CharacterCreation __instance)
		{
			Core.LogMethodCall();
			logger.LogDebug("\tlistType: " + listType + "\n\tunlockName: " + unlockName);

			if (listType == "Traits" && TraitManager.HiddenTraitNames.Contains(unlockName))
				__instance.traitLimit -= 1;
		}
	}
}
