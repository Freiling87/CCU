using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior_Modifier
{
    public class Eternal_Release : T_DrugWarriorModifier
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Eternal_Release>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Drug warrior status effect lasts forever. Expectant mothers should not use this drug, as the effects on the unborn are as yet untested. But it might be cool."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Eternal_Release)),
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
