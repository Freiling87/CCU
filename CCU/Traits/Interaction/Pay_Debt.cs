using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Pay_Debt : T_Interaction
    {
        public override string ButtonText => VButtonText.PayDebt;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Pay_Debt>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can accept debt payments.\n\n" + 
                    "Note: If you want them to lend money as well, use {0} too.", CTrait.BorrowMoney),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName<Pay_Debt>(),
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
