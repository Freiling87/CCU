using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class _Test_Inventory : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BaseballBat, 1),
            new KeyValuePair<string, int>( vItem.FoodProcessor, 1),
            new KeyValuePair<string, int>( vItem.GasMask, 1),
            new KeyValuePair<string, int>( vItem.HardHat, 1),
            new KeyValuePair<string, int>( vItem.MiniFridge, 1),
            new KeyValuePair<string, int>( vItem.Pistol, 1),
            new KeyValuePair<string, int>( vItem.WalkieTalkie, 1),
        };

        [RLSetup]
        public static void Setup()
        {
            if (Core.debugMode)
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
                        IsAvailableInCC = Core.debugMode,
                        UnlockCost = 0,
                    });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
