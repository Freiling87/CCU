using RogueLibsCore;

namespace CCU.Traits.App
{
	public class Dynamic_Player_Appearance : T_CCU
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Dynamic_Player_Appearance>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Activates the Appearance System for player versions of this character.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Dynamic_Player_Appearance)),
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