using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Player.Language
{
    public class Polyglot : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Polyglot>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Speak all languages."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Polyglot)),
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