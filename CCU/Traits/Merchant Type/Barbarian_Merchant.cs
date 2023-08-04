﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Barbarian_Merchant : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Axe, 3),
            new KeyValuePair<string, int>( vItem.BaconCheeseburger, 2),
            new KeyValuePair<string, int>( vItem.Beer, 2),
            new KeyValuePair<string, int>( vItem.BraceletofStrength, 1),
            new KeyValuePair<string, int>( vItem.CodPiece, 3),
            new KeyValuePair<string, int>( vItem.RagePoison, 1),
            new KeyValuePair<string, int>( vItem.Sword, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Barbarian_Merchant>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Ale, meat & a sharp blade. All that is best in life!"),
                    [LanguageCode.Spanish] = "Ale, carne y cuchilla, Este NPC vende todas las herramientas de un bravo barbaro, barba y musculatura no incluida.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Barbarian_Merchant)),
                    [LanguageCode.Spanish] = "Bar-Barbaro",

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
