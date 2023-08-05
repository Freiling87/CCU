using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	internal class Strong_Immune_System : T_PermanentStatusEffect_P, ISetupAgentStats
    {
		public override string statusEffectName => VanillaEffects.StableSystem;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Strong_Immune_System>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You're immune to negative status effects.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Strong_Immune_System))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { VanillaTraits.LongerStatusEffects },
					CharacterCreationCost = 16,
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