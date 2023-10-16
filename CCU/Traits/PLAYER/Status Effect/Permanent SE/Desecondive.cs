using BunnyLibs;
using RogueLibsCore;

namespace CCU.Traits.Player.Status_Effect
{
	public class Desecondive : T_PermanentStatusEffect_P, ISetupAgentStats
	{
		public override string statusEffectName => VanillaEffects.Shrunk;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Desecondive>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Permanently 1/60 the size of someone who's Diminutive.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Desecondive))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { VanillaTraits.Bulky, nameof(Gigantic) },
					CharacterCreationCost = -32,
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