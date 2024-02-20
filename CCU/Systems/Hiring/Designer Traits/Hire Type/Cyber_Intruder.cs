using RogueLibsCore;

namespace CCU.Traits.Hire_Type
{
	public class Cyber_Intruder : T_HireType
	{
		public override string HiredActionButtonText => VButtonText.Hired_HackSomething;
		public override string ButtonTextName => VButtonText.Hire_Expert;
		public override string MoneyCostName => VDetermineMoneyCost.Hire_Hacker;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cyber_Intruder>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character can be hired to hack something remotely.",
					[LanguageCode.Spanish] = "Este NPC puede ser contratador para hackear remotamente.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cyber_Intruder), "Cyber-Intruder"),
					[LanguageCode.Spanish] = "Cyber-Intruso",
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}