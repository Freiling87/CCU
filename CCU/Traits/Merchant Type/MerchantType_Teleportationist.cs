﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class MerchantType_Teleportationist : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<MerchantType_Teleportationist>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells teleportation tools. Get them before they're gone!"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.MerchantType_Teleportationist,
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
