using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class Regenerationist : T_PermanentStatusEffect_P, ISetupAgentStats
	{
		public override string statusEffectName => VanillaEffects.RegenerateHealth;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Regenerationist>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently shrunken.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Regenerationist))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { nameof(Dying), VanillaTraits.ModernWarfarer },
					CharacterCreationCost = 32,
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