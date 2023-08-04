using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Extortable : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Extortable>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be extorted for income, if the player has the Extortionist trait."),
                    [LanguageCode.Spanish] = "Este NPC puede ser extorcionado si el jugador tiene el rasgo Extorsionador.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Extortable)),
                    [LanguageCode.Spanish] = "Extorsionable",

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
