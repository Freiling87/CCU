using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
	public class Wholesalerest : T_MerchantStock
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Wholesalerest>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent sells items at 4x the normal quantity."),
					[LanguageCode.Spanish] = "Los items que este NPC vende estan agrupados y multiplicados por 4.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Wholesalerest)),
					[LanguageCode.Spanish] = "Al Por Mayor Mayorista",
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
		
		public override void OnAddItem(ref InvItem invItem)
		{
			if (QuantityTypes.Contains(invItem.itemType))
				invItem.invItemCount *= 4;
		}
		
	}
}
