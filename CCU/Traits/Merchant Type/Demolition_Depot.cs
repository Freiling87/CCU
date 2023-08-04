using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Demolition_Depot : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BombProcessor, 1),
            new KeyValuePair<string, int>( vItem.DoorDetonator, 3),
            new KeyValuePair<string, int>( vItem.Fireworks, 1),
            new KeyValuePair<string, int>( vItem.Grenade, 3),
            new KeyValuePair<string, int>( vItem.LandMine, 3),
            new KeyValuePair<string, int>( vItem.RemoteBomb, 3),
            new KeyValuePair<string, int>( vItem.RemoteBombTrigger, 1),
            new KeyValuePair<string, int>( vItem.RocketLauncher, 1),
            new KeyValuePair<string, int>( vItem.TimeBomb, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Demolition_Depot>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells explosives."),
                    [LanguageCode.Spanish] = "Este NPC vende explosivos.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Demolition_Depot)),
                    [LanguageCode.Spanish] = "Depot de Demolicion",

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
