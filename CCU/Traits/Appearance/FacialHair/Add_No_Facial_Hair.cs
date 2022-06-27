using RogueLibsCore;

namespace CCU.Traits.Facial_Hair
{
	public class NoFacialHair : T_FacialHair
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<NoFacialHair>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this trait to the appearance pool. If this character is generated as an NPC, their appearance will be randomized between all appearance traits in the pool.",
					
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DisplayName(typeof(NoFacialHair)),
					
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
