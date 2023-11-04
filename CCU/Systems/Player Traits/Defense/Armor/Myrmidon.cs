using RogueLibsCore;

namespace CCU.Traits.Player.Armor
{
	public class Myrmidon : T_Myrmicosanostra
	{
		public override float ArmorDurabilityChangeMultiplier => 0.33f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Myrmidon>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Armor damage reduced by 2/3.",
					[LanguageCode.Spanish] = "Reduce el daño a la armadura por 2/3.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Myrmidon)),
					[LanguageCode.Spanish] = "Myrmidon",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { nameof(Myrmicapo) },
					CharacterCreationCost = 7,
					IsAvailable = false,
					IsAvailableInCC = true,
					
					UnlockCost = 12,
					Unlock =
					{
						categories = { VTraitCategory.Defense },
						isUpgrade = true,
					}
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
