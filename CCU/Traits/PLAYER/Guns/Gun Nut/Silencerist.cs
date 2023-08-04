using CCU.Localization;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    internal class Silencerist : T_GunNut
    {
		public override string GunMod => VanillaItems.Silencer;
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
			PostProcess = RogueLibs.CreateCustomTrait<Silencerist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies a Silencer to all ranged weapons in inventory.",
                    [LanguageCode.Spanish] = "Todas tus armas estan silenciadas.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Silencerist)),
                    [LanguageCode.Spanish] = "Silencioso y Letal",
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