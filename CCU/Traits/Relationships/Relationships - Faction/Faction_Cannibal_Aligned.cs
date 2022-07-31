using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Rel_Faction
{
    public class Faction_Cannibal_Aligned : T_Rel_Faction
    {
        public override int Faction => 0;
        public override Alignment FactionAlignment => throw new NotImplementedException();

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Faction_Cannibal_Aligned>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Aligned to Cannibals and anyone else with this trait. They're Hostile to Soldiers and anyone with Faction Soldier Aligned.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Faction_Cannibal_Aligned)),
                    
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

        public override string GetRelationshipTo(Agent agent) =>
            agent.HasTrait<Faction_Cannibal_Aligned>() || agent.agentName == VanillaAgents.Cannibal
                ? VRelationship.Aligned
            : agent.HasTrait<Faction_Soldier_Aligned>() || agent.agentName == VanillaAgents.Soldier
                ? VRelationship.Hostile
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
