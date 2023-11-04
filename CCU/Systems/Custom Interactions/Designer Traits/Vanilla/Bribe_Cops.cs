using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Bribe_Cops : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.BribeCops;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => VDetermineMoneyCost.BribeCops;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bribe_Cops>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will accept cash to bribe law enforcement."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bribe_Cops)),

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
		
		
	}
}
