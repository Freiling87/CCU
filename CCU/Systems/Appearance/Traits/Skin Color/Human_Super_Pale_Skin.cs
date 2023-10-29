using RogueLibsCore;

namespace CCU.Traits.App_SC1
{
	public class Human_Super_Pale_Skin : T_SkinColor
	{
		public override string[] Rolls => new string[] { "SuperPaleSkin" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Human_Super_Pale_Skin>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds this item to the appearance pool.",
					[LanguageCode.Spanish] = "Agrega este color de piel a los que el personaje puede usar.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Human_Super_Pale_Skin)),
					[LanguageCode.Spanish] = "Piel Natural Muy Palida",
				})
				.WithUnlock(new TU_DesignerUnlock
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
