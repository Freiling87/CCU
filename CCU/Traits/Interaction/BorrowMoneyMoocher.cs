using RogueLibsCore;
using System;
using SORCE.Localization;

namespace CCU.Traits.Interaction
{
    public class BorrowMoneyMoocher : T_Interaction
    {
        public override string ButtonText => VButtonText.BorrowMoney;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<BorrowMoneyMoocher>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can lend money, if the player has the Moocher trait."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.Moochable,
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
