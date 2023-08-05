using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	internal class Thick_Skin : T_PermanentStatusEffect_P, ISetupAgentStats
    {
		public override string statusEffectName => VanillaEffects.ResistDamageSmall;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Thick_Skin>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Divides incoming damage by 1.25.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Thick_Skin))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = {  },
					CharacterCreationCost = 16,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 0,
					Upgrade = nameof(Thicker_Skin),
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