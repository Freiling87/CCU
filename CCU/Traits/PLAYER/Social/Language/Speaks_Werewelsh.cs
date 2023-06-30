using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Language
{
    public class Speaks_Werewelsh : T_Language
    {
        public override string[] VanillaSpeakers => new string[] 
        { 
            VanillaAgents.Werewolf, 
            VanillaAgents.WerewolfTransformed 
        };

		public override string[] LanguageNames => new string[] { Language.Werewelsh };

		[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_Werewelsh>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Lunatic raving interspersed with adorable doggy noises! Bypass Vocally Challenged with Werewolves (both forms), and anyone with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Speaks_Werewelsh)),
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