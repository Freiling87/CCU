using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Indestructible : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Indestructible>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("I meant what I said! Can't be gibbed or cannibalized."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Indestructible)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
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