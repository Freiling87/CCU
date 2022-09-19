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
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Colognier)),
                    
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
