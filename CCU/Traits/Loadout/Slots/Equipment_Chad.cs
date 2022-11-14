using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Slots
{
    internal class Equipment_Chad : T_Loadout
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Equipment_Chad>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Flat Distribution Loader: No effect.\n\n" +
					"Scaled & Upscaled Distribution Loaders: Agent will never fail to generate an equippable item in a slot, if there are any added to their item pool. Including the Stacy slot! REEEEEEEEEEEEE",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Equipment_Chad)),
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
