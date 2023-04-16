using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
    internal class Melee_Maniac_2 : T_MeleeSpeed
    {
        public override float SpeedMultiplier => 1.50f;

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Melee_Maniac_2>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Melee attack speed increased by 50%.",
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
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
