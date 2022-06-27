using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
    public class Faction_2_Aligned : T_Rel_Faction
    {
        public override int Faction => 2;
        public override Alignment FactionAlignment => Alignment.Aligned;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Faction_2_Aligned>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Aligned with all characters who share the trait.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Faction_2_Aligned)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Faction_2_Hostile)) },
                    CharacterCreationCost = 0,
                    IsAvailable = false,
                    IsAvailableInCC = Core.designerEdition,
                    UnlockCost = 0,
                });
        }

        public override string GetRelationshipTo(Agent agent) =>
            agent.HasTrait<Faction_2_Aligned>()
                ? VRelationship.Aligned
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
