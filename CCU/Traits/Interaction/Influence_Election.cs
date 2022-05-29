using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Influence_Election : T_Interaction
    {
        public override string ButtonText => VButtonText.InfluenceElection;
        public override bool ExtraTextCostOnly => false;
        public override string InteractionCost => VDetermineMoneyCost.BribeElection;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Influence_Election>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be paid to sway the vote."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Influence_Election)),
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
