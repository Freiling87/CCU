using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Pacifist_Provisioner : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BananaPeel, 2),
            new KeyValuePair<string, int>( vItem.HologramBigfoot, 1),
            new KeyValuePair<string, int>( vItem.Hypnotizer, 2),
            new KeyValuePair<string, int>( vItem.HypnotizerII, 1),
            new KeyValuePair<string, int>( vItem.FirstAidKit, 2),
            new KeyValuePair<string, int>( vItem.ParalyzerTrap, 2),
            new KeyValuePair<string, int>( vItem.QuickEscapeTeleporter, 2),
            new KeyValuePair<string, int>( vItem.TranquilizerGun, 1),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pacifist_Provisioner>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells tools to avoid hurting people."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Pacifist_Provisioner)),
                    
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
