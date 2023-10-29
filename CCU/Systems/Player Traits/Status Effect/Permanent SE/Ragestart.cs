using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class Ragestart : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.Rage;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Ragestart>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You are permanently enraged. Uh... good luck?",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ragestart))
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = -16,
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