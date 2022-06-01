using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Give_Blood : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.DonateBlood;
        public override bool ExtraTextCostOnly => true;
        public override string InteractionCost =>
            GameController.gameController.challenges.Contains("LowHealth")
                ? VDetermineMoneyCost.GiveBloodLowHealth
                : VDetermineMoneyCost.GiveBlood;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Give_Blood>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can help donate blood for cash."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Give_Blood)),
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
