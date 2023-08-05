using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	internal class Invisibility_Enjoyer : T_PermanentStatusEffect_P, ISetupAgentStats
    {
		public override string statusEffectName => VanillaEffects.Invisible;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Invisibility_Enjoyer>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently Invisible. Enjoy.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Invisibility_Enjoyer))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { VanillaTraits.BlendsInNicely },
					CharacterCreationCost = 100,
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