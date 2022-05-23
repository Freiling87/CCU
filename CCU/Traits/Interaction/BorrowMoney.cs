using RogueLibsCore;
using System;
using SORCE.Localization;

namespace CCU.Traits.Interaction
{
    public class BorrowMoney : T_Interaction
    {
        public override string ButtonText => VButtonText.BorrowMoney;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<BorrowMoney>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can lend money."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.BorrowMoney,
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
