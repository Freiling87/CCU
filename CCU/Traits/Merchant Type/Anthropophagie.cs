using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Anthropophagie : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Anthropophagie>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("A boutique for fine young cannibals."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Anthropophagie)),
                    
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
