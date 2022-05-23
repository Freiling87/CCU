using RogueLibsCore;

namespace CCU.Traits.Relationships
{
    public class Hostile_To_Cannibal : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Hostile_To_Cannibal>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Hostile to Cannibals.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.Relationships_HostileToCannibals,
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
