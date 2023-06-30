using BepInEx.Logging;
using CCU.Hooks;
using CCU.Localization;
using CCU.Traits.Merchant_Type;
using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Traits.Merchant_Stock
{
    public abstract class T_MerchantStock : T_CCU
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;
     
        public T_MerchantStock() : base() { }

        internal abstract void OnAddItem(ref InvItem invItem);
        internal static List<string> ExceptionItems = new List<string>()
        {
            vItem.Taser,
            vItem.MiniFridge,
            vItem.WaterPistol,
        };
        internal static List<string> DurabilityTypes = new List<string>()
        {
            VItemType.WeaponMelee,
            VItemType.WeaponProjectile,
            VItemType.Wearable,
        };
        internal static List<string> ManualPriceCalcTypes = new List<string>()
        {
            VItemType.Combine,
            VItemType.Consumable,
            VItemType.Food,
            VItemType.Tool,
            VItemType.WeaponMelee,
            //VItemType.WeaponProjectile,
            VItemType.WeaponThrown,
            //VItemType.Wearable,
        };
        internal static List<string> QuantityTypes = new List<string>()
        {
            VItemType.Combine,
            VItemType.Consumable,
            VItemType.Food,
            VItemType.Tool,
            VItemType.WeaponThrown
        };
    }

    [HarmonyPatch(declaringType: typeof(InvDatabase))]
    internal static class P_InvDatabase
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        [HarmonyPrefix, HarmonyPatch(methodName: nameof(InvDatabase.FillSpecialInv))]
        internal static bool FillSpecialInv_Prefix(InvDatabase __instance)
        {
            Agent merchant = __instance.agent;
            List<string> itemPool = new List<string>();
            List<string> shopInventory = new List<string>();

            if (merchant is null || merchant.agentName != VanillaAgents.CustomCharacter || __instance.filledSpecialInv)
                return true;

            List<T_MerchantType> traits = merchant.GetTraits<T_MerchantType>().ToList();

            foreach (T_MerchantType trait in traits)
                foreach (KeyValuePair<string, int> item in trait.weightedItemPool)
                {
                    // For Insider (Key), Insider (Safe Combo), etc.; Bypasses random selection entirely
                    if (trait.weightedItemPool.Count == 1
                        && trait.weightedItemPool[0].Value == 1)
                        shopInventory.Add(trait.weightedItemPool[0].Key);
                    else
                        for (int i = 0; i < item.Value; i++) // Value = Qty
                            itemPool.Add(__instance.SwapWeaponTypes(item.Key)); // Key = Name
                }

            int attempts = 0;
            bool allowDuplicates = merchant.HasTrait<Clearancer>();

            while (itemPool.Any() && shopInventory.Count < 5 && attempts < 100)
            {
                attempts++;

                string pickedItem = CoreTools.GetRandomMember(itemPool);
                itemPool.Remove(pickedItem);

                if (shopInventory.Contains(pickedItem) && !allowDuplicates)
                    continue;

                shopInventory.Add(pickedItem);
            }

            foreach (string itemName in shopInventory)
            {
                InvItem invItem;

                try
                {
                    // Applies random-selection item characteristics, such as for Syringe.
                    // See LoadLevel.loadStuff2, RandomSelection.randomListTable["SyringeContents"]
                    string rListItem = __instance.rnd.RandomSelect(itemName, "Items");
                    invItem = AddItemReal_Custom(__instance, rListItem);
                }
                catch
                {
                    invItem = AddItemReal_Custom(__instance, itemName);
                }

                foreach (T_MerchantStock trait in merchant.GetTraits<T_MerchantStock>())                // Review these
                    trait.OnAddItem(ref invItem);

                if (T_MerchantStock.ExceptionItems.Contains(invItem.invItemName))
                    invItem.invItemCount = 1;
                else if (T_MerchantStock.ManualPriceCalcTypes.Contains(invItem.itemType) && !invItem.hasCharges) // These items automatically adjust price for Qty
                {
                    invItem.itemValue =
                        (int)(invItem.GetOrAddHook<H_InvItem>().vanillaItemValue *
                            (invItem.invItemCount / GetShopVanillaQty(invItem)));


                }

            }

            __instance.filledSpecialInv = true;
            return false;
        }

        private static InvItem AddItemReal_Custom(InvDatabase invDatabase, string randItem)
        {
            InvItem invItem = invDatabase.AddItem(randItem, 1);

            invItem.invItemCount =
                invItem.itemType == VItemType.WeaponMelee
                    ? 200
                    : invItem.initCount;

            return invItem;
        }

        public static float GetShopVanillaQty(InvItem invItem)
        {
            if (invItem.itemType == VItemType.WeaponMelee)
                return 200f;
            else if (invItem.itemType == VItemType.WeaponProjectile || invItem.itemType == VItemType.WeaponThrown
                || invItem.isArmor || invItem.isArmorHead)
                return invItem.initCount;
            else
                return 1f;
        }
    }
}