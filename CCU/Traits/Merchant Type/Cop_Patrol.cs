using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Cop_Patrol : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BulletproofVest, 2),
            new KeyValuePair<string, int>( vItem.Pistol, 2),
            new KeyValuePair<string, int>( vItem.PoliceBaton, 2),
            new KeyValuePair<string, int>( vItem.Revolver, 2),
            new KeyValuePair<string, int>( vItem.Shotgun, 2),
            new KeyValuePair<string, int>( vItem.Taser, 1),
            new KeyValuePair<string, int>( vItem.WalkieTalkie, 2),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Cop_Patrol>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells standard-issue patrolman's gear."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Cop_Patrol), "Cop (Patrol)"),
                    
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
