using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
    public class Slavemaster_Hostile : T_Rel_Faction
    {
        public override int Faction => 0;
        public override Alignment FactionAlignment => throw new System.NotImplementedException();

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Slavemaster_Hostile>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Agent is Aligned to Soldiers and anyone else with this trait.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Slavemaster_Hostile)),
                    
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
            agent.agentName == VanillaAgents.Slavemaster
                ? VRelationship.Hostile
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
