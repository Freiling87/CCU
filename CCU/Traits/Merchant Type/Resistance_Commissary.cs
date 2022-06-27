using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Resistance_Commissary : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Resistance_Commissary>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character distributes resources for the Resistance."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Resistance_Commissary)),
                    
                })
                .WithUnlock(new TraitUnlock
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
