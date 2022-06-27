using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Banana_Boutique : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Banana_Boutique>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Yes, that is a banana in their pocket. But yes, they're still happy to see you."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Banana_Boutique)),
                    
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
