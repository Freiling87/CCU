using RogueLibsCore;
using System;
using SORCE.Localization;

namespace CCU.Traits.Interaction
{
    public class AdministerBloodBag : T_Interaction
    {
        public override string ButtonText => VButtonText.AdministerBloodBag;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<AdministerBloodBag>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can give you a blood bag, at the cost of 20 HP."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.AdministerBloodBag,
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
