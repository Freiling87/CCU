using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Slots
{
	public class Sidearmed_to_the_Teeth : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Sidearmed_to_the_Teeth>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Allows any number of equippable items within the same slot to be generated in inventory.",
					[LanguageCode.Spanish] = "Permite que un numero inlimitado de items equipables en el mismo espacio de inventario.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Sidearmed_to_the_Teeth)),
					[LanguageCode.Spanish] = "Recontra Armado Hasta Los Dientes",
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