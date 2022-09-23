using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Resistance_Commissary : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.FreeItemVoucher, 6),
            new KeyValuePair<string, int>( vItem.HiringVoucher, 6),
            new KeyValuePair<string, int>( vItem.QuickEscapeTeleporter, 2),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Resistance_Commissary>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character distributes resources for the Resistance."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Resistance_Commissary)),
                    
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
