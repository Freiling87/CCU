using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class The_Invincibility_Gambit : T_PermanentStatusEffect_P, ISetupAgentStats
	{
		public override string statusEffectName => VanillaEffects.KillerThrower;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<The_Invincibility_Gambit>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ah, you clever strategist, you!\n\nNO NUGGETS EVER.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(The_Invincibility_Gambit))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 100,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 0,
					Upgrade = null,
					Unlock =
					{
						removal = false,
						categories = { VTraitCategory.Defense }
					}
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}