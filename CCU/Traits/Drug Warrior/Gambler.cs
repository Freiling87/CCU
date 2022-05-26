using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Gambler : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.FeelingLucky;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Gambler>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("You feel lucky, punk? I do."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Gambler)),
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
