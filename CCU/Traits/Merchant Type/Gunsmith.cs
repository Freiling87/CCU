using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Gunsmith : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.AmmoProcessor, 2),
            new KeyValuePair<string, int>( vItem.AmmoStealer, 1),
            new KeyValuePair<string, int>( vItem.KillAmmunizer, 1),
            new KeyValuePair<string, int>( vItem.MachineGun, 2),
            new KeyValuePair<string, int>( vItem.Pistol, 3),
            new KeyValuePair<string, int>( vItem.Revolver, 3),
            new KeyValuePair<string, int>( vItem.Shotgun, 3),
            new KeyValuePair<string, int>( "WeaponMod", 4),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Gunsmith>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells guns and gun accessories."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Gunsmith)),
                    
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
