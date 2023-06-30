using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Anthropophagie : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Axe, 2),
            new KeyValuePair<string, int>( vItem.Beartrap, 3),
            new KeyValuePair<string, int>( vItem.Beer, 3),
            new KeyValuePair<string, int>( vItem.Rock, 3),
            new KeyValuePair<string, int>( vItem.Whiskey, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Anthropophagie>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("A boutique for fine young cannibals."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Anthropophagie)),
                    
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
