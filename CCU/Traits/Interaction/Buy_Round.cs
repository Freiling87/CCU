using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Buy_Round : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.BuyRound;
        public override bool ExtraTextCostOnly => false;
        public override string DetermineMoneyCost => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Buy_Round>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be paid to serve a round of drinks to everyone in the chunk."),
                    [LanguageCode.Spanish] = "Este NPC puede ser utilizado para hacer que todos en el edificio se vuelvan amistosos, recuerda que el precio aumenta con cada NPC presente.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Buy_Round)),
                    [LanguageCode.Spanish] = "Comprar Ronda",

                })
                .WithUnlock(new TraitUnlock_CCU
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
