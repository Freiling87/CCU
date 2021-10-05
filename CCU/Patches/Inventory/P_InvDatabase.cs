using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
using HarmonyLib;
using CCU.Traits;
using CCU.Traits.AI.Vendor;
using System.Reflection;
using Random = UnityEngine.Random;

namespace CCU.Patches.Inventory
{
	[HarmonyPatch(declaringType:typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.AddRandItem), argumentTypes: new[] { typeof(string) })]
		public static bool AddRandItem_Prefix(string itemNum, InvDatabase __instance, ref InvItem __result)
		{
			if (__instance.CompareTag("Agent") && TraitManager.HasTraitFromList(__instance.agent, TraitManager.VendorTypes))
			{
				string rName = TraitManager.GetOnlyTraitFromList(__instance.agent, TraitManager.VendorTypes).Name;
				string text = "";

				int num = 0;
				bool flag = false;

				do
				{
					try
					{
						text = __instance.rnd.RandomSelect(rName, "Items");
						text = __instance.SwapWeaponTypes(text);

						if (text != "")
							flag = true;
					}
					catch
					{
						text = "Empty";
					}

					foreach (InvItem invItem in __instance.InvItemList)
						if (invItem.invItemName == text && !invItem.canRepeatInShop)
							flag = false;

					if (text == "FreeItemVoucher")
						flag = false;

					num++;
				}
				while (!flag && num < 100);

				if (num == 100)
					text = "Empty";

				if (text != "Empty" && text != "")
				{
					// return __instance.AddItemReal(text);

					MethodInfo addItemReal = AccessTools.DeclaredMethod(typeof(InvDatabase), "AddItemReal", new Type[1] { typeof(string) });
					InvItem newItem = addItemReal.GetMethodWithoutOverrides<Action<string>>(__instance).Invoke(text);
				}

				return false;
			}

			return true;
		}
	}
}
