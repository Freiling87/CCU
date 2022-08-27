using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Occultist : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BloodBag, 3),
            new KeyValuePair<string, int>( vItem.BooUrn, 1),
            new KeyValuePair<string, int>( vItem.Cologne, 1),
            new KeyValuePair<string, int>( vItem.CubeOfLampey, 1),
            new KeyValuePair<string, int>( vItem.GhostGibber, 1),
            new KeyValuePair<string, int>( vItem.Knife, 1),
            new KeyValuePair<string, int>( vItem.Necronomicon, 1),
            new KeyValuePair<string, int>( vItem.ResurrectionShampoo, 1),
            new KeyValuePair<string, int>( vItem.Sword, 1),        
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Occultist>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells occult-related items."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Occultist)),
                    
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
