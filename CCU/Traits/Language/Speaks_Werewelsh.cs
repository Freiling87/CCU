using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class Speaks_Werewelsh : T_Language
    {
        public override string[] VanillaSpeakers => new string[] 
        { 
            VanillaAgents.Werewolf, 
            VanillaAgents.WerewolfTransformed 
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_Werewelsh>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to Werewolves (both forms), and anyone else with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Speaks_Werewelsh)),
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