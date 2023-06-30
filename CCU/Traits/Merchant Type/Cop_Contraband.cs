﻿using CCU.Traits.Trait_Gate;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Cop_Contraband : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Syringe, 6),
            new KeyValuePair<string, int>( vItem.MusclyPill, 6),
            new KeyValuePair<string, int>( vItem.Sugar, 6),
            new KeyValuePair<string, int>( vItem.Pistol, 3),
            new KeyValuePair<string, int>( vItem.MachineGun, 3),
            new KeyValuePair<string, int>( vItem.Revolver, 3),
            new KeyValuePair<string, int>( vItem.Shotgun, 3),
            new KeyValuePair<string, int>( vItem.Taser, 3),
            new KeyValuePair<string, int>( vItem.Knife, 3),
            new KeyValuePair<string, int>( vItem.BaseballBat, 3),
            new KeyValuePair<string, int>( vItem.Crowbar, 3),
            new KeyValuePair<string, int>( vItem.BaseballBat, 3),
            new KeyValuePair<string, int>( vItem.Sledgehammer, 1),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Cop_Contraband>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Stuff confiscated from the City's Ne'er- and/or Rarely-Do-Wells.\n\n" + 
                        "<color=green>{0}</color> = Player needs The Law to access shop", LongishDocumentationName(typeof(Cop_Access))),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Cop_Contraband), "Cop (Contraband)"),
                    
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
