using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Hack
{
    public class Go_Haywire : T_Hack
    {
        public override string ButtonText => VButtonText.Hack_Haywire;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Go_Haywire>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be hacked to go Haywire.\n\n" + 
                    "<color=red>Requires:</color> Electronic"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName<Go_Haywire>(),
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