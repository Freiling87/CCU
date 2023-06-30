using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Drug_Dealer : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Syringe, 6),
            new KeyValuePair<string, int>( vItem.Cigarettes, 3),
            new KeyValuePair<string, int>( vItem.MusclyPill, 3),
            new KeyValuePair<string, int>( vItem.ElectroPill, 3),
            new KeyValuePair<string, int>( vItem.CyanidePill, 3),
            new KeyValuePair<string, int>( vItem.CritterUpper, 3),
            new KeyValuePair<string, int>( vItem.Antidote, 3),
            new KeyValuePair<string, int>( vItem.Giantizer, 3),
            new KeyValuePair<string, int>( vItem.Shrinker, 3),
            new KeyValuePair<string, int>( vItem.RagePoison, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Drug_Dealer>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells drugs."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Drug_Dealer)),
                    
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
