using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Stock
{
    public class Masterworkerer : T_MerchantStock
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Masterworkerer>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This agent sells items at 3x durability."),
                    [LanguageCode.Spanish] = "Los items que este NPC vende tienen un 3x de durabilidad.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Masterworkerer)),
                    [LanguageCode.Spanish] = "Fabricante Maestral",
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
            if (DurabilityTypes.Contains(invItem.itemType))
                invItem.invItemCount *= 3;
        }
        public override void OnRemoved() { }
    }
}
