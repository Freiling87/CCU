using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Slots
{
    internal class Sidearmed_to_the_Teeth : T_Loadout
    {
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Sidearmed_to_the_Teeth>()
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
				.WithUnlock(new TraitUnlock_CCU
				{
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