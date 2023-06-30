﻿using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Slave_Shop : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Axe, 2),
            new KeyValuePair<string, int>( vItem.CodPiece, 4),
            new KeyValuePair<string, int>( vItem.DizzyGrenade, 2),
            new KeyValuePair<string, int>( vItem.FreezeRay, 1),
            new KeyValuePair<string, int>( vItem.Sword, 2),
            new KeyValuePair<string, int>( vItem.SlaveHelmetRemover, 2),
            new KeyValuePair<string, int>( vItem.Taser, 1),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Slave_Shop>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells gear for slavemasters."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Slave_Shop)),
                    
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
