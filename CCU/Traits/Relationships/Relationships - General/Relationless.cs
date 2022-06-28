using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_General
{
    public class Relationless : T_Rel_General
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Relationless>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is always Neutral, like Butler Bot. What a lonely life.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Relationless)),
                    
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
            VRelationship.Neutral;
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}