using RogueLibsCore;
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
                    [LanguageCode.English] = String.Format("This character has a Shapeshifter firmly lodged up their ass.\n\nThat's their excuse, what's yours?!"),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Possessed)),
                })
                .WithUnlock(new TraitUnlock
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