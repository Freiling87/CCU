using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Suicide_Bomber_Normal : T_DrugWarrior
    {
        public override string DrugEffect => CStatusEffect.SuicideBomb_Normal;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Suicide_Bomber_Normal>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will die in a Normal explosion 15 seconds after starting combat."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Suicide_Bomber_Normal), "Suicide Bomber (Normal)"),
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
