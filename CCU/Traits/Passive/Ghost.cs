using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Ghost : T_CCU, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Ghost>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character is a spooky, spooky ghost."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Ghost)),
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
            agent.ghost = true;
        }
    }
}