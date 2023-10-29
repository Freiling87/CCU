using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
	public class Have_Not : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Have_Not>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "50% chance to not generate with any Pocket items. For Scaled / Upscaled loaders, items must still roll their chance successfully to generate.\n\n",
					[LanguageCode.Spanish] = "Chance de 50% de no generar un item de bolsillo. Para Distribucion Escalada items tienen una chance menor de aparecer.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Have_Not)),
					[LanguageCode.Spanish] = "A Veces no Tiene",
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
