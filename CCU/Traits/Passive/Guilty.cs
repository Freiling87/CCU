using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Guilty : T_CCU, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Guilty>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character is designated Guilty for The Law."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Guilty)),
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DesignerName(typeof(Innocent)) },
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
        }
    }
}
