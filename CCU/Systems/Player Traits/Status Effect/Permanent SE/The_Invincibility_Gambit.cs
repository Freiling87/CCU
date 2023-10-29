using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class The_Invincibility_Gambit : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.KillerThrower;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<The_Invincibility_Gambit>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ah, you clever strategist, you!\n\nNO NUGGETS EVER.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(The_Invincibility_Gambit))
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 100,
					IsAvailable = false,
					IsAvailableInCC = true,
					
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