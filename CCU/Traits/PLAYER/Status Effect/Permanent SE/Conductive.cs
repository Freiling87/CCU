using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	internal class Conductive : T_PermanentStatusEffect_P, ISetupAgentStats
    {
		public override string statusEffectName => VanillaEffects.ElectroTouch;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Conductive>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanent Electro Touch.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Conductive))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = {  },
					CharacterCreationCost = 10,
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