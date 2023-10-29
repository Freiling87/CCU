using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Buy_Round : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.BuyRound;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Buy_Round>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be paid to serve a round of drinks to everyone in the chunk."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Buy_Round)),

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
