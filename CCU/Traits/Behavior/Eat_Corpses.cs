﻿using RogueLibsCore;

namespace CCU.Traits.Behavior
{
    public class Eat_Corpses : T_Behavior
    {
        public override bool LosCheck => true;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Eat_Corpses>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will eat corpses like the Cannibal.\n\n<color=red>Requires:</color> {0}", vSpecialAbility.Cannibalize),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Eat_Corpses)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Pick_Pockets)), DisplayName(typeof(Suck_Blood)) },
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
