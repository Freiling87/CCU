using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Rel_Faction
{
    public class Faction_Firefighter_Aligned : T_Rel_Faction
    {
        public override int Faction => 0;
        public override Alignment FactionAlignment => throw new NotImplementedException();

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Faction_Firefighter_Aligned>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Aligned to Firefighters and anyone else with this trait.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Faction_Firefighter_Aligned), "Firefighter-Aligned"),
                    
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
            agent.agentName == VanillaAgents.Firefighter || agent.HasTrait<Faction_Firefighter_Aligned>()
                ? VRelationship.Aligned
            : agent.arsonist || agent.activeArsonist
                ? VRelationship.Hostile
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
