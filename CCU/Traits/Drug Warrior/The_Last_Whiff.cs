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
                    [LanguageCode.English] = String.Format("This character smokes a cigarette right when they get into a fight. How fuckin' cool are they??\n\nWow! Smoking is cool! Smoking is cool! Smoking is cool! Matt Dabrowski wants kids to take up smoking. Spread the word."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(The_Last_Whiff)),
                    
                })
                .WithUnlock(new TraitUnlock
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
