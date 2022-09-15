using RogueLibsCore;

namespace CCU.Traits.Skin_Color
{
	public class White_Skin : T_SkinColor
	{
		public override string[] SkinColors => new string[] { "White" };

        [RLSetup]
		public static void Setup()
		{
			PostProcess = PostProcess = RogueLibs.CreateCustomTrait<White_Skin>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(White_Skin)),
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
