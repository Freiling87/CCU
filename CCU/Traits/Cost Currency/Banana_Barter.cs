using RogueLibsCore;

namespace CCU.Traits.Cost_Currency
{
    public class Banana_Barter : T_CostCurrency
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Banana_Barter>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are converted to Bananas.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Banana_Barter)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DesignerName(typeof(Booze_Bargain)) },
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
