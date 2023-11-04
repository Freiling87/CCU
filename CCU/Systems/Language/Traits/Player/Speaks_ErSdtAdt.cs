using RogueLibsCore;
using System;

namespace CCU.Systems.Language
{
	public class Speaks_ErSdtAdt : T_Language
	{
		public Speaks_ErSdtAdt() : base() { }

		public override string[] VanillaSpeakers => new string[] { VanillaAgents.Alien };
		public override string[] LanguageNames => new string[] { LanguageSystem.ErSdtAdt };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Speaks_ErSdtAdt>()
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