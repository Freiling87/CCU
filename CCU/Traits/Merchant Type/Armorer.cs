using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Armorer : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.ArmorDurabilitySpray, 2),
            new KeyValuePair<string, int>( vItem.BulletproofVest, 3),
            new KeyValuePair<string, int>( vItem.CodPiece, 2),
            new KeyValuePair<string, int>( vItem.FireproofSuit, 1),
            new KeyValuePair<string, int>( vItem.GasMask, 1),
            new KeyValuePair<string, int>( vItem.HardHat, 1),
            new KeyValuePair<string, int>( vItem.SoldierHelmet, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Armorer>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells Armor & Armor accessories."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Armorer)),
                    
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
