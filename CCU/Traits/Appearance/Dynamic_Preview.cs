using RogueLibsCore;

namespace CCU.Traits.App
{
	public class Dynamic_Preview : T_CCU
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Dynamic_Preview>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Causes the agent's appearance to reroll on the Character Select Menu.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Dynamic_Preview)),
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