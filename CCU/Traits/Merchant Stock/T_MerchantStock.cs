using BepInEx.Logging;
using BunnyLibs;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Stock
{
	public abstract class T_MerchantStock : T_CCU
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();

		public T_MerchantStock() : base() { }

		public abstract void OnAddItem(ref InvItem invItem);
		public static List<string> ExceptionItems = new List<string>()
		{
			VItemName.Taser,
			VItemName.WaterPistol,
		};
		public List<string> DurabilityTypes = new List<string>()
		{
			"WeaponMelee",
			"WeaponProjectile",
			"Wearable",
		};
		public static List<string> QuantityTypes = new List<string>()
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