using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Some_Bark : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.Loud;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Some_Bark>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will become Loud on entering combat. They probably still bite, though."),
                    [LanguageCode.Spanish] = "Este NPC se pone muy ruidoso al entrar en combate, porque actual como un perro es la cosa cool ahora no sabias?",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Some_Bark)),
                    [LanguageCode.Spanish] = "Ladrador",

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
