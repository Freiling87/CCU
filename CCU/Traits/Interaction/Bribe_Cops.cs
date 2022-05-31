using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Bribe_Cops : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.BribeCops;
        public override bool ExtraTextCostOnly => false;
        public override string InteractionCost => VDetermineMoneyCost.BribeCops;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Bribe_Cops>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will accept cash to bribe law enforcement."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Bribe_Cops)),
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
