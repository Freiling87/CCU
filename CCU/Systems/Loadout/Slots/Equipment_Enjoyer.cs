using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Slots
{
	public class Equipment_Enjoyer : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Equipment_Enjoyer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "25% chance to not generate with an item in any given Slot. For Scaled/Upscaled loaders, items must still roll their chance successfully to generate.",
					[LanguageCode.Spanish] = "Chance del 25% de no generarce con un Item en cualquier espacio, Distribucion Escalada reduce la chance.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Equipment_Enjoyer)),
					[LanguageCode.Spanish] = "Fanatico del Equipamiento",
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
