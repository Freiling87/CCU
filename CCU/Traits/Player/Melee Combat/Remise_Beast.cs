using RogueLibsCore;

namespace CCU.Traits.Player.Melee_Combat
{
    internal class Remise_Beast : T_MeleeSpeed
	{
		public override float SpeedMultiplier => 1f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Remise_Beast>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "All melee weapons have rapid fire.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Remise_Beast))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 15,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
