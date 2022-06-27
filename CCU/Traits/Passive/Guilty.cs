using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Guilty : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Guilty>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character is designated Guilty for The Law or Scumbag Slaughterer."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Guilty)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Innocent)) },
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
