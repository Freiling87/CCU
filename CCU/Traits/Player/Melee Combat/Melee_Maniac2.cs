using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
    internal class Melee_Maniac2 : T_MeleeSpeed
    {
        public override float SpeedMultiplier => 1.4f;

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Melee_Maniac2>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Melee attack speed increased by 40%. All melee weapons have rapid fire.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Melee_Maniac2))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 8,
					IsAvailable = true,
					IsAvailableInCC = false,
					IsPlayerTrait = true,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
