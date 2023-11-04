using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Slots
{
	public class Sidearmed_but_on_Both_Sides : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Sidearmed_but_on_Both_Sides>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Allows two additional equippable items within the same slot to be generated in inventory.",
					[LanguageCode.Spanish] = "Permite que dos items equipables addicionales en el mismo espacio de inventario.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Sidearmed_but_on_Both_Sides)),
					[LanguageCode.Spanish] = "Recontra Armado",
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