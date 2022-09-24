using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class Speaks_Binary : T_Language
    {
        public override string[] VanillaSpeakers => new string[] 
        { 
            "ButlerBot",
            VanillaAgents.CopBot, 
            VanillaAgents.Hacker,
            VanillaAgents.KillerRobot, 
            VanillaAgents.Robot 
        };


        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_Binary>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Agent can bypass Vocally Challenged when speaking to vanilla robots, Hackers, and anyone else with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Speaks_Binary)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = 1,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    UnlockCost = 5,
                    //Unlock = { upgrade = nameof(Polyglot) }
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}