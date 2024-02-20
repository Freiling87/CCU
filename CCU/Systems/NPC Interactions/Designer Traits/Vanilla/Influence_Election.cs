using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Influence_Election : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.InfluenceElection;
		public override string MoneyCostName => VDetermineMoneyCost.BribeElection;
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& !GC.sessionData.electionBribedMob[interactingAgent.isPlayer];

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Influence_Election>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be paid to sway the vote."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Influence_Election)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}