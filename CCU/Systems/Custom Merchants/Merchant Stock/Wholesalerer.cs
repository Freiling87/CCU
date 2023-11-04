using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
	public class Wholesalerer : T_MerchantStock
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Wholesalerer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent sells items at 3x the normal quantity."),
					[LanguageCode.Spanish] = "Los items que este NPC vende estan agrupados y multiplicados por 3.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Wholesalerer)),
					[LanguageCode.Spanish] = "Al Por Mayorista",
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
				invItem.invItemCount *= 3;
		}
		
	}
}
