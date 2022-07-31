using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Pay_Entrance_Fee : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.PayEntranceFee;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => VDetermineMoneyCost.Bribe;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Pay_Entrance_Fee>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character, if serving as Doorman, will allow access if paid with cash."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Pay_Entrance_Fee)),
                    
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
