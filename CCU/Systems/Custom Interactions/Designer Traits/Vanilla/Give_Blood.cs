using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Give_Blood : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.DonateBlood;
		public override bool HideCostInButton => true;
		public override string DetermineMoneyCostID =>
			GameController.gameController.challenges.Contains("LowHealth")
				? VDetermineMoneyCost.GiveBloodLowHealth
				: VDetermineMoneyCost.GiveBlood;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Give_Blood>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can help donate blood for cash."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Give_Blood)),

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
