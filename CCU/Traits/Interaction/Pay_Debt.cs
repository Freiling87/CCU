using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Pay_Debt : T_Interaction
    {
        public override bool AllowUntrusted => true;
        public override string ButtonText => VButtonText.PayDebt;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pay_Debt>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can accept debt payments.\n\n" + 
                    "Note: If you want them to lend money as well, use {0} too.", LongishDocumentationName(typeof(Borrow_Money))),
                    [LanguageCode.Spanish] = "Este NPC te permite pagar tus deudas, con dinero no con los otros 2 metodos.\n\n" +
                    "Nota: Para poder utilizarlos tambien para pedir dinero prestado necesitas {0}."
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Pay_Debt)),
                    [LanguageCode.Spanish] = "Pagar Deuda",

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
