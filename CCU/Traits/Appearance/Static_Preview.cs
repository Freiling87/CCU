using RogueLibsCore;

namespace CCU.Traits.App
{
	public class Static_Preview : T_CCU
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Static_Preview>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Prevents the character select screen's thumbnail from randomizing between runs. Use this if you want a certain look when playing as this character, or if you need your character select screen to be more easily organized.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Static_Preview)),
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
