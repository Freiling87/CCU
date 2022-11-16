using RogueLibsCore;

namespace CCU.Traits.Player.Ammo
{
    internal class Ammo_Artiste : T_AmmoCap
    {
		public override float AmmoCapMultiplier => 2f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Ammo_Artiste>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ammo capacity multiplied by 2.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Artiste))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 4,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 5,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
