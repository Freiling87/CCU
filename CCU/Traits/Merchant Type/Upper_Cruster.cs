using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Upper_Cruster : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Cologne, 3),
            new KeyValuePair<string, int>( vItem.Cocktail, 3),
            new KeyValuePair<string, int>( vItem.ResurrectionShampoo, 1),
            new KeyValuePair<string, int>( vItem.BraceletofStrength, 1),
            new KeyValuePair<string, int>( vItem.FriendPhone, 1),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Upper_Cruster>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells fancy shit by top brands made by slaves. This is the vanilla Upper Cruster's mall inventory."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Upper_Cruster)),
                    
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
