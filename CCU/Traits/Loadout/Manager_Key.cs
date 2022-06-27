using RogueLibsCore;

namespace CCU.Traits.Loadout
{
    public class Manager_Key : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Manager_Key>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Key Holder. If multiple characters have this trait, one will be chosen randomly. This will override default behaviors that assign keys to Clerks, etc.",
					
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CTrait.Loadout_ChunkKey,
					
				})
				.WithUnlock(new TraitUnlock
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
