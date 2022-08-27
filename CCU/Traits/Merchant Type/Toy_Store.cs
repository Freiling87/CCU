using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Toy_Store : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BananaPeel, 4),
            new KeyValuePair<string, int>( vItem.Blindenizer, 1),
            new KeyValuePair<string, int>( vItem.EarWarpWhistle, 1),
            new KeyValuePair<string, int>( vItem.HologramBigfoot, 1),
            new KeyValuePair<string, int>( vItem.Shuriken, 2),
            new KeyValuePair<string, int>( vItem.WalkieTalkie, 2),
            new KeyValuePair<string, int>( vItem.WaterPistol, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Toy_Store>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells toys. Not adult toys."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Toy_Store)),
                    
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
