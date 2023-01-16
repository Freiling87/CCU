using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
    public class Firefighter_Aligned : T_Rel_Faction
    {
        public override int Faction => 0;
        public override Alignment FactionAlignment => throw new System.NotImplementedException();

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Firefighter_Aligned>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Agent is Aligned to Firefighters and anyone else with this trait. They are hostile to Arsonists.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Firefighter_Aligned)),
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

        public override string GetRelationshipTo(Agent agent) =>
            agent.agentName == VanillaAgents.Firefighter ||
            agent.HasTrait<Firefighter_Aligned>()
                ? VRelationship.Aligned
                : agent.arsonist || agent.activeArsonist
                    ? VRelationship.Hostile
                    : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
