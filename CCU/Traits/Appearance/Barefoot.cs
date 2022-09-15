using RogueLibsCore;

namespace CCU.Traits.Appearance
{
	public class Barefoot : T_CCU
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Barefoot>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Causes legs to match skin color.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Barefoot)),
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
