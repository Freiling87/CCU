using RogueLibsCore;

namespace CCU.Traits.Player.Ammo
{
    internal class Ammo_Artiste : T_AmmoCap
    {
		public override float AmmoCapMultiplier => 1.8f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Ammo_Artiste>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ammo capacity increased by 80%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Artiste))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { nameof(Ammo_Amateur), nameof(Ammo_Auteur) },
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 5,
					Upgrade = nameof(Ammo_Auteur),
				});
		}
	}
}