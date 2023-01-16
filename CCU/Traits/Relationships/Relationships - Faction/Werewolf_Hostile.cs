using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Faction
{
    public class Werewolf_Hostile : T_Rel_Faction
    {
        public override int Faction => 0;
        public override Alignment FactionAlignment => throw new System.NotImplementedException();

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Werewolf_Hostile>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Hostile to transformed Werewolves. If they have Werewolf A-Were-Ness, they are hostile to the non-transformed as well.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Werewolf_Hostile)),
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
            agent.agentName == VanillaAgents.WerewolfTransformed
                ? VRelationship.Hostile
                : null;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
