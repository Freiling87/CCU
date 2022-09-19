﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction_Gate
{
    public class Untrustinger : T_InteractionGate
    {
        public override int MinimumRelationship => 4;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Untrustinger>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will only interact with Loyal or better.\n\n" +
                    "Exceptions: \n" +
                    "- Leave Weapons Behind\n" +
                    "- Offer Motivation\n" +
                    "- Pay Debt"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Untrustinger)),
                    
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
