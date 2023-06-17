using RogueLibsCore;

namespace CCU.Traits.Loadout_Loader
{
	public class Scaled_Distribution : T_LoadoutLoader
	{
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Scaled_Distribution>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "The items added to this character in the editor have a chance to generate in their inventory on each spawn, based inversely on their value. Chances are rolled in order from most to least valuable, and by default only one item per equippable slot will be generated.\n\n" +
					"The net effect is that cheaper items are more common, and more expensive items are rarer.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Scaled_Distribution)),
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
