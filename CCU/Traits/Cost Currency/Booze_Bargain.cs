using RogueLibsCore;

namespace CCU.Traits.Cost_Currency
{
    public class Booze_Bargain : T_CostCurrency
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Booze_Bargain>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are converted to Alcohol.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Booze_Bargain)),
                    
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { DesignerName(typeof(Banana_Barter)) },
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
