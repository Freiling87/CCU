using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
    public class Scumbag : T_TraitGate
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Scumbag>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This Agent is a valid target for Scumbag Slaughterer, and will be hostile to them."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Scumbag)),
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
