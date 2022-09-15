using CCU.Traits.Skin_Color;
using RogueLibsCore;

namespace CCU.Traits.Skin_Color_Grouped
{
	public class Vampire_Skin : T_SkinColor
	{
		public override string[] SkinColors => 
			new string[] { "SuperPaleSkin", "BlackSkin" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = PostProcess = RogueLibs.CreateCustomTrait<Vampire_Skin>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Vampire_Skin)),
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
