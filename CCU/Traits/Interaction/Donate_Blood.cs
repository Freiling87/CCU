using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Donate_Blood : T_Interaction
    {
        public override string ButtonText => VButtonText.DonateBlood;
        public override string InteractionCost =>
            GameController.gameController.challenges.Contains("LowHealth")
                ? VDetermineMoneyCost.GiveBloodLowHealth
                : VDetermineMoneyCost.GiveBlood;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Donate_Blood>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can administer blood bags for cash."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Donate_Blood)),
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
