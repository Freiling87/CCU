﻿using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Maimer : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.AlwaysCrit;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Maimer>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will use a Critter-Upper upon entering combat."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Maimer)),
                    
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
