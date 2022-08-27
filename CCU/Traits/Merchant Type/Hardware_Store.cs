using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Hardware_Store : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Axe, 2),
            new KeyValuePair<string, int>( vItem.Crowbar, 3),
            new KeyValuePair<string, int>( vItem.FireExtinguisher, 1),
            new KeyValuePair<string, int>( vItem.GasMask, 2),
            new KeyValuePair<string, int>( vItem.HardHat, 4),
            new KeyValuePair<string, int>( vItem.Knife, 2),
            new KeyValuePair<string, int>( vItem.Leafblower, 2),
            new KeyValuePair<string, int>( vItem.MeleeDurabilitySpray, 3),
            new KeyValuePair<string, int>( vItem.Sledgehammer, 2),
            new KeyValuePair<string, int>( vItem.WindowCutter, 1),
            new KeyValuePair<string, int>( vItem.Wrench, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Hardware_Store>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells tools."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Hardware_Store)),
                    
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
