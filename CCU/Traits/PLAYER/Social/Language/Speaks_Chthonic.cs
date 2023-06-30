using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Language
{
    public class Speaks_Chthonic : T_Language
    {
        public override string[] VanillaSpeakers => new string[] 
        { 
            VanillaAgents.Ghost, 
            VanillaAgents.ShapeShifter, 
            VanillaAgents.Vampire,
            VanillaAgents.Zombie 
        };
        public override string[] LanguageNames => new string[] { Language.Chthonic };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_Chthonic>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("If I even describe this language I could summon something, and I don't have insurance... so figure it out. Bypass Vocally Challenged with Ghosts, Shapeshifters, Vampires, Zombies, and anyone with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Speaks_Chthonic)),
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