﻿using RogueLibsCore;

namespace CCU.Traits.Hire_Duration
{
    public class Homesickly : T_HireDuration
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Homesickly>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Agent will never follow the player between levels.",
                    [LanguageCode.Spanish] = "Este NPC siempre te dejara al abandonar el piso.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Homesickly)),
                    [LanguageCode.Spanish] = "Sedentario",

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