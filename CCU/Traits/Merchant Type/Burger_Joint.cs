using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Burger_Joint : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Burger_Joint>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Burgers & beer, get them here!"),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Burger_Joint)),
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