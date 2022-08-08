using RogueLibsCore;
using System;

namespace CCU.Traits.Behavior
{
    public class AccidentProne : T_Behavior, ISetupAgentStats
    {
        public override bool LosCheck => false;
        public override string[] GrabItemCategories => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<AccidentProne>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will not path around Crushers, Fire Spewers, and Sawblades.\n\n" +
                        "Working on the other traps. Maybe."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(AccidentProne), "Accident-Prone"),
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
            agent.dontStopForDanger = true;
        }
    }
}