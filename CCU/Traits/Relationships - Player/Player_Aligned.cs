﻿using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
    public class Player_Aligned : T_Rel_Player
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Player_Aligned>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Aligned to players.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Player_Aligned)),
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
