using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Research_Materials : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.FreezeRay, 1),
            new KeyValuePair<string, int>( vItem.GhostGibber, 1),
            new KeyValuePair<string, int>( vItem.IdentifyWand, 2),
            new KeyValuePair<string, int>( vItem.ShrinkRay, 1),
            new KeyValuePair<string, int>( vItem.Syringe, 4),
            new KeyValuePair<string, int>( vItem.TranquilizerGun, 1),
            new KeyValuePair<string, int>( vItem.WaterPistol, 1),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Research_Materials>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells chemicals and experimental tools."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Research_Materials)),
                    
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
