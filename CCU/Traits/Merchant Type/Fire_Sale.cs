using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Fire_Sale : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Fire_Sale>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Sale! Big sale! Everything must go (up in flames)!"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Fire_Sale)),
                    
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
