using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Borrow_Money : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.BorrowMoney; // Rest is in Pay Debt

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
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}