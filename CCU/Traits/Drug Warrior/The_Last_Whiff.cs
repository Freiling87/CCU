using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class The_Last_Whiff : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.KillerThrower;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<The_Last_Whiff>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character smokes a cigarette right when they get into a fight. How fuckin' cool are they??"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(The_Last_Whiff)),
                    
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
