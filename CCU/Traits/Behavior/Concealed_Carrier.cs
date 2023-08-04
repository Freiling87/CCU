﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Behavior
{
    public class Concealed_Carrier : T_Behavior
    {
        public override bool LosCheck => false;
        public override string[] GrabItemCategories => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Concealed_Carrier>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This agent knows their rights, and declines your request for them to shut up about it. They'll hide their weapon when not in combat."),
                    [LanguageCode.Spanish] = "Este NPC habla tranquilo pero trae fierro, Las armas que lleven no se prodran ver hasta que entren en combate.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Concealed_Carrier)),
                    [LanguageCode.Spanish] = "Portador Oculto",
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