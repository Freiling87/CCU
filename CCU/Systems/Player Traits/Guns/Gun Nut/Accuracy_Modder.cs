using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
	public class Accuracy_Modder : T_GunNut
	{
		public override string[] AddedItemCategories => new string[] { };
		public override string GunMod => VanillaItems.AccuracyMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			VItemName.OilContainer,
			VItemName.ResearchGun,
			VItemName.WaterPistol,
		};

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Accuracy_Modder>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies an Accuracy Mod to all eligible ranged weapons in inventory.",
					[LanguageCode.Spanish] = "Aplica el Moderador de Precisión a tus armas, en realidad se llama (quotes)Moderador para Mayor Precisión(quotes) pero ese es un nombre terrible.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Accuracy_Modder)),
					[LanguageCode.Spanish] = "Moderador de Armas",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { VanillaTraits.StubbyFingers, VanillaTraits.Pacifist }, // More?
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