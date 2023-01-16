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
					[LanguageCode.English] = "Randomly selects an item for each pool (including pockets), with a (1/N+1)% chance to generate no item, where N is the number of items in the pool.",
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