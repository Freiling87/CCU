using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	internal class Thickest_Skin : T_PermanentStatusEffect_P, ISetupAgentStats
    {
		public override string statusEffectName => VanillaEffects.ResistDamageLarge;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Thickest_Skin>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Divides incoming damage by 2.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Thickest_Skin))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = {  },
					CharacterCreationCost = 32,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 0,
					Upgrade = nameof(Thickester_Skin),
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