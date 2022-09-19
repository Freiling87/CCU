using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Fainting_Goat_Warrior : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.Tranquilized;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Fainting_Goat_Warrior>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will become Tranquilized upon entering combat. Shoulda done cardio."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Fainting_Goat_Warrior)),
                    
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
