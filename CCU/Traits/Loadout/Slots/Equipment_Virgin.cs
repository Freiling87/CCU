using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Slots
{
	internal class Equipment_Virgin : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Equipment_Virgin>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Flat Distribution Loader: 50% chance to not generate an item in any given Slot.\n\n" +
					"Scaled & Upscaled Distribution Loaders: Agent has halved chances of generating equippable items.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Equipment_Virgin)),
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
