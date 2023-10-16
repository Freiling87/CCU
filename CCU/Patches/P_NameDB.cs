using BepInEx.Logging;
using BunnyLibs;
using CCU.Localization;
using HarmonyLib;
using System;

namespace CCU.Patches
{
	[HarmonyPatch(typeof(NameDB))]
	public static class P_NameDB
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		/// <summary>
		/// This is a hacky way of displaying outdated traits correctly in character selection. 
		/// Since they're no longer in the assembly, they'll show as E_TraitName in the list.
		/// Once they open the character in the editor, the traits are replaced as described in the Legacy Trait Updater.
		/// </summary>
		/// <param name="myName"></param>
		/// <param name="type"></param>
		/// <param name="__result"></param>
		[HarmonyPostfix, HarmonyPatch(nameof(NameDB.GetName))]
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

	}
}