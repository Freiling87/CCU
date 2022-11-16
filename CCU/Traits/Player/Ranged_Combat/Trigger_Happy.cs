using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Player.Ranged_Combat
{
    internal class Trigger_Happy : T_RateOfFire
    {
		public override float CooldownMultiplier => 0.8f;

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Trigger_Happy>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Fire rate increased by 20%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Trigger_Happy))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = new List<string> { nameof(Trigger_Junkie) },
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 10,
					Upgrade = nameof(Trigger_Junkie),
				});;
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}