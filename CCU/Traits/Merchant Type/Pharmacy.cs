using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Pharmacy : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Antidote, 3),
            new KeyValuePair<string, int>( vItem.FirstAidKit, 3),
            new KeyValuePair<string, int>( vItem.IdentifyWand, 3),
            new KeyValuePair<string, int>( vItem.MusclyPill, 3),
            new KeyValuePair<string, int>( vItem.ResurrectionShampoo, 1),
            new KeyValuePair<string, int>( vItem.Syringe, 6),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pharmacy>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells medicine."),
                    [LanguageCode.Spanish] = "Este NPC vende medicamentos.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Pharmacy)),
                    [LanguageCode.Spanish] = "Farmacia",

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
