using RogueLibsCore;
using System;

namespace CCU.Traits.Player
{
    public class Blinker : T_PlayerTrait
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Blinker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("When damaged, instantly teleports to a random nearby spot."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Blinker)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = 5,
                    IsAvailable = true,
                    IsAvailableInCC = true,
                    IsPlayerTrait = true,
                    UnlockCost = 7,
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}