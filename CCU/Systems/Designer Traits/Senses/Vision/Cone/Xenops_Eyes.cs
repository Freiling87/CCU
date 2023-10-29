using RogueLibsCore;

namespace CCU.Traits.Senses
{
    public class Xenops_Eyes : T_Senses, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Xenops_Eyes>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Sets vision cone to 360 degrees (vanilla value = 96).",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Xenops_Eyes)),
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
            agent.LOSCone = 360f;
        }
    }
}