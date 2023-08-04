﻿using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Offer_Motivation : T_Interaction
    {
        public override bool AllowUntrusted => true;
        public override string ButtonText => VButtonText.OfferMotivation;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Offer_Motivation>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be given small items, and will become Friendly."),
                    [LanguageCode.Spanish] = "Este NPC se puede volver amistoso a costo de un item.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Offer_Motivation)),
                    [LanguageCode.Spanish] = "Ofrecer Motivacion",

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
