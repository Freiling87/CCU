using RogueLibsCore;
using System;
using CCU.Localization;

namespace CCU.Traits.Interaction
{
    public class Borrow_Money : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.BorrowMoney;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Borrow_Money>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can lend money."),
                    [LanguageCode.Spanish] = "Este NPC puede prestarte dinero.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Borrow_Money)),
                    [LanguageCode.Spanish] = "Hacer un préstamo",

                })
                .WithUnlock(new TraitUnlock_CCU
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
