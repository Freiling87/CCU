using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Give_Blood : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.DonateBlood;
		public override string MoneyCostName =>
			GameController.gameController.challenges.Contains(VanillaMutators.LowHealth)
				? VDetermineMoneyCost.GiveBloodLowHealth
				: VDetermineMoneyCost.GiveBlood;
		public override bool NonMoneyCost => true;

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
				.WithUnlock(new TU_DesignerUnlock());
		}		
	}
}