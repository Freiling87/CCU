using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Crusty : T_CCU, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Crusty>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character has the privileges of the upper class."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Crusty)),
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
            agent.upperCrusty = true;
        }
    }
}