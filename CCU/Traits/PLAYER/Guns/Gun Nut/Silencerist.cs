using BunnyLibs;
using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
	public class Silencerist : T_GunNut
	{
		public override string GunMod => VanillaItems.Silencer;
		public override List<string> ExcludedItems => new List<string>()
		{
			VItemName.FireExtinguisher,
			VItemName.Flamethrower,
			VItemName.GhostGibber,
			VItemName.Leafblower,
			VItemName.OilContainer,
			VItemName.ResearchGun,
			VItemName.Taser,
			VItemName.TranquilizerGun,
			VItemName.WaterPistol,
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