using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Monke_Mart : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Banana, 12),
            new KeyValuePair<string, int>( vItem.Lockpick, 3),
            new KeyValuePair<string, int>( vItem.MonkeyBarrel, 3),
            new KeyValuePair<string, int>( vItem.HologramBigfoot, 3),
            new KeyValuePair<string, int>( vItem.Translator, 3),
            new KeyValuePair<string, int>( vItem.Wrench, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Monke_Mart>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells stuff for monke, gorgia, you name it.\n\nThanks for coming, please return (to monke)!"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Monke_Mart)),
                    
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
