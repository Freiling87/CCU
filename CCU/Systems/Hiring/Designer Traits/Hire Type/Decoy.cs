using RogueLibsCore;

namespace CCU.Traits.Hire_Type
{
	public class Decoy : T_HireType
	{
		public override string HiredActionButtonText => VButtonText.Hired_CauseRuckus;
		public override string ButtonTextName => VButtonText.Hire_Expert;
		public override string MoneyCostName => VDetermineMoneyCost.Hire_Thief;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Decoy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character can be hired to cause a distraction, or a Ruckus, if you will.",
					[LanguageCode.Spanish] = "Este NPC puede ser contratado para hacer un ruidoso alboroto y despues abandonarte, una buena inversion!",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Decoy)),
					[LanguageCode.Spanish] = "Distracion",
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}