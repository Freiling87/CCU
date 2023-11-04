using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class Strong_Immune_System : T_PermanentStatusEffect, ISetupAgentStats
	{
		public override string StatusEffectName => VanillaEffects.StableSystem;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Strong_Immune_System>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "You're immune to negative status effects.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Strong_Immune_System))
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { VanillaTraits.LongerStatusEffects },
					CharacterCreationCost = 16,
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
		
		
	}
}