using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    internal class Ammo_Stock : T_GunNut
    {
		public override string GunMod => vItem.AmmoCapacityMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			vItem.OilContainer,
			vItem.ResearchGun,
			vItem.Taser,
			vItem.WaterPistol,
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Ammo_Stock>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies an Ammo Stock to all ranged weapons in inventory.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Ammo_Stock)),
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