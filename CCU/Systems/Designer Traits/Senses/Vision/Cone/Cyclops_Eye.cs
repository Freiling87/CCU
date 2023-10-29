using RogueLibsCore;

namespace CCU.Traits.Senses
{
    public class Cyclops_Eye : T_Senses, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Cyclops_Eye>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Sets vision cone to 10 degrees (vanilla value = 96).",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Cyclops_Eye)),
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
            // Vanilla 96f
            agent.LOSCone = 10f;
        }
    }
}