using RogueLibsCore;
using System;

namespace CCU.Traits.MerchantType
{
    public class MerchantType_ResistanceCommissary : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<MerchantType_ResistanceCommissary>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character distributes resources for the Resistance."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.MerchantType_ResistanceCommissary,
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
