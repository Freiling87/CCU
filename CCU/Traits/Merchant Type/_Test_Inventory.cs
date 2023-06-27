﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class _Test_Inventory : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.WaterPistol, 12),
        };

        [RLSetup]
        public static void Setup()
        {
            if (Core.developerEdition)
                PostProcess = RogueLibs.CreateCustomTrait<_Test_Inventory>()
                    .WithDescription(new CustomNameInfo
                    {
                        [LanguageCode.English] = String.Format("Developer test inventory"),
                    
                    })
                    .WithName(new CustomNameInfo
                    {
                        [LanguageCode.English] = DesignerName(typeof(_Test_Inventory)),
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
