using CCU.Traits.App_BC1;
using RogueLibsCore;

namespace CCU.Traits.App_BC3
{
	public class Shirtless : T_BodyColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Shirtless>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Causes body to match skin color.",
					[LanguageCode.Spanish] = "Hace que el color de cuerpo sea el mismo que el color de piel, como el Luchador.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Shirtless)),
					[LanguageCode.Spanish] = "Al Desnudo",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		
	}
}
