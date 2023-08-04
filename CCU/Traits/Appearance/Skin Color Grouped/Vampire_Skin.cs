using CCU.Traits.App_SC1;
using RogueLibsCore;

namespace CCU.Traits.App_SC2
{
	public class Vampire_Skin : T_SkinColor
	{
		public override string[] Rolls => 
			new string[] { "SuperPaleSkin", "BlackSkin" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Vampire_Skin>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Adds multiple items to the appearance pool.",
                    [LanguageCode.Spanish] = "Agrega varios colores de piel a los que el personaje puede usar.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Vampire_Skin)),
                    [LanguageCode.Spanish] = "Piel de Vampiro",
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
