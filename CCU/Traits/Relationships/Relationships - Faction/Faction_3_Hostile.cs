using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
    public class Faction_3_Hostile : T_Rel_Faction
    {
        public override int Faction => 3;
        public override Alignment FactionAlignment => Alignment.Hateful;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Faction_3_Hostile>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Hostile to all characters aligned with Faction 3.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Faction_3_Hostile)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Faction_3_Aligned)) },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }

        public override string GetRelationshipTo(Agent agent) =>
            agent.HasTrait<Faction_3_Aligned>()
                ? VRelationship.Hostile
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
