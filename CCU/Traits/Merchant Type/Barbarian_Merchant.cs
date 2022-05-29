﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Barbarian_Merchant : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Barbarian_Merchant>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Ale, meat & a sharp blade. All that is best in life!"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Barbarian_Merchant)),
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
