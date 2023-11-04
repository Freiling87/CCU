using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Bribe_for_Entry_Alcohol : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => null;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bribe_for_Entry_Alcohol>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character, if serving as Doorman, will allow access if bribed with alcohol."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bribe_for_Entry_Alcohol), ("Bribe for Entry (Alcohol)")),

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
