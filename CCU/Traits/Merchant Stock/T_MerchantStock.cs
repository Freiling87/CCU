using BepInEx.Logging;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Stock
{
    public abstract class T_MerchantStock : T_CCU
    {
        private static readonly ManualLogSource logger = CCULogger.GetLogger();
        public static GameController GC => GameController.gameController;

        public T_MerchantStock() : base() { }

        public abstract void OnAddItem(ref InvItem invItem);
        internal List<string> DurabilityTypes = new List<string>()
        {
            "WeaponMelee",
            "WeaponProjectile",
            "Wearable",
        };
        internal static List<string> QuantityTypes = new List<string>()
        {
            "Consumable",
            "Food",
            "Tool",
            "WeaponThrown"
        };
        public static void ShopPrice(ref InvItem invItem)
        {
            invItem.itemValue = (int)((float)invItem.itemValue * ((float)invItem.invItemCount / (float)invItem.initCount));
        }
    }
}