using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
	public class Clearancer : T_MerchantStock
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Clearancer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Enables duplicate items to be drawn from inventory lists."),
					[LanguageCode.Spanish] = "Permite que items duplicados sean elegidos en la tienda de inventario.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Clearancer)),
					[LanguageCode.Spanish] = "Vendedor Acreditado",
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
		
		public override void OnAddItem(ref InvItem invItem) { }
		
	}
}