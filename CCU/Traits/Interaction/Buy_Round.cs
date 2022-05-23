using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Buy_Round : T_Interaction
    {
        public override string ButtonText => VButtonText.BuyRound;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Buy_Round>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be paid to serve a round of drinks to everyone in the chunk."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName<Buy_Round>(),
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
