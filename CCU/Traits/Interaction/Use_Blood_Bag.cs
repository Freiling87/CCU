﻿using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Use_Blood_Bag : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.UseBloodBag;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Use_Blood_Bag>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can help the player use a Blood Bag in their inventory."),
                    [LanguageCode.Spanish] = "Este NPC te permite usar bolsas de sangre.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Use_Blood_Bag)),
                    [LanguageCode.Spanish] = "Usar Bolsa de Sangre",

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
