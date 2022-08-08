using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class Speaks_ErSdtAdt : T_Language
    {
        public override string[] VanillaSpeakers => new string[] 
        { 
            VanillaAgents.Alien 
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_ErSdtAdt>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to Aliens, and anyone else with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Speaks_ErSdtAdt)),
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { },
                    CharacterCreationCost = 1,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    UnlockCost = 5,
                    Unlock = { upgrade = nameof(Polyglot) }
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}