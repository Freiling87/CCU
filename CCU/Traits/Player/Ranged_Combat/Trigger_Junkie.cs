using RogueLibsCore;

namespace CCU.Traits.Player.Ranged_Combat
{
    internal class Trigger_Junkie : T_RateOfFire
	{
		public override float CooldownMultiplier => 0.6f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Trigger_Junkie>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Fire rate increased by 40%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Trigger_Junkie))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 6,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 20,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
} 