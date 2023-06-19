using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Melee_Combat
{
    public class Knockback_Peon : T_PlayerTrait
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Knockback_Peon>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Cause people you hit to be knocked back less. Makes followup attacks easier."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Knockback_Peon)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { VanillaTraits.KnockbackKing, VanillaTraits.WallsWorstNightmare },
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