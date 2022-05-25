using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Sporting_Goods : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Sporting_Goods>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells sporting goods."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Sporting_Goods)),
                    [LanguageCode.Russian] = "",
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
