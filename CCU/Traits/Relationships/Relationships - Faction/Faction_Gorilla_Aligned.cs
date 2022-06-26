using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Rel_Faction
{
    public class Faction_Gorilla_Aligned : T_Rel_Faction
    {
        public override int Faction => 0;
        public override Alignment FactionAlignment => throw new NotImplementedException();

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Faction_Gorilla_Aligned>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This agent is aligned with Gorillas and anyone else with this trait. They are hostile to Scientists and Specists. They provide an XP bonus when neutralized by Specists."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Faction_Gorilla_Aligned)),
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
            agent.agentName == VanillaAgents.Scientist || agent.HasTrait(VanillaTraits.Specist)
                ? VRelationship.Hostile
            : agent.agentName == VanillaAgents.Gorilla || agent.HasTrait<Faction_Gorilla_Aligned>()
                ? VRelationship.Aligned
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
