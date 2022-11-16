using RogueLibsCore;
using System.Collections.Generic;

namespace CCU.Traits.Player.Ammo
{
    internal class Ammo_Amateur : T_AmmoCap
    {
        public override float AmmoCapMultiplier => 0.5f;

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Ammo_Amateur>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ammo capacity multiplied by 0.5.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Amateur))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = new List<string> { nameof(Ammo_Artiste), nameof(Ammo_Auteur) },
					CharacterCreationCost = -1,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 5,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
