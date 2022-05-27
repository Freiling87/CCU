using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class An_Inimitable_Bulk : T_DrugWarrior
    {
        public override string DrugEffect => VStatusEffect.Giant;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<An_Inimitable_Bulk>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character gains Giant when entering combat. They're angry because they can't pronounce the name of this trait out loud. And, I presume, you will not like them when they are angry."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(An_Inimitable_Bulk)),
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
