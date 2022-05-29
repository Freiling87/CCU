using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Sure_I_Can : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.KillerThrower;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Sure_I_Can>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character has a can-do attitude about everything, including whether they can kill you with one hit of a thrown item for a limited time upon entering combat."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Sure_I_Can), "Sure I Can!"),
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
