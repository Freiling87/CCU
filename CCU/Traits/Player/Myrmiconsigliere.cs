using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Player
{
    internal class Myrmiconsigliere : T_PlayerTrait
    {
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Myrmiconsigliere>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Body armor takes 1/3 damage. Multiplies with other traits in this group.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Myrmiconsigliere)),
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 4,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 8,
					Upgrade = nameof(Myrmidon),
				});
		}
		public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
