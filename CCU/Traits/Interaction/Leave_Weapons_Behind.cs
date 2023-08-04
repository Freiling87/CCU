using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Leave_Weapons_Behind : T_Interaction
    {
        public override bool AllowUntrusted => true;
        public override string ButtonText => VButtonText.LeaveWeaponsBehind;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => null;

        // Should include FollowersLeaveWeaponsBehind

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Leave_Weapons_Behind>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be interacted with to drop all weapons in the Player's inventory."),
                    [LanguageCode.Spanish] = "Este NPC puede ser usado para tirar todas tus armas y las de tus seguidores al suelo.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Leave_Weapons_Behind)),
                    [LanguageCode.Spanish] = "Dejar Armas",

                })
                .WithUnlock(new TraitUnlock_CCU
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
