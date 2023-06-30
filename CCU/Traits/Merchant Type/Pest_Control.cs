using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Traits.Merchant_Type
{
    public class Pest_Control : T_MerchantType
    {
        public override List<KeyValuePair<string, int>> weightedItemPool => new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>( vItem.Antidote, 3),
            new KeyValuePair<string, int>( vItem.Beartrap, 3),
            new KeyValuePair<string, int>( vItem.CyanidePill, 3),
            new KeyValuePair<string, int>( vItem.Flamethrower, 1),
            new KeyValuePair<string, int>( vItem.GasMask, 3),
            new KeyValuePair<string, int>( vItem.KillProfiter, 1),
            new KeyValuePair<string, int>( vItem.Taser, 1),
            new KeyValuePair<string, int>( vItem.TranquilizerGun, 1),
        };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pest_Control>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells chemicals and tools for exterminating or subduing pests."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Pest_Control)),
                    
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
