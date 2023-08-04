using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Riot_Inc : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.BaseballBat, 3),
            new KeyValuePair<string, int>( vItem.CigaretteLighter, 1),
            new KeyValuePair<string, int>( vItem.CubeOfLampey, 1),
            new KeyValuePair<string, int>( vItem.Knife, 3),
            new KeyValuePair<string, int>( vItem.MolotovCocktail, 4),
            new KeyValuePair<string, int>( vItem.OilContainer, 2),
            new KeyValuePair<string, int>( vItem.RagePoison, 1),
            new KeyValuePair<string, int>( vItem.Rock, 4),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Riot_Inc>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells anything a rioter could want."),
                    [LanguageCode.Spanish] = "Este NPC vende herramientas de destrucion livianas perfectas para la revolucion de tu ideologia completamente apta!",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Riot_Inc), "Riot, Inc."),
                    [LanguageCode.Spanish] = "Alboroto, Disturbios y Asociados",

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
