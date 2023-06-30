using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
    public class Wholesaler : T_MerchantStock
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Wholesaler>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This agent sells items at 2x the normal quantity."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Wholesaler)),
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
        internal override void OnAddItem(ref InvItem invItem)
        {
            if (QuantityTypes.Contains(invItem.itemType))
                invItem.invItemCount *= 2;
        }
        public override void OnRemoved() { }
    }
}
