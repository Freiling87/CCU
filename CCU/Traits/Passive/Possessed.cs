﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Possessed : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Possessed>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character has a Shapeshifter firmly lodged up their ass.\n\nThat's their excuse, what's yours?!"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Possessed)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Z_Infected)) },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}