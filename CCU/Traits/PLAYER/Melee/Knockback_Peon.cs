using CCU.Localization;
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
                    [LanguageCode.Spanish] = "Reduce el retroceso de tus golpes, facilitando combos rapidos.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Knockback_Peon)),
                    [LanguageCode.Spanish] = "Peon del Retroceso",
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { VanillaTraits.KnockbackKing, VanillaTraits.WallsWorstNightmare },
                    CharacterCreationCost = 5,
                    IsAvailable = true,
                    IsAvailableInCC = true,
                    IsPlayerTrait = true,
                    UnlockCost = 10,
                    Unlock =
                    {
                        categories = { VTraitCategory.Melee },
                    }
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}