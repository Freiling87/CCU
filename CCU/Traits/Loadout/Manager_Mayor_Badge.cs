using RogueLibsCore;

namespace CCU.Traits.Loadout
{
    public class Manager_Mayor_Badge : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			RogueLibs.CreateCustomTrait<Manager_Mayor_Badge>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "When placed in a chunk, this character will by default be the Badge Holder. If multiple characters have this trait, one will be chosen randomly. This will override default behaviors that assign keys to Clerks, etc.",
					[LanguageCode.Russian] = "",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CTrait.Loadout_ChunkMayorBadge,
					[LanguageCode.Russian] = "",
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
