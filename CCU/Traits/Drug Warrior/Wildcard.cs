using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior
{
    public class Wildcard : T_DrugWarrior
    {
        public override string DrugEffect => null;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Wildcard>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will use a random syringe upon entering combat."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Wildcard)),
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
