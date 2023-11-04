using RogueLibsCore;
using System;

namespace CCU.Systems.Language
{
	public class Speaks_High_Goryllian : T_Language
	{
		public Speaks_High_Goryllian() : base() { }

		public override string[] VanillaSpeakers => new string[] { VanillaAgents.Gorilla };
		public override string[] LanguageNames => new string[] { LanguageSystem.Goryllian };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Speaks_High_Goryllian>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to Gorillas, and anyone else with this trait."),
					[LanguageCode.Spanish] = "Este personaje ignora Dificultad al Hablar cuando interactua con Gorilas y todos quienes tengan este rasgo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speaks_High_Goryllian)),
					[LanguageCode.Spanish] = "Habla Goriglosajon",
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
						categories = { /*None, since most characters will have one or more.*/ },
					}
				});
		}
	}
}