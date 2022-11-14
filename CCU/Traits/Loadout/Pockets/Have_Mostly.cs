using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Pockets
{
	internal class Have_Mostly : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Have_Mostly>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Flat Distribution Loader: 25% chance to not generate with a pocket item.\n\n" +
					"Scaled & Upscaled Distribution Loaders: Agent has doubled chances of generating pocket items.\n\n" + 
					"It turns out there are <i>three</i> kinds of people. Who knew?",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Have_Mostly)),
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
