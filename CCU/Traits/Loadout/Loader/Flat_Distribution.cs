using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Loader
{
	public class Flat_Distribution : T_Loadout
	{
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Flat_Distribution>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent spawns with the inventory items added in the Character Creator. For equippable items, one item from each category is randomly chosen from items added.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Flat_Distribution)),
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
