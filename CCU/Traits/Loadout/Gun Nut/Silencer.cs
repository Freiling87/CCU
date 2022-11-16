using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    internal class Silencer : T_GunNut
    {
		public override string GunMod => vItem.Silencer;
		public override List<string> ExcludedItems => new List<string>()
		{
			vItem.FireExtinguisher,
			vItem.Flamethrower,
			vItem.GhostGibber,
			vItem.Leafblower,
			vItem.OilContainer,
			vItem.ResearchGun,
			vItem.Taser,
			vItem.TranquilizerGun,
			vItem.WaterPistol,
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Silencer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies a Silencer to all ranged weapons in inventory.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Silencer)),
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