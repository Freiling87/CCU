using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Language
{
    public class Speaks_High_Goryllian : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { VanillaAgents.Gorilla };
        public override string[] LanguageNames => new string[] { Language.Goryllian };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_High_Goryllian>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("A highly complex language with elaborate honorifics. Bypass Vocally Challenged with Gorillas, and anyone with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Speaks_High_Goryllian)),
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