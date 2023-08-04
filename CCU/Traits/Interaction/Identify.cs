using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Identify : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.Identify;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => VDetermineMoneyCost.IdentifySyringe;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Identify>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can identify objects for money."),
                    [LanguageCode.Spanish] = "Este NPC puede identificar objectos por dinero",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Identify)),
                    [LanguageCode.Spanish] = "Identificar",

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
