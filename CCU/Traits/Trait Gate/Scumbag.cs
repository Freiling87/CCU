using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
    public class Scumbag : T_TraitGate, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Scumbag>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This Agent is a valid target for Scumbag Slaughterer, and will be hostile to them."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Scumbag)),
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

        public void SetupAgentStats(Agent agent)
        {
            agent.oma.mustBeGuilty = true;
            agent.oma._mustBeGuilty = true;
            //agent.isBigQuestObject = true;
            //agent.bigQuestType = "MechPilot";
            //agent.showBigQuestMarker = true;
            //agent.bigQuestMarkerAlwaysSeen = true;
            //agent.bigQuestMarkerMustBeDiscovered = true;
            //agent.SpawnNewMapMarker();
        }
    }
}
