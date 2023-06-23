using CCU.Localization;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    internal class Ammo_Stocker : T_GunNut
    {
		public override string GunMod => VanillaItems.AmmoCapacityMod;
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
			PostProcess = RogueLibs.CreateCustomTrait<Ammo_Stocker>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies an Ammo Stock to all eligible ranged weapons in inventory. Only gives free ammo on start to NPCs.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Stocker)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 7,
					Unlock =
					{
						categories = { VTraitCategory.Guns }
					}
				});
		}
	}
}