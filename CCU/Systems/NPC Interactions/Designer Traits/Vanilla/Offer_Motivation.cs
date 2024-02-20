using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Offer_Motivation : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.OfferMotivation;
		public override bool RequireTrust => false;
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& !Owner.oma.offeredOfficeDrone;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Offer_Motivation>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be given small items, and will become Friendly.\n\nBypasses Untrusting traits."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Offer_Motivation)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}