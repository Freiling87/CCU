using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Pawn_Shop : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BaseballBat, 1),
            new KeyValuePair<string, int>( vItem.BoomBox, 2),
            new KeyValuePair<string, int>( vItem.Crowbar, 1),
            new KeyValuePair<string, int>( vItem.FoodProcessor, 1),
            new KeyValuePair<string, int>( vItem.FriendPhone, 1),
            new KeyValuePair<string, int>( vItem.GasMask, 1),
            new KeyValuePair<string, int>( vItem.HackingTool, 1),
            new KeyValuePair<string, int>( vItem.HardHat, 1),
            new KeyValuePair<string, int>( vItem.Knife, 1),
            new KeyValuePair<string, int>( vItem.Leafblower, 2),
            new KeyValuePair<string, int>( vItem.MiniFridge, 1),
            new KeyValuePair<string, int>( vItem.Pistol, 2),
            new KeyValuePair<string, int>( vItem.Revolver, 2),
            new KeyValuePair<string, int>( vItem.Shotgun, 1),
            new KeyValuePair<string, int>( vItem.Taser, 1),
            new KeyValuePair<string, int>( vItem.WalkieTalkie, 1),
            new KeyValuePair<string, int>( vItem.Wrench, 2),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pawn_Shop>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells various goods of low value."),
                    [LanguageCode.Spanish] = "Este NPC vende varios bienes baratos.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Pawn_Shop)),
                    [LanguageCode.Spanish] = "Casa de Empeño",

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
