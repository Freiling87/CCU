﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Behavior
{
    public class Accident_Prone : T_Behavior, ISetupAgentStats
    {
        public override bool LosCheck => false;
        public override string[] GrabItemCategories => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Accident_Prone>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format($"This character will not path around Crushers, Fire Spewers, Killer Plants, Laser Emitters & Sawblades.\n" +
                        "<color=green>{0}</color>: Will try to pick up armed traps.", LongishDocumentationName(typeof(Grab_Everything))), 
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Accident_Prone), "Accident-Prone"),
                    [LanguageCode.Spanish] = "Accidentado-Compulsivo",
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

        public void SetupAgentStats(Agent agent)
        {
            agent.dontStopForDanger = true;
        }
    }
}