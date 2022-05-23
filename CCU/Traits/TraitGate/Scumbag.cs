using RogueLibsCore;
using System;

namespace CCU.Traits.TraitGate
{
    public class Scumbag : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Scumbag>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This Agent is a valid target for Scumbag Slaughterer."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.TraitGate_Scumbag,
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
