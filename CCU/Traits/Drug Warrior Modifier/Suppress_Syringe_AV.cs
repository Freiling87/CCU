using CCU.Traits.Passive;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior_Modifier
{
    public class Suppress_Syringe_AV : T_DrugWarriorModifier
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Suppress_Syringe_AV>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Prevents Syringe text and sound when this agent uses a drug. N.b.: This only addresses the \"-Syringe\" text. To suppress status effect text, use {0}.", DesignerName(typeof(Suppress_Status_Text))),
                    [LanguageCode.Spanish] = "Oculta el texto cuando un NPCs usa una droga, solo funciona con el texto al usarse, el texto informativo sobre los NPCs solo se puede quitar con (Suprimir Texto de Status)",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Suppress_Syringe_AV)),
                    [LanguageCode.Spanish] = "Suprimir Aviso de Efecto",

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
