using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Systems.Language
{
	public class Polyglot : T_Language
	{
		public override string[] VanillaSpeakers => new string[] { };
		public override string[] LanguageNames => LanguageSystem.AllLanguages.ToArray();

		[RLSetup]
		public static void Setup()
		{
			PostProcess_PlayerTrait = RogueLibs.CreateCustomTrait<Polyglot>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Speak all languages."),
					[LanguageCode.Spanish] = "Este personaje ignora Dificultad al Hablar como si tuviera un traductor.",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = PlayerName(typeof(Polyglot)),
					[LanguageCode.Spanish] = "Polyglot",
				})
				.WithUnlock(new TU_PlayerUnlock
				{
					Cancellations = { VanillaTraits.VocallyChallenged },
					CharacterCreationCost = 2,
					IsAvailable = false,
					IsAvailableInCC = true,
					
					UnlockCost = 5,
					Unlock =
					{
						categories = { VTraitCategory.Social },
                        // Can't lose or swap until you stash known languages that would be restored on removal
                        cantLose = true,
						cantSwap = true,
						isUpgrade = true
					}
				});
		}
		public override void OnAdded()
		{
			// Untested
			foreach (T_Language trait in Owner.GetTraits<T_Language>().Where(t => !(t is Polyglot)))
				if (!(trait is Polyglot))
				{
					Owner.statusEffects.RemoveTrait(nameof(trait));
				}
		}
	}
}