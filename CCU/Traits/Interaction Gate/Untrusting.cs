using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction_Gate
{
    public class Untrusting : T_InteractionGate
    {
        public override int MinimumRelationship => 3;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Untrusting>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will not interact with anyone Neutral or worse."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Untrusting)),
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
