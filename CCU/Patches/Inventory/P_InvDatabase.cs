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
			Core.LogMethodCall();
			string rName = TraitManager.GetOnlyTraitFromList(__instance.agent, TraitManager.VendorTypes).Name;
			logger.LogDebug("\trName: " + rName);

			if (__instance.CompareTag("SpecialInvDatabase") && !(rName is null))
			{
				string text;

				int num = 0;
				bool foundItem = false;

				do
				{
					try
					{
						text = __instance.rnd.RandomSelect(rName, "Items");
						text = __instance.SwapWeaponTypes(text);

						if (text != "")
							foundItem = true;
					}
					catch
					{
						logger.LogDebug("\tCatch");

						text = "Empty";
					}

					logger.LogDebug("\tText: " + text);

					foreach (InvItem invItem in __instance.InvItemList)
						if (invItem.invItemName == text && !invItem.canRepeatInShop)
							foundItem = false;

					if (text == "FreeItemVoucher")
						foundItem = false;

					num++;
				}
				while (!foundItem && num < 100);

				if (num == 100)
					text = "Empty";

				if (text != "Empty" && text != "")
				{
					//return this.AddItemReal(text);

					//MethodInfo addItemReal = AccessTools.DeclaredMethod(typeof(InvDatabase), "AddItemReal", new Type[1] { typeof(string) });
					//__result = addItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(text);

					// Just copying AddItemReal into here in case I'm doing something wrong with AccessTools
					// TODO: Remove this code and just invoke the private method

					InvItem invItem = __instance.AddItem(text, 1);
					int invItemCount;
					
					if (invItem.invItemName == "Money")
						invItemCount = __instance.FindMoneyAmt(false);
					else if (invItem.itemType == "WeaponMelee" && __instance.CompareTag("Agent"))
					{
						if (GC.challenges.Contains("InfiniteMeleeDurability"))
							invItemCount = 100;
						else
							invItemCount = int.Parse(__instance.rnd.RandomSelect("AgentMeleeDurability", "Others"));
					}
					else if (invItem.itemType == "WeaponMelee" && __instance.CompareTag("SpecialInvDatabase") && __instance.agent != null)
						invItemCount = 200;
					else
						invItemCount = invItem.initCount;
					
					invItem.invItemCount = invItemCount;

					__result = invItem;
					return false;
				}

				return false;
			}

			return true;
		}
	}
}
