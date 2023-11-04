using RogueLibsCore;
using System;

namespace CCU.Systems.Language
{
	public class Speaks_Foreign : T_Language
	{
		public Speaks_Foreign() : base() { }

		public override string[] VanillaSpeakers => new string[] { VanillaAgents.Assassin };
		public override string[] LanguageNames => new string[] { LanguageSystem.Foreign };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Speaks_Foreign>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to Assassins and anyone else with this trait."),
					[LanguageCode.Spanish] = "Este personaje ignora Dificultad al Hablar cuando interactua con Asesinos y todos quienes tengan este rasgo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speaks_Foreign)),
					[LanguageCode.Spanish] = "Habla Extranjero",
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