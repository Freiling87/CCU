using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Rel_Faction
{
    public class Faction_Blahd_Aligned : T_Rel_Faction
    {
        public override int Faction => 0;
        public override Alignment FactionAlignment => throw new NotImplementedException();

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Faction_Blahd_Aligned>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This agent is aligned with Blahds and hostile to Crepes and Blahd Bashers. They provide an XP bonus when neutralized by Blahd Bashers."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Faction_Blahd_Aligned)),
                    [LanguageCode.Russian] = "",
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
            agent.HasTrait<Faction_Blahd_Aligned>() || agent.agentName == VanillaAgents.GangsterBlahd
                ? VRelationship.Aligned
            : agent.HasTrait<Faction_Crepe_Aligned>() || agent.agentName == VanillaAgents.GangsterCrepe || agent.HasTrait(VanillaTraits.BlahdBasher)
                ? VRelationship.Hostile
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
