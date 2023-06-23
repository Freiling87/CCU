using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
    internal class Melee_Maniac_2 : T_MeleeSpeed
    {
		// Highest good value: 1.485
		// Lowest bad value: 1.4875
        public override float SpeedMultiplier => 1.485f;

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Melee_Maniac_2>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Melee attack speed increased by 48.5%.\n\nUh yeah, that's the fastest it can go without the animation clipping through targets.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Melee_Maniac_2))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 10,
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
