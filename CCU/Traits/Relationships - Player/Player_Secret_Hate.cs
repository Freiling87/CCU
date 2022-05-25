﻿using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
    public class Player_Secret_Hate : T_Rel_Player
    {
        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Player_Secret_Hate>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character will ambush Players.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Player_Secret_Hate)),
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
