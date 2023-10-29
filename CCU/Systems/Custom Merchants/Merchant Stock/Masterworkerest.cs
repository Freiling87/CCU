using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
	public class Masterworkerest : T_MerchantStock
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Masterworkerest>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent sells items at 4x durability."),
					[LanguageCode.Spanish] = "Los items que este NPC vende tienen un 4x de durabilidad.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Masterworkerest)),
					[LanguageCode.Spanish] = "Fabricante de Obras Maestras",
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
				invItem.invItemCount *= 4;
		}
		public override void OnRemoved() { }
	}
}
