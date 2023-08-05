using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	internal class Killer_Throwerer : T_PermanentStatusEffect_P, ISetupAgentStats
    {
		public override string statusEffectName => VanillaEffects.KillerThrower;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Killer_Throwerer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You permanently killer thrower thingers.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Killer_Throwerer))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = {  },
					CharacterCreationCost = 32,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 0,
					Upgrade = null,
					Unlock =
					{
						removal = false,
						categories = { }
					}
				});
		}
		public override void OnAdded() { }
        public override void OnRemoved() { }
	}
}