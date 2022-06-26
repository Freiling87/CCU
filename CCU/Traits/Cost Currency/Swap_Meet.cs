using RogueLibsCore;

namespace CCU.Traits.Cost_Currency
{
    public class Swap_Meet : T_CostCurrency
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Swap_Meet>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character doesn't accept money. Instead, you have to give them an item of equal or greater value than the cost of the good or service.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Swap_Meet)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Banana_Barter)) },
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
