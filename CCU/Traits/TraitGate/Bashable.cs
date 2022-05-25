using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
    public class Bashable : T_TraitGate
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Bashable>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This NPC is aligned with Blahds, and provides an XP bonus for Blahd Bashers."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Bashable)),
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
