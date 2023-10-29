using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
	public class Shiddy_Goods : T_MerchantStock
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Shiddy_Goods>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent sells items at 1/3 durability."),
					[LanguageCode.Spanish] = "Los items que este NPC vende tienen un 1/3 de durabilidad.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Shiddy_Goods)),
					[LanguageCode.Spanish] = "Bienes de Presupuesto",
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
			if (DurabilityTypes.Contains(invItem.itemType))
				invItem.invItemCount = (int)Math.Max(0, (invItem.invItemCount / 3f));
		}
		public override void OnRemoved() { }
	}
}
