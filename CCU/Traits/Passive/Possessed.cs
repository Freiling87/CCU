﻿using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Possessed : T_CCU, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Possessed>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character has an inhuman psychopath firmly lodged up their ass, controlling their every action. Except this one doesn't even pay them wages in return."),
                    [LanguageCode.Spanish] = "Este NPC tiene un Cambiaformas metido adentro, medio incomodo y es mejor no preguntar como va al baño.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Possessed)),
                    [LanguageCode.Spanish] = "Poseído",
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { DesignerName(typeof(Z_Infected)) },
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
            agent.secretShapeShifter = true;
            agent.oma.secretShapeShifter = true;
            agent.oma.mustBeGuilty = true;
            agent.agentHitboxScript.GetColorFromString("Red", "Eyes");
        }
    }
}