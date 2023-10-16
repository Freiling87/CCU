using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Language
{
	public class Polyglot : T_Language
	{
		public override string[] VanillaSpeakers => new string[] { };
		public override string[] LanguageNames => new string[] { "Binary", "Chthonic", "English", "ErSdtAdt", "Foreign", "Goryllian", "Werewelsh" };
		public static string[] LanguagesStatic = new string[] { "Binary", "Chthonic", "English", "ErSdtAdt", "Foreign", "Goryllian", "Werewelsh" };

		[RLSetup]
		public static void Setup()
		{
			PostProcess = RogueLibs.CreateCustomTrait<Polyglot>()
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
				.WithUnlock(new TraitUnlock_CCU
				{
					Cancellations = { VanillaTraits.VocallyChallenged },
					CharacterCreationCost = 2,
					IsAvailable = false,
					IsAvailableInCC = true,
					IsPlayerTrait = true,
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
			foreach (T_Language trait in Owner.GetTraits<T_Language>())
				if (!(trait is Polyglot))
					Owner.statusEffects.RemoveTrait(trait.TextName);
		}
		public override void OnRemoved() { }
	}
}