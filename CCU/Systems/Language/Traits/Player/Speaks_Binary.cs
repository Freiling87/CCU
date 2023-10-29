using RogueLibsCore;
using System;

namespace CCU.Systems.Language
{
	public class Speaks_Binary : T_Language
	{
		public Speaks_Binary() : base() { }

		public override string[] VanillaSpeakers => new string[]
		{
			"ButlerBot",
			VanillaAgents.CopBot,
			VanillaAgents.Hacker,
			VanillaAgents.KillerRobot,
			VanillaAgents.Robot
		};
		public override string[] LanguageNames => new string[] { LanguageSystem.Binary };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Speaks_Binary>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to vanilla robots, Hackers, and anyone else with this trait."),
					[LanguageCode.Spanish] = "Este personaje ignora Dificultad al Hablar cuando interactua con Robots, Hackers y todos quienes tengan este rasgo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speaks_Binary)),
					[LanguageCode.Spanish] = "Habla Binario",
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