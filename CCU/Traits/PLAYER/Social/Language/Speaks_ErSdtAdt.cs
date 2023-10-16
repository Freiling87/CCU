using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Language
{
	public class Speaks_ErSdtAdt : T_Language
	{
		public override string[] VanillaSpeakers => new string[]
		{
			VanillaAgents.Alien
		};
		public override string[] LanguageNames => new string[] { "ErSdtAdt" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Speaks_ErSdtAdt>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to Aliens, and anyone else with this trait."),
					[LanguageCode.Spanish] = "Este personaje ignora Dificultad al Hablar cuando interactua con Aliens y todos quienes tengan este rasgo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speaks_ErSdtAdt)),
					[LanguageCode.Spanish] = "Habla ErTngAo",
				})
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { },
					CharacterCreationCost = 1,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
					UnlockCost = 3,
					//Unlock = { upgrade = nameof(Polyglot) }
					Unlock =
					{
						categories = { VTraitCategory.Social },
					}
				});
		}
		public override void OnAdded() { }
		public override void OnRemoved() { }
	}
}