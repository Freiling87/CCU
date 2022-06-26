﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Vision_Beams : T_CCU
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Vision_Beams>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character's vision is visually indicated, as with Cop Bot."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Vision_Beams)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
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