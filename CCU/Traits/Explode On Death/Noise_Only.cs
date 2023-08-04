﻿using CCU.Localization;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Traits.Explode_On_Death
{
    public class Noise_Only : T_ExplodeOnDeath
    {
        public override string ExplosionType => VExplosionType.NoiseOnly;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Noise_Only>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("On death, this character makes a REALLY loud death rattle."),
                    [LanguageCode.Spanish] = "Al morir, este NPC se desintegra de manera muy ruidosa.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Noise_Only)),
                    [LanguageCode.Spanish] = "Solo Ruidos",

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