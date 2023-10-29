using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
	public class Rate_of_Fire_Modder : T_GunNut
	{
		public override string[] AddedItemCategories => throw new System.NotImplementedException();
		public override string GunMod => VanillaItems.RateOfFireMod;
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
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Rate_of_Fire_Modder>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies a Rate of Fire Mod to all eligible ranged weapons in inventory.",
					[LanguageCode.Spanish] = "Aplica el Modificador de Velocidad de Fuego a tus armas, siempre odie a ese nombre tan poco creativo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Rate_of_Fire_Modder)),
					[LanguageCode.Spanish] = "Modificador de Armas",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					
					UnlockCost = 7,
					Unlock =
					{
						categories = { VTraitCategory.Guns }
					}
				});
		}
	}
}