using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Villains_Vault : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.CyanidePill, 2),
            new KeyValuePair<string, int>( vItem.Explodevice, 2),
            new KeyValuePair<string, int>( vItem.Giantizer, 2),
            new KeyValuePair<string, int>( vItem.MonkeyBarrel, 2),
            new KeyValuePair<string, int>( vItem.Necronomicon, 2),
            new KeyValuePair<string, int>( vItem.RagePoison, 2),
            new KeyValuePair<string, int>( vItem.TimeBomb, 2),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Villains_Vault>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells things that are dastardly to use. If they have a mustache, they will twirl it deviously."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Villains_Vault), "Villains' Vault"),
                    
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
