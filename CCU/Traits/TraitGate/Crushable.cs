using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
    public class Crushable : T_TraitGate
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Crushable>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This NPC is aligned with Crepes, and provides an XP bonus for Crepe Crushers."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Crushable)),
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
