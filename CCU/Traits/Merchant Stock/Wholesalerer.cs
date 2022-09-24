using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
    public class Wholesalerer : T_MerchantStock
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Wholesalerer>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This agent sells items at 3x the normal quantity."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Wholesalerer)),
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
        public override void OnAddItem(ref InvItem invItem)
        {
            if (QuantityTypes.Contains(invItem.itemType))
                invItem.invItemCount *= 3;
        }
        public override void OnRemoved() { }
    }
}
