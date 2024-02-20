using RogueLibsCore;

namespace CCU.Traits.Hire_Type
{
	public class Intruder : T_HireType
	{
		public override string HiredActionButtonText => VButtonText.Hired_LockpickDoor;
		public override string ButtonTextName => VButtonText.Hire_Expert;
		public override string MoneyCostName => VDetermineMoneyCost.Hire_Thief;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Intruder>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character can be hired to break into windows or doors.",
					[LanguageCode.Spanish] = "Este NPC puede ser contratado para forzar ventanas o puertas.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Intruder)),
					[LanguageCode.Spanish] = "Intruso",

				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}