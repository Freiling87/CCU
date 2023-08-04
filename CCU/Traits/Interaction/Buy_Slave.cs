using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Buy_Slave : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.PurchaseSlave;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => null; // Determined in code

        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Buy_Slave>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("If this character owns any Slaves, they will sell them."),
                    [LanguageCode.Spanish] = "Este NPC puede vender los esclavos presentes en el edificio.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Buy_Slave)),
                    [LanguageCode.Spanish] = "Comprar Esclavo",

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
