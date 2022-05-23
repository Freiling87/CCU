using RogueLibsCore;
using SORCE.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Leave_Weapons_Behind : T_Interaction
    {
        public override string ButtonText => VButtonText.LeaveWeaponsBehind;

        // Should include FollowersLeaveWeaponsBehind

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Leave_Weapons_Behind>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be interacted with to drop all weapons in the Player's inventory."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName<Leave_Weapons_Behind>(),
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
