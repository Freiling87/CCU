using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
	internal class Have_Some : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Have_Some>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "25% chance to not generate with any Pocket items. For Scaled / Upscaled loaders, items must still roll their chance successfully to generate.\n\n" + 
					"It turns out there are <i>three</i> kinds of people. Who knew?",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Have_Some)),
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
