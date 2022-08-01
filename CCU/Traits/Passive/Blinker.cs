using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
    public class Blinker : T_CCU
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Blinker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("When damaged, teleports to a random nearby spot."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Blinker)),
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { },
                    CharacterCreationCost = 5,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    UnlockCost = 5,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}