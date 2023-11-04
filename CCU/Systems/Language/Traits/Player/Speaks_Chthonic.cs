using RogueLibsCore;
using System;

namespace CCU.Systems.Language
{
	public class Speaks_Chthonic : T_Language
	{
		public Speaks_Chthonic() : base() { }

		public override string[] VanillaSpeakers => new string[]
		{
			VanillaAgents.Ghost,
			VanillaAgents.ShapeShifter,
			VanillaAgents.Vampire,
			VanillaAgents.Zombie
		};
		public override string[] LanguageNames => new string[] { LanguageSystem.Chthonic };

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Speaks_Chthonic>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to Ghosts, Shapeshifters, Vampires, Zombies, and anyone else with this trait."),
					[LanguageCode.Spanish] = "Este personaje ignora Dificultad al Hablar cuando interactua con Fantasmas, Cambiaformas, Vampiros, Zombies y todos quienes tengan este rasgo.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Speaks_Chthonic)),
					[LanguageCode.Spanish] = "Habla Cataclisan",
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