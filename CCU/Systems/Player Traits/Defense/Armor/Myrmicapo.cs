using RogueLibsCore;

namespace CCU.Traits.Player.Armor
{
	public class Myrmicapo : T_Myrmicosanostra
	{
		public override float ArmorDurabilityChangeMultiplier => 0.66f;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Myrmicapo>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Armor damage reduced by 1/3.",
					[LanguageCode.Spanish] = "Reduce el daño a la armadura por 1/3.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Myrmicapo)),
					[LanguageCode.Spanish] = "Myrmicapo",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { nameof(Myrmidon) },
					CharacterCreationCost = 5,
					IsAvailable = true,
					IsAvailableInCC = true,
					
					UnlockCost = 7,
					Upgrade = nameof(Myrmidon),
					Unlock =
					{
						categories = { VTraitCategory.Defense }
					}
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}
