using RogueLibsCore;

namespace CCU.Traits.Player.Armor
{
    internal class Myrmidon : T_Myrmicosanostra
    {
        public override float ArmorDurabilityChangeMultiplier => 0.33f;

        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Myrmidon>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Armor damage reduced by 2/3.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Myrmidon)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 6,
					IsAvailable = true,
					IsAvailableInCC = false,
					IsPlayerTrait = true,
					UnlockCost = 12,
				});
		}
		public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
