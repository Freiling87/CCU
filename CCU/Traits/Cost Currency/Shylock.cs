using RogueLibsCore;

namespace CCU.Traits.Cost_Currency
{
    public class Shylock : T_CostCurrency
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Shylock>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are converted to uh... bites of flesh.",
                    [LanguageCode.Spanish] = "Este NPC usa (Quotes)Pedacitos de Ti(Quotes) como moneda, solo pedacitos.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Shylock)),
                    [LanguageCode.Spanish] = "Filetero",

                })
                .WithUnlock(new TraitUnlock_CCU
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
