using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction_Gate
{
    public class Insularest : T_InteractionGate
    {
        public override int MinimumRelationship => 5;

        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Insularest>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will not interact with anyone who has a relationship with their faction at Loyal or worse."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Insularest)),
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
