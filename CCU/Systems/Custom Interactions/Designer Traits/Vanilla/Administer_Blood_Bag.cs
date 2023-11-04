using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Administer_Blood_Bag : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.AdministerBloodBag;
		public override bool HideCostInButton => true;
		public override string DetermineMoneyCostID => VDetermineMoneyCost.HP_20;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Administer_Blood_Bag>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can give you a blood bag, at the cost of 20 HP."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Administer_Blood_Bag)),

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
