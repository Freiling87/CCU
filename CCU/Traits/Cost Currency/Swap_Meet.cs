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
                    [LanguageCode.English] = "This character doesn't accept money. Instead, you have to give them an item of equal or greater value than the cost of the good or service. The ratio of acceptable items improves with vanilla bargaining traits.",
                    [LanguageCode.Spanish] = "Este NPC no acepta dinero y en su lugar acepta intercambiar items que tengas el mismo o mas valor que el item o servicio, Se puede negociar mejores precios con respectivos rasgos.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Swap_Meet)),
                    [LanguageCode.Spanish] = "De Trueque",

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
