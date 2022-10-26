using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Language
{
    public class Polyglot : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { };

        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Polyglot>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Choose 2 languages to learn at start, and gain another language every 2 levels.\n\n" +
                    "Sharing a language with an NPC allows you to bypass Vocally Challenged."),
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
                    Recommendations = new List<string>() { "Speaks High Goryllian or a Translator instead. This trait will not be worth taking until the Language system is expanded, or unless you're playing Chunk Packs/Campaign that make heavy use of it." },
                    UnlockCost = 5,
                    Unlock = { isUpgrade = true }
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}