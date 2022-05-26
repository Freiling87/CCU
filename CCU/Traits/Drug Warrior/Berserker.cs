using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Berserker : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.Rage;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Berserker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character just totally sees red when he gets mad, bro. Like not even kidding bro, he loses control."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Berserker)),
                    [LanguageCode.Russian] = "",
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
