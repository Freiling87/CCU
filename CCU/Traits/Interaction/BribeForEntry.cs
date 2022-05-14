using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction
{
    public class BribeForEntry : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<BribeForEntry>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character, if serving as Doorman, will allow access if bribed with cash."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.BribeForEntry,
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
