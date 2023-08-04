using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
    internal class Melee_Maniac : T_MeleeSpeed
    {
		public override float SpeedMultiplier => 1.25f;

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Melee_Maniac>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Melee attack speed increased by 25%.",
                    [LanguageCode.Spanish] = "Aumenta la velocidad de ataques cuerpo a cuerpo por 25%.",
                })
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Melee_Maniac))
                    [LanguageCode.Spanish] = "Violentatico",
                })
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 5,
					Upgrade = nameof(Melee_Maniac_2),
					Unlock =
					{
						categories = { VTraitCategory.Melee },
					}
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
