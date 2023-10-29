using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class Lucky_Duck : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.FeelingLucky;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Lucky_Duck>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You are lucky.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Lucky_Duck))
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 7,
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