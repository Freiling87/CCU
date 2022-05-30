using RogueLibsCore;
using System;
using SORCE.Localization;

namespace CCU.Traits.Interaction
{
    public class Borrow_Money_Moocher : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.BorrowMoney;
        public override bool ExtraTextCostOnly => false;
        public override string InteractionCost => null;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Borrow_Money_Moocher>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can lend money, if the player has the Moocher trait."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Borrow_Money_Moocher), ("Borrow Money (Moocher)")),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Borrow_Money)) },
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
