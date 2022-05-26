using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class The_Impermanent_Hunk : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.Strength;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<The_Impermanent_Hunk>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will gain a *temporary* Strength buff upon entering combat.\n\n<color=red>Remember, kids: Don't maintain? Lose your gains!</color>"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(The_Impermanent_Hunk)),
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
