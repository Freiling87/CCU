using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
    public class Cop_Access : T_TraitGate
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Cop_Access>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Will not sell to the player if they don't have The Law."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Cop_Access)),
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
