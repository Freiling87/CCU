using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	internal class Above_the_Laws : T_PermanentStatusEffect_P, ISetupAgentStats
    {
		public override string statusEffectName => VanillaEffects.AbovetheLaw;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Above_the_Laws>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently Above the Law... s.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Above_the_Laws))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { 
						VanillaTraits.CorruptionCosts, 
						VanillaTraits.TheLaw, 
						VanillaTraits.Wanted
					},
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