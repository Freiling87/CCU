using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Buy_Slave : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.PurchaseSlave;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null; // Determined in code

		//[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Buy_Slave>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("If this character owns any Slaves, they will sell them."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Buy_Slave)),

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
		public override void OnRemoved() { }
	}
}
