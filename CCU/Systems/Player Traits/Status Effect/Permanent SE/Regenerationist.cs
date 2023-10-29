using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class Regenerationist : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.RegenerateHealth;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Regenerationist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently shrunken.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Regenerationist))
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { nameof(Dying), VanillaTraits.ModernWarfarer },
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