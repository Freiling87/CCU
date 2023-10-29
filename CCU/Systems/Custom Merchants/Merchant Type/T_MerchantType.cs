using BepInEx.Logging;
using BunnyLibs;
using CCU.Traits.Merchant_Stock;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCU.Traits.Merchant_Type
{
	public abstract class T_MerchantType : T_CCU, ISetupAgentStats
	{
		public T_MerchantType() : base() { }

		public string DisplayName => DesignerName(GetType());

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.SetupSpecialInvDatabase();
		}

		public abstract List<KeyValuePair<string, int>> MerchantInventory { get; }
	}

	[HarmonyPatch(typeof(InvDatabase))]
	public static class P_InvDatabase
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		public static FieldInfo nameProviderField = AccessTools.Field(typeof(RogueLibs), "NameProvider");
		public static CustomNameProvider nameProvider = (CustomNameProvider)nameProviderField.GetValue(null);

		[HarmonyPrefix, HarmonyPatch(nameof(InvDatabase.AddRandItem), new[] { typeof(string) })]
		public static bool AddRandItem_Prefix(string itemNum, InvDatabase __instance, ref InvItem __result)
		{
			if (__instance.agent is null)
				return true;

			if (__instance.agent.GetTraits<T_MerchantType>().Any())
			{
				T_MerchantType trait = __instance.agent.GetTrait<T_MerchantType>();
				string rName = trait.DisplayName;

				if (__instance.CompareTag("SpecialInvDatabase") && !(rName is null))
				{
					string itemName;
					int num = 0;
					bool foundItem = false;

					do
					{
						try
						{
							itemName = __instance.rnd.RandomSelect(rName, "Items");
							itemName = __instance.SwapWeaponTypes(itemName);

							if (itemName != "")
								foundItem = true;
						}
						catch
						{
							itemName = "Empty";
						}

						foreach (InvItem invItem in __instance.InvItemList)
							if (invItem.invItemName == itemName && !invItem.canRepeatInShop)
								foundItem = false;

						if (itemName == "FreeItemVoucher")
							foundItem = false;

						num++;
					}
					while (!foundItem && num < 100);

					if (num == 100)
						itemName = "Empty";

					if (itemName != "Empty" && itemName != "")
					{
						MethodInfo addItemReal = AccessTools.DeclaredMethod(typeof(InvDatabase), "AddItemReal", new Type[1] { typeof(string) });
						__result = addItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(itemName);

						return false;
					}

					return false;
				}
			}

			return true;
		}

		[HarmonyPrefix, HarmonyPatch(nameof(InvDatabase.FillSpecialInv))]
		public static bool FillSpecialInv_Prefix(InvDatabase __instance)
		{
			// WARNING: // This was accidentally deleted at some point (Commit d02c7cde, 06/30/2023). It might have been for a good reason.
			Agent agent = __instance.agent;
			List<string> potentialItems = new List<string>();
			List<string> rolledItems = new List<string>();

			if (agent is null || agent.agentName != VanillaAgents.CustomCharacter || __instance.filledSpecialInv)
				return true;

			List<T_MerchantType> traits = agent.GetTraits<T_MerchantType>().ToList();

			foreach (T_MerchantType trait in traits)
				foreach (KeyValuePair<string, int> item in trait.MerchantInventory)
				{
					// Gives priority to Insider traits
					if (trait.MerchantInventory.Count == 1)
						rolledItems.Add(trait.MerchantInventory[0].Key);
					else
					{
						for (int i = 0; i < item.Value; i++) // Qty
							potentialItems.Add(__instance.SwapWeaponTypes(item.Key)); // Name
					}
				}

			int attempts = 0;
			bool forceDuplicates = false;

		redo: // Yeah why don't you "redo" this whole damn thing or more like "undo" or like "redon't" or or or
			while (potentialItems.Any() && rolledItems.Count < 5 && attempts < 100)
			{
				attempts++;

				int bagPickedIndex = CoreTools.random.Next(0, Math.Max(0, potentialItems.Count - 1));
				string bagPickedItem = potentialItems[bagPickedIndex];

				if (forceDuplicates || !rolledItems.Contains(bagPickedItem) || agent.HasTrait<Clearancer>())
				{
					rolledItems.Add(bagPickedItem);
					potentialItems.RemoveAt(bagPickedIndex);
					attempts = 0;
				}
			}

			if (potentialItems.Any() && rolledItems.Count < 5)
			{
				forceDuplicates = true;
				attempts = 0;
				goto redo;
			}

			foreach (string item in rolledItems)
			{
				MethodInfo AddItemReal = AccessTools.DeclaredMethod(typeof(InvDatabase), "AddItemReal");
				InvItem invItem = null;

				try
				{
					string rListItem = __instance.rnd.RandomSelect(item, "Items");
					invItem = AddItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(rListItem);

					if (agent.HasTrait<Clearancer>())
						invItem.canRepeatInShop = true;
				}
				catch
				{
					invItem = AddItemReal.GetMethodWithoutOverrides<Func<string, InvItem>>(__instance).Invoke(item);

					if (agent.HasTrait<Clearancer>())
						invItem.canRepeatInShop = true;
				}

				if (T_MerchantStock.ExceptionItems.Contains(invItem.invItemName))
					invItem.invItemCount = 0;

				// This is apparently done automatically for Durability items
				if (T_MerchantStock.QuantityTypes.Contains(invItem.itemType))
					T_MerchantStock.ShopPrice(ref invItem);
			}

			__instance.filledSpecialInv = true;
			return false;
		}
	}

	[HarmonyPatch(typeof(InvItem))]
	public static class P_InvItem
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPostfix, HarmonyPatch(nameof(InvItem.SetupDetails))]
		public static void SetupDetails_Postfix(InvItem __instance)
		{
			__instance.nonStackableInShop = true;
		}
	}
}