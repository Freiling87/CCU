using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Borrow_Money_Moocher : T_InteractionNPC
	{
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& interactingAgent.HasTrait(VanillaTraits.Moocher);

		public override string ButtonTextName => VButtonText.BorrowMoney;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Borrow_Money_Moocher>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can lend money, if the player has the Moocher trait."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Borrow_Money_Moocher), ("Borrow Money (Moocher)")),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}