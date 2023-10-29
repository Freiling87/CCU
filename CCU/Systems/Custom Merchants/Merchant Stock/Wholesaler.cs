using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
	public class Wholesaler : T_MerchantStock
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Wholesaler>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent sells items at 2x the normal quantity."),
					[LanguageCode.Spanish] = "Los items que este NPC vende estan agrupados y multiplicados por 2.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Wholesaler)),
					[LanguageCode.Spanish] = "Mayorista",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnAddItem(ref InvItem invItem)
		{
			if (QuantityTypes.Contains(invItem.itemType))
				invItem.invItemCount *= 2;
		}
		public override void OnRemoved() { }
	}
}
