using CCU.Traits.App_HC1;
using RogueLibsCore;

namespace CCU.Traits.App_HC3
{
	public class Uncolored_Masks : T_HairColor
	{
		public override string[] Rolls => new string[] { };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Uncolored_Masks>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "If a Mask is rolled, a color is not applied to it.",
					[LanguageCode.Spanish] = "Si se elige un no-peinado (Gorila, Alien, etc.), no se le aplicara color.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Uncolored_Masks)),
					[LanguageCode.Spanish] = "Máscaras Simples",
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
