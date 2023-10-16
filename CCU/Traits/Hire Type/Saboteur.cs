using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Hire_Type
{
	public class Saboteur : T_HireType
	{
		public override string HiredActionButtonText => CJob.TamperSomething;
		public override string ButtonText => VButtonText.Hire_Expert;
		public override object HireCost => VDetermineMoneyCost.Hire_Hacker;

		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Saboteur>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character can be hired to tamper with machinery and electronics.",
					[LanguageCode.Spanish] = "Este NPC puede ser contratado para manipular maquineria.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Saboteur)),
					[LanguageCode.Spanish] = "Saboteador",

				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
