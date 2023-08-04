﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Vision_Beams : T_CCU, ISetupAgentStats
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Vision_Beams>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character's vision is visually indicated, as with Cop Bot."),
                    [LanguageCode.Spanish] = "La vision de este personaje esta resaltada como si fueran un Robopoli.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Vision_Beams)),
                    [LanguageCode.Spanish] = "Rayo de Vision",
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
            agent.agentSecurityBeams.enabled = true;
        }
    }
}