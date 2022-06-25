using CCU.Localization;
using RogueLibsCore;

namespace CCU.Traits.Rel_Player
{
    public class Player_Loyal : T_Rel_Player
    {
        public override string Relationship => VRelationship.Loyal;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Player_Loyal>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Loyal to Players.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Player_Loyal)),
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
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}
