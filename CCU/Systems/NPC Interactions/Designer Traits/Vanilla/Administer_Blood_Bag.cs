using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Administer_Blood_Bag : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.AdministerBloodBag;
		public override bool NonMoneyCost => true;
		public override string MoneyCostName => VDetermineMoneyCost.HP_20;

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
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}