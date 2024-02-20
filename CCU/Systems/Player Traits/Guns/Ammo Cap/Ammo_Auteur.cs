using RogueLibsCore;

namespace CCU.Traits.Player.Ammo
{
	public class Ammo_Auteur : T_AmmoCap
	{
		public override float AmmoCapMultiplier => 2.2f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Ammo_Auteur>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ammo capacity increased by 120%.",
					[LanguageCode.Spanish] = "Aumenta la capacidad de munición por 120%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Auteur)),
					[LanguageCode.Spanish] = "Auteur de la Munición",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = {
						nameof(Ammo_Amateur),
						nameof(Ammo_Artiste)
					},
					CharacterCreationCost = 7,
					IsAvailable = false,
					IsAvailableInCC = false,
					
					UnlockCost = 9,
					Unlock =
					{
						categories = { VTraitCategory.Guns },
						isUpgrade = true,
					}
				});
		}
	}
}