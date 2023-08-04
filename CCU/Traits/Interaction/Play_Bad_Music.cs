﻿using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Play_Bad_Music : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => null;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => null; // Determined in code

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Play_Bad_Music>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be paid to play a bad song, clearing the chunk out. They can also play Mayor Evidence on Turntables."),
                    [LanguageCode.Spanish] = "Este NPC puede ser usado para que los NPCs que no sean dueños dejen el edificio. Tambien pueden ser usados para poner el disco de evidencia",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Play_Bad_Music)),
                    [LanguageCode.Spanish] = "Tocar Musica Mala",

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
