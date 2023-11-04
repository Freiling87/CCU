using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
	public class Borrow_Money : T_Interaction
	{
		public override bool AllowUntrusted => false;
		public override string ButtonID => VButtonText.BorrowMoney;
		public override bool HideCostInButton => false;
		public override string DetermineMoneyCostID => null;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Borrow_Money>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can lend money."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Borrow_Money)),

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { "Borrow Money (Moocher)" },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}
