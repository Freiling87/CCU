using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
	public class Masterworker : T_MerchantStock
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Masterworker>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This agent sells items at 2x durability."),
					[LanguageCode.Spanish] = "Los items que este NPC vende tienen un 2x de durabilidad.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Masterworker)),
					[LanguageCode.Spanish] = "Fabricante Maestro",
				})
				.WithUnlock(new TraitUnlock_CCU
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
				invItem.invItemCount *= 2;
		}
		public override void OnRemoved() { }
	}
}