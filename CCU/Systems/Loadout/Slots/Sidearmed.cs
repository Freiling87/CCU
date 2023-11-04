using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Slots
{
	public class Sidearmed : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Sidearmed>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Allows one additional equippable item within the same slot to be generated in inventory.",
					[LanguageCode.Spanish] = "Permite que un item equipable addicional en el mismo espacio de inventario.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Sidearmed)),
					[LanguageCode.Spanish] = "Re Armado",
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