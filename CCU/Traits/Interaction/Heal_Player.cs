using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Heal_Player : T_Interaction
    {
        public override string ButtonText => VButtonText.Heal;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Heal_Player>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can heal, for money.\n\nThey may or may not keep it real."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName<Heal_Player>(),
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
