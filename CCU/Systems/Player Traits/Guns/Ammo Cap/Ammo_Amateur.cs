using RogueLibsCore;

namespace CCU.Traits.Player.Ammo
{
	public class Ammo_Amateur : T_AmmoCap
	{
		public override float AmmoCapMultiplier => 1.4f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Ammo_Amateur>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ammo capacity increased by 40%.",
					[LanguageCode.Spanish] = "Aumenta la capacidad de munición por 40%.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Amateur)),
					[LanguageCode.Spanish] = "Amateur de la Munición",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { nameof(Ammo_Artiste), nameof(Ammo_Auteur) },
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = true,
					
					UnlockCost = 5,
					Upgrade = nameof(Ammo_Artiste),
					Unlock =
					{
						categories = { VTraitCategory.Guns }
					}
				});
		}
	}
}