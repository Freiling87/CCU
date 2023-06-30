using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Tech_Mart : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Blindenizer, 2),
            new KeyValuePair<string, int>( vItem.EMPGrenade, 4),
            new KeyValuePair<string, int>( vItem.Explodevice, 1),
            new KeyValuePair<string, int>( vItem.GhostGibber, 2),
            new KeyValuePair<string, int>( vItem.HackingTool, 4),
            new KeyValuePair<string, int>( vItem.IdentifyWand, 2),
            new KeyValuePair<string, int>( vItem.PortableSellOMatic, 1),
            new KeyValuePair<string, int>( vItem.MemoryMutilator, 2),
            new KeyValuePair<string, int>( vItem.SafeBuster, 2),
            new KeyValuePair<string, int>( vItem.Translator, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Tech_Mart>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells high-tech gear."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Tech_Mart)),
                    
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
