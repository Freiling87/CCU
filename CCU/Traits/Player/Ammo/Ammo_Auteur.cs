using RogueLibsCore;

namespace CCU.Traits.Player.Ammo
{
    internal class Ammo_Auteur : T_AmmoCap
    {
		public override float AmmoCapMultiplier => 3f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Ammo_Auteur>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Ammo capacity multiplied by 3.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Ammo_Auteur))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 6,
					IsAvailable = true,
					IsAvailableInCC = false,
					IsPlayerTrait = true,
					UnlockCost = 5,
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
