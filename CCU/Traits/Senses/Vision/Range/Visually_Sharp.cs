﻿using RogueLibsCore;

namespace CCU.Traits.Senses
{
    public class Visually_Sharp : T_Senses, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Visually_Sharp>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Line of Sight range set to 20.16 (Vanilla value = 13.44).",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Visually_Sharp)),
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
            // Vanilla 13.44f
            agent.LOSRange = 20.16f;
        }
    }
}