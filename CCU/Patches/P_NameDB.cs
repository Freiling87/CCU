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

		// Disabling these TEMPORARILY while implementing replacements.
		// Hopefully we can replace these fixes while we're at it.

		/// <summary>
		/// I think this was meant to bypass the postfix, showing the new replaced traits in the list instead? Good god
		/// </summary>
		/// <param name="myName"></param>
		/// <param name="type"></param>
		/// <param name="__result"></param>
		/// <returns></returns>
		//[HarmonyPrefix, HarmonyPatch(methodName: nameof(NameDB.GetName))]
		public static bool GetName_Prefix(ref string myName, string type, ref string __result)
		{
			//if (type == "StatusEffect" && Legacy.TraitConversions.ContainsKey(myName))
			//	myName = T_CCU.DesignerName(Legacy.TraitConversions[myName]);

			if (type == "Item")
				if (Investigateables.IsInvestigationString(myName))
				{
					__result = myName;
					return false;
				}

			return true;
        }

		/// <summary>
		/// This is a hacky way of displaying outdated traits correctly in character selection. 
		/// Since they're no longer in the assembly, they'll show as E_TraitName in the list.
		/// Once they open the character in the editor, the traits are replaced as described in the Legacy Trait Updater.
		/// </summary>
		/// <param name="myName"></param>
		/// <param name="type"></param>
		/// <param name="__result"></param>
        [HarmonyPostfix, HarmonyPatch(methodName: nameof(NameDB.GetName))]
		public static void GetName_Postfix(string myName, string type, ref string __result)
        {
			// TODO: Make this iterate with a while loop to be able to rename multiple generations of releases.
			if (type != "StatusEffect" || !__result.Contains("E_"))
				return;

			foreach (Type[] traitOutput in Legacy.TraitConversions.Values)
            {
				foreach (Type trait in traitOutput)
				{
					string traitName = T_CCU.DesignerName(trait);

					if (__result == "E_" + traitName)
                    {
						__result = __result.Remove(0, 2);
						return;
					}
				}
			}
		}

		// TODO: Test Note Drop bug and see if the commented part fixes it
		public static bool IsActualItem(InvItem invItem) =>
			!invItem.invItemName?.Contains("E_") ?? false;
			
	}
}