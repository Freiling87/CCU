using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Assassineer : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Blindenizer, 1),
            new KeyValuePair<string, int>( vItem.CardboardBox, 1),
            new KeyValuePair<string, int>( vItem.EarWarpWhistle, 1),
            new KeyValuePair<string, int>( vItem.KillProfiter, 1),
            new KeyValuePair<string, int>( vItem.Shuriken, 4),
            new KeyValuePair<string, int>( vItem.Silencer, 1),
            new KeyValuePair<string, int>( vItem.Sword, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Assassineer>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells anything an assassin could need."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Assassineer)),
                    
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
