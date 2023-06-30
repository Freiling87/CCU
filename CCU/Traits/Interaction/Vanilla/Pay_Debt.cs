using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Pay_Debt : T_Interaction
    {
        public override bool AllowUntrusted => true;
        public override string ButtonID => VButtonText.PayDebt;
        public override bool HideCostInButton => false;
        public override string DetermineMoneyCostID => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pay_Debt>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can accept debt payments.\n\n" +
                    "Note: If you want them to lend money as well, use {0} too.\n\nBypasses Untrusting traits.", LongishDocumentationName(typeof(Borrow_Money))),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Pay_Debt)),
                    
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
        public override void OnRemoved() { }
    }
}
