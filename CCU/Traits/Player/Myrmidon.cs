using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Player
{
    internal class Myrmidon : T_PlayerTrait
    {
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Myrmidon>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Body armor takes 1/4 damage. Multiplies with other traits in this group.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Myrmidon)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 6,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 12,
				});
		}
		public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
