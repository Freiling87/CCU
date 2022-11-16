using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    internal class Rubber_Bullets : T_GunNut
    {
		public override string GunMod => vItem.RubberBulletsMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			vItem.FireExtinguisher,
			vItem.Flamethrower,
			vItem.FreezeRay,
			vItem.GhostGibber,
			vItem.Leafblower,
			vItem.OilContainer,
			vItem.ResearchGun,
			vItem.RocketLauncher,
			vItem.ShrinkRay,
			vItem.Taser,
			vItem.TranquilizerGun,
			vItem.WaterPistol,
		};

		//[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Rubber_Bullets>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies a Rubber Bullets mod to all traditional firearms in inventory. These knock out targets instead of killing them.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Rubber_Bullets)),
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