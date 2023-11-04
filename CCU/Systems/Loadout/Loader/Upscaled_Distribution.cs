using RogueLibsCore;

namespace CCU.Traits.Loadout_Loader
{
	public class Upscaled_Distribution : T_LoadoutLoader
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Upscaled_Distribution>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "The items added to this character in the editor have a chance to generate in their inventory on each spawn based on their value. For equippable items, chances are rolled in order from rarest to most common, and by default only one item will be generated.\n\n" +
					"The net effect is that cheaper items are rarer, and more expensive items are more common.",
					[LanguageCode.Spanish] = "Los items adregados a este personaje en el editor tiene una chance de generarce en el invetanrio del NPC en spawn, basado en su valor de menos a mas valuable, por default solo un item equipable sera elegido.\n\n" +
					"En efecto los items mas baratos son mas raros, y los items mas caros son mas comunes.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Upscaled_Distribution)),
					[LanguageCode.Spanish] = "Distribucion Escalada Mayor",
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
