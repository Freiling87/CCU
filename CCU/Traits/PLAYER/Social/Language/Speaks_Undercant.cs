using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Language
{
    public class Speaks_Undercant : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { VanillaAgents.Cannibal, VanillaAgents.Thief };
        public override string[] LanguageNames => new string[] { Language.Undercant }; 

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_Undercant>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("A coded dialect of subterranean outcasts, the Undercant is spoken in the Underdank and many Topside criminal networks. Bypass Vocally Challenged with Cannibals, Thieves, and anyone with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Speaks_Undercant)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { },
                    CharacterCreationCost = 1,
                    IsAvailable = false,
                    IsAvailableInCC = true,
                    IsPlayerTrait = true,
                    UnlockCost = 3,
                    //Unlock = { upgrade = nameof(Polyglot) }
                    Unlock =
                    {
                        categories = { VTraitCategory.Social },
                    }
                });
        }
        public override void OnAdded() { }
        public override void OnRemoved() { }
    }
}