using RogueLibsCore;

namespace CCU.Traits.Player.Armor
{
    internal class Myrmicapo : T_Myrmicosanostra
    {
        public override float ArmorDurabilityChangeMultiplier => 0.66f;

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Myrmicapo>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Armor damage reduced by 1/3.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Myrmicapo))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { nameof(Myrmidon) },
					CharacterCreationCost = 3,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 6,
					Upgrade = nameof(Myrmidon),
				});
		}
		public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
