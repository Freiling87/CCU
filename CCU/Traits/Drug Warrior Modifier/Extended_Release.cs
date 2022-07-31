using RogueLibsCore;
using System;

namespace CCU.Traits.Drug_Warrior_Modifier
{
    public class Extended_Release : T_DrugWarriorModifier
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Extended_Release>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Drug warrior status effect lasts until end of combat."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Extended_Release)),
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
