using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Outdoor_Outfitter : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Axe, 1),
            new KeyValuePair<string, int>( vItem.Beer, 1),
            new KeyValuePair<string, int>( vItem.CigaretteLighter, 1),
            new KeyValuePair<string, int>( vItem.Beartrap, 1),
            new KeyValuePair<string, int>( vItem.Fireworks, 1),
            new KeyValuePair<string, int>( vItem.FirstAidKit, 1),
            new KeyValuePair<string, int>( vItem.OilContainer, 1),
            new KeyValuePair<string, int>( vItem.ParalyzerTrap, 1),
            new KeyValuePair<string, int>( vItem.Revolver, 1),
            new KeyValuePair<string, int>( vItem.Shotgun, 1),
            new KeyValuePair<string, int>( vItem.Whiskey, 1),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Outdoor_Outfitter>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells traps & wilderness gear. And fireworks, because yee-haw!"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Outdoor_Outfitter)),
                    
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
