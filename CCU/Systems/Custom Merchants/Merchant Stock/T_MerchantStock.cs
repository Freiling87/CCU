using BepInEx.Logging;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Stock
{
	public abstract class T_MerchantStock : T_DesignerTrait
	{
		private static readonly ManualLogSource logger = BLLogger.GetLogger();

		public T_MerchantStock() : base() { }

		public abstract void OnAddItem(ref InvItem invItem);
		public static List<string> ZeroQtyItems = new List<string>()
		{
			VItemName.BombProcessor,
			VItemName.Taser,
			VItemName.WaterPistol,
		};
		public List<string> DurabilityTypes = new List<string>()
		{
			VItemType.WeaponMelee,
			VItemType.WeaponProjectile,
			VItemType.Wearable,
		};
		public static List<string> QuantityTypes = new List<string>()
		{
			VItemType.Consumable,
			VItemType.Food,
			VItemType.Tool,
			VItemType.WeaponThrown,
		};
		public static void ShopPrice(ref InvItem invItem)
		{
			logger.LogDebug($"ShopPrice: {invItem.invItemName}");
			invItem.itemValue = (int)(invItem.itemValue * ((float)invItem.invItemCount / (float)invItem.initCount));
		}
	}
}