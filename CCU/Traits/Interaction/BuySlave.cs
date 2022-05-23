using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class BuySlave : T_Interaction
    {
        public override string ButtonText => VButtonText.PurchaseSlave;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<BuySlave>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("If this character owns any Slaves, they will sell them."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.BuySlave,
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
