using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
    public class Fearless : T_Combat
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Fearless>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will never flee from combat."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Fearless)),
                    
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { DesignerName(typeof(Fearless)) },
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
