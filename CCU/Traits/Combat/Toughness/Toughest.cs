﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
    internal class Toughest : T_Toughness
    {
        internal override int Toughness => 3;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Toughest>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character is more willing to enter and stay in combat, like Killer Robot or Zombie."),
                    [LanguageCode.Spanish] = "Este NPC esta SUPER DUPER DETERMINADO a entrar y seguir en combate! Como el Robot Asesino o un zombie!",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Toughest)),
                    [LanguageCode.Spanish] = "Rudisisisisimo",

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
