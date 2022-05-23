using RogueLibsCore;

namespace CCU.Traits.Cost
{
    public class CostBanana : CustomTrait
    {
        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<CostBanana>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are converted to Bananas.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.CostBanana,
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
