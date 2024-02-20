using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
	public class Ammo_Stocker : T_GunNut
	{
		public Ammo_Stocker() : base() { }

		public override string[] AddedItemCategories => new string[] { };
		public override string GunMod => VanillaItems.AmmoCapacityMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			VItemName.OilContainer,
			VItemName.ResearchGun,
			VItemName.Taser,
			VItemName.WaterPistol,
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Ammo_Stocker>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies an Ammo Stock to all eligible ranged weapons in inventory. Gives max ammo on start to NPCs.",
					[LanguageCode.Spanish] = "Aplica el Stock de Munición a todas tus armas, NPCs con este rasgo obtienen un poco de munición gratis.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Stocker)),
					[LanguageCode.Spanish] = "Restocker de Munición",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = Core.designerEdition,
					
					UnlockCost = 7,
					Unlock =
					{
						categories = { VTraitCategory.Guns }
					}
				});
		}
	}
}