using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Mining_Gear : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BombProcessor, 1),
            new KeyValuePair<string, int>( vItem.Fud, 3),
            new KeyValuePair<string, int>( vItem.GasMask, 1),
            new KeyValuePair<string, int>( vItem.HardHat, 4),
            new KeyValuePair<string, int>( vItem.RemoteBombTrigger, 1),
            new KeyValuePair<string, int>( vItem.Sledgehammer, 4),
            new KeyValuePair<string, int>( vItem.TimeBomb, 1),
            new KeyValuePair<string, int>( vItem.WallBypasser, 2),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Mining_Gear>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells mining gear. Now accepting payments not in company scrip!"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Mining_Gear)),
                    
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
