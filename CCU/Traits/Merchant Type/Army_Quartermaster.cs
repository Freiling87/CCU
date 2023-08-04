using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Army_Quartermaster : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.MachineGun, 3),
            new KeyValuePair<string, int>( vItem.Revolver, 3),
            new KeyValuePair<string, int>( vItem.Shotgun, 3),
            new KeyValuePair<string, int>( vItem.RocketLauncher, 3),
            new KeyValuePair<string, int>( vItem.Flamethrower, 3),
            new KeyValuePair<string, int>( vItem.Pistol, 3),
            new KeyValuePair<string, int>( vItem.AccuracyMod, 3),
            new KeyValuePair<string, int>( vItem.AmmoCapacityMod, 3),
            new KeyValuePair<string, int>( vItem.RateofFireMod, 3),
            new KeyValuePair<string, int>( vItem.Silencer, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Army_Quartermaster>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells military hardware."),
                    [LanguageCode.Spanish] = "Este NPC vende tecnología militar de alto rendimiento.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Army_Quartermaster)),
                    [LanguageCode.Spanish] = "Oficial Intendente",

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
