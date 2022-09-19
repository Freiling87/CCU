using RogueLibsCore;

namespace CCU.Traits.Loadout
{
    public class Chunk_Stash_Hint : T_Loadout
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Chunk_Stash_Hint>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Stash Hint Holder. If multiple characters have this trait, one will be chosen randomly.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Chunk_Stash_Hint)),
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
