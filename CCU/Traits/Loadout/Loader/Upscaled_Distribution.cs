using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Loader
{
	public class Upscaled_Distribution : T_Loadout
	{
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Upscaled_Distribution>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "The items added to this character in the editor have a chance to generate in their inventory on each spawn based on their value. For equippable items, chances are rolled in order from rarest to most common, and by default only one item will be generated.\n\n" +
					"The net effect is that cheaper items are rarer, and more expensive items are more common.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Upscaled_Distribution)),
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
