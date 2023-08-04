using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Thief_Master : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> MerchantInventory => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.QuickEscapeTeleporter, 3),
            new KeyValuePair<string, int>( vItem.SkeletonKey, 2),
            new KeyValuePair<string, int>( vItem.SafeCrackingTool, 2),
            new KeyValuePair<string, int>( vItem.TranquilizerGun, 2),
            new KeyValuePair<string, int>( vItem.WallBypasser, 3),
            new KeyValuePair<string, int>( vItem.WindowCutter, 3),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Thief_Master>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells advanced intrusion tools."),
                    [LanguageCode.Spanish] = "Este NPC vende herramientas para ladrones.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Thief_Master)),
                    [LanguageCode.Spanish] = "Honorable Criminalista",

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
