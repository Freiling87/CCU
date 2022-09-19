using RogueLibsCore;

namespace CCU.Traits.Loadout
{
    public class Guns_Common : T_Loadout
	{
		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Guns_Common>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character will generate with a pistol, shotgun, machine gun or revolver.",
					
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = CTrait.Loadout_Guns_Common,
					
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
