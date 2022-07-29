using BepInEx.Logging;
using CCU.Localization;
using CCU.Traits;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Patches
{
	[HarmonyPatch(declaringType: typeof(NameDB))]
	public static class P_CharacterCreation
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(NameDB.GetName))]
		public static bool GetName_Prefix(ref string myName, string type)
		{
			if (type == "StatusEffect" && Legacy.TraitConversions.ContainsKey(myName))
				myName = T_CCU.DisplayName(Legacy.TraitConversions[myName]);

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
				string traitName = T_CCU.DisplayName(trait);

				if (__result == "E_" + traitName)
					__result = __result.Remove(0, 2);
			}
		}
	}
}