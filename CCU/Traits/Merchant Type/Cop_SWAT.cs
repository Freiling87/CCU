using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Cop_SWAT : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BulletproofVest, 3),
            new KeyValuePair<string, int>( vItem.DizzyGrenade, 3),
            new KeyValuePair<string, int>( vItem.DoorDetonator, 3),
            new KeyValuePair<string, int>( vItem.GasMask, 2),
            new KeyValuePair<string, int>( vItem.WindowCutter, 2),
            new KeyValuePair<string, int>( vItem.MachineGun, 3),
            new KeyValuePair<string, int>( vItem.Shotgun, 3),
            new KeyValuePair<string, int>( vItem.SkeletonKey, 1),
            new KeyValuePair<string, int>( "WeaponMod", 2),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Cop_SWAT>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells gear for Doorkickers."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Cop_SWAT), "Cop (SWAT)"),
                    
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
