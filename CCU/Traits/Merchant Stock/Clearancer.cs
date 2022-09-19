using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
    public class Clearancer : T_MerchantStock
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Clearancer>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Enables duplicate items to be drawn from inventory lists."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Clearancer)),
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
        public override void OnAddItem(ref InvItem invItem) { }
        public override void OnRemoved() { }
    }
}