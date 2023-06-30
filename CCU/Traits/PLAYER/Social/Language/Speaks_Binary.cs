﻿using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Language
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
        public override string[] LanguageNames => new string[] { Language.Binary };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_Binary>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Literal actual beeping and booping, literally actually. Bypass Vocally Challenged with Robots, Hackers, and anyone with this trait."),
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
                    IsPlayerTrait = true,
                    UnlockCost = 3,
                    //Unlock = { upgrade = nameof(Polyglot) }
                    Unlock =
                    {
                        categories = { VTraitCategory.Social },
                    }
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}