using RogueLibsCore;
using System;

namespace CCU.Traits.Behavior
{
    public class AccidentProne : T_Behavior
    {
        public override bool LosCheck => false;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<AccidentProne>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will not path around Crushers, Fire Spewers, and Sawblades.\n\n" +
                        "Working on the other traps. Maybe."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(AccidentProne), "Accident-Prone"),
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