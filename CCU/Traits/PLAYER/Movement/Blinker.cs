﻿using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Movement
{
    public class Blinker : T_PlayerTrait
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Blinker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("When damaged, instantly teleports to a random nearby spot."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Blinker)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = 5,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    IsPlayerTrait = true,
                    UnlockCost = 7,
                    Unlock =
                    {
                        categories = { VTraitCategory.Movement },
                    }
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}