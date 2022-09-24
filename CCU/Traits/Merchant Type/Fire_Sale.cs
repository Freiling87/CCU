using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Fire_Sale : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.CigaretteLighter, 2),
            new KeyValuePair<string, int>( vItem.Flamethrower, 2),
            new KeyValuePair<string, int>( vItem.FireproofSuit, 2),
            new KeyValuePair<string, int>( vItem.MolotovCocktail, 4),
            new KeyValuePair<string, int>( vItem.OilContainer, 2),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Fire_Sale>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Sale! Big sale! Everything must go (up in flames)!"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Fire_Sale)),
                    
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
