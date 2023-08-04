using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Colognier : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.NiceSmelling;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Colognier>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Louis XII's Cologniers were a legendary regiment known for smelling very pleasant right before they died quickly in combat. This character carries on that proud tradition."),
                    [LanguageCode.Spanish] = "Si te vas a morir es mejor que al menos no se nota el mal aroma, ser un cadaver no es escusa! por eso este NPC se Perfuma antes de que eso pase.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Colognier)),
                    [LanguageCode.Spanish] = "Coloñado",

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
