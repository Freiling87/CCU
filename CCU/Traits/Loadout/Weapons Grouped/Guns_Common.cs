using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Loadout_Weapons
{
    public class Guns_Common : T_RandomWeapon
	{
		public override string[] Rolls => new string[] { vItem.MachineGun, vItem.Pistol, vItem.Shotgun, vItem.Revolver };

        //[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Guns_Common>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "This character will spawn with a random weapon from a list.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Guns_Common), "Guns (Common)"),
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
