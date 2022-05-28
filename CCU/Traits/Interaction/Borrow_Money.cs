using RogueLibsCore;
using System;
using SORCE.Localization;

namespace CCU.Traits.Interaction
{
    public class Borrow_Money : T_Interaction
    {
        public override string ButtonText => VButtonText.BorrowMoney;
        public override string InteractionCost => null;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Borrow_Money>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can lend money."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Borrow_Money)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { "Borrow Money (Moocher)" },
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
