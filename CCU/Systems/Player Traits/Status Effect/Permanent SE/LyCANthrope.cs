using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class LyCANthrope : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Werewolf;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<LyCANthrope>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "There's nothing you can't do. You're a werewolf forever.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(LyCANthrope))
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 32,
					IsAvailable = false,
					IsAvailableInCC = true,
					
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