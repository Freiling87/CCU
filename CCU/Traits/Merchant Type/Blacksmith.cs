using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Blacksmith : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Axe, 3),
            new KeyValuePair<string, int>( vItem.BraceletofStrength, 1),
            new KeyValuePair<string, int>( vItem.Knife, 3),
            new KeyValuePair<string, int>( vItem.MeleeDurabilitySpray, 3),
            new KeyValuePair<string, int>( vItem.Sledgehammer, 1),
            new KeyValuePair<string, int>( vItem.Sword, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Blacksmith>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells melee weapons and uh, related stuff."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Blacksmith), "Blacksmith, Ye Olde"),
                    
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
