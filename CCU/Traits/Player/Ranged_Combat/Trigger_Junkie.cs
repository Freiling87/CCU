using RogueLibsCore;

namespace CCU.Traits.Player.Ranged_Combat
{
    internal class Trigger_Junkie : T_RateOfFire
	{
		public override float CooldownMultiplier => 0.6f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Trigger_Junkie>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] =
					Core.designerEdition
						? "Fire rate cooldown decreased by 40%."
						: "Fire rate cooldown decreased by 40 %.\n\n" +
							"<color=yellow>NPCs:</color> Firing interval decreased by 40%",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Trigger_Junkie))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 10,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
} 