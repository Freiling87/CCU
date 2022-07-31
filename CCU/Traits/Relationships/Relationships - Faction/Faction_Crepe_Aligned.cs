using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Rel_Faction
{
    public class Faction_Crepe_Aligned : T_Rel_Faction
    {
        public override int Faction => 0;
        public override Alignment FactionAlignment => throw new NotImplementedException();

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Faction_Crepe_Aligned>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This agent is aligned with Crepes and hostile to Blahds and Crepe Crushers. They provide an XP bonus when neutralized by Crepe Crushers."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Faction_Crepe_Aligned)),
                    
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
            agent.HasTrait<Faction_Crepe_Aligned>() || agent.agentName == VanillaAgents.GangsterCrepe
                ? VRelationship.Aligned
            : agent.HasTrait<Faction_Blahd_Aligned>() || agent.agentName == VanillaAgents.GangsterBlahd || agent.HasTrait(VanillaTraits.CrepeCrusher)
                ? VRelationship.Hostile
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
