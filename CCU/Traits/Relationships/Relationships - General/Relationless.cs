using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_General
{
    public class Relationless : T_Rel_General, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Relationless>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is always Neutral, like Butler Bot. What a lonely life.",
                    [LanguageCode.Spanish] = "Este NPC esta siempre neutral no importe lo que pase, como el Robomayordomo, Los tibios nunca van al cielo.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Relationless)),
                    [LanguageCode.Spanish] = "Siempre Neutral",
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

        public override string GetRelationshipTo(Agent agent) => VRelationship.Neutral;
        public override void OnAdded() { }
        public override void OnRemoved() { }

        public void SetupAgentStats(Agent agent)
        {
            agent.dontChangeRelationships = true;
        }
    }
}