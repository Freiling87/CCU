using BepInEx.Logging;
using CCU.Localization;
using CCU.Systems.Investigateables;
using CCU.Traits;
using HarmonyLib;
using System;

namespace CCU.Patches
{
    [HarmonyPatch(declaringType: typeof(NameDB))]
	public static class P_NameDB
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(NameDB.GetName))]
		public static bool GetName_Prefix(ref string myName, string type, ref string __result)
		{
			if (type == "StatusEffect" && Legacy.TraitConversions.ContainsKey(myName))
				myName = T_CCU.DesignerName(Legacy.TraitConversions[myName]);

			if (type == "Item")
				if (Investigateables.IsInvestigationString(myName))
				{
					__result = myName;
					return false;
				}

			return true;
        }

        [HarmonyPostfix, HarmonyPatch(methodName: nameof(NameDB.GetName))]
		public static void GetName_Postfix(string myName, string type, ref string __result)
        {
			// TODO: Make this iterate with a while loop to be able to rename multiple generations of releases.
			// Might also need to be extended past traits, but you have more time for that.
			if (Core.debugMode ||
				type != "StatusEffect" || !__result.Contains("E_"))
				return;

			foreach (Type trait in Legacy.TraitConversions.Values)
            {
				string traitName = T_CCU.DesignerName(trait);

				if (__result == "E_" + traitName)
					__result = __result.Remove(0, 2);
			}
		}

		// TODO: Test Note Drop bug and see if the commented part fixes it
		public static bool IsActualItem(InvItem invItem) =>
			!invItem.invItemName?.Contains("E_") ?? false;
			
	}
}