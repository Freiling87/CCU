using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Loadout_Gun_Nut
{
    internal class Accuracy_Modder : T_GunNut
    {
		public override string GunMod => VanillaItems.AccuracyMod;
		public override List<string> ExcludedItems => new List<string>()
		{
			vItem.OilContainer, 
			vItem.ResearchGun,
			vItem.WaterPistol,
		};

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Accuracy_Modder>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Agent applies an Accuracy Mod to all eligible ranged weapons in inventory.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Accuracy_Modder)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 7,
				});
		}
	}
}