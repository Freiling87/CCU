using RogueLibsCore;

namespace CCU.Traits.Cost
{
    public class CostAlcohol : CustomTrait
    {
        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<CostAlcohol>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are converted to Alcohol.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.CostAlcohol,
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
