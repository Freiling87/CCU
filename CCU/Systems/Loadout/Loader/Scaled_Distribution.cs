using RogueLibsCore;

namespace CCU.Traits.Loadout_Loader
{
	public class Scaled_Distribution : T_LoadoutLoader
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Scaled_Distribution>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "The items added to this character in the editor have a chance to generate in their inventory on each spawn, based inversely on their value. Chances are rolled in order from most to least valuable, and by default only one item per equippable slot will be generated.\n\n" +
					"The net effect is that cheaper items are more common, and more expensive items are rarer.",
					[LanguageCode.Spanish] = "Los items adregados a este personaje en el editor tiene una chance de generarce en el invetanrio del NPC en spawn, basado en su valor de mas a menos valuable, por default solo un item equipable sera elegido.\n\n" +
					"En efecto los items mas baratos son mas comunes, y los items caros son mas raros.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Scaled_Distribution)),
					[LanguageCode.Spanish] = "Distribucion Escalada",
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
