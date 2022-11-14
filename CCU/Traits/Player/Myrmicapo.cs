using CCU.Traits.Loadout;
using RogueLibsCore;

namespace CCU.Traits.Player
{
    internal class Myrmicapo : T_PlayerTrait
    {
        [RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Myrmicapo>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Body armor takes 1/2 damage. Multiplies with other traits in this group.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Myrmicapo))
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					CharacterCreationCost = 2,
					IsAvailable = true,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 4,
					Upgrade = nameof(Myrmiconsigliere),
				});
		}
		public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
