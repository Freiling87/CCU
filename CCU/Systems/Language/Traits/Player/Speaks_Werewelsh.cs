using RogueLibsCore;
using System;

namespace CCU.Systems.Language
{
	public class Speaks_Werewelsh : T_Language
	{
		public Speaks_Werewelsh() : base() { }

		public override string[] VanillaSpeakers => new string[] { VanillaAgents.Werewolf, VanillaAgents.WerewolfTransformed };
		public override string[] LanguageNames => new string[] { LanguageSystem.Werewelsh };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Speaks_Werewelsh>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to Werewolves (both forms), and anyone else with this trait."),
					[LanguageCode.Spanish] = "Este personaje ignora Dificultad al Hablar cuando interactua con Hombres Lobo y todos quienes tengan este rasgo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speaks_Werewelsh)),
					[LanguageCode.Spanish] = "Habla Lobezno",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 1,
					IsAvailable = false,
					IsAvailableInCC = true,
					
					UnlockCost = 3,
					//Unlock = { upgrade = nameof(Polyglot) }
					Unlock =
					{
						categories = { VTraitCategory.Social },
					}
				});
		}
	}
}