using RogueLibsCore;

namespace CCU.Traits.Faction
{
    public class Faction_3_Hostile : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Faction_3_Hostile>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character is Hostile to all characters aligned with Faction 3.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.Relationships_Faction3Hostile,
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { CTrait.Relationships_Faction3Aligned },
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
