using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
    public class Player_Submissive : T_Rel_Player
    {
        public override string Relationship => VRelationship.Submissive;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Player_Submissive>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Submissive to Players.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Player_Submissive)),
                    
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

        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
