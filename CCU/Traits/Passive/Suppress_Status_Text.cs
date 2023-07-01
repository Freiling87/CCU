﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Suppress_Status_Text : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Suppress_Status_Text>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Prevents Status Effect text popup when the agent receives a new status effect."),

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Suppress_Status_Text)),

                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
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