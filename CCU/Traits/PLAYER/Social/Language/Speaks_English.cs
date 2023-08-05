using CCU.Localization;
using RogueLibsCore;
using System;

namespace CCU.Traits.Player.Language
{
    public class Speaks_English : T_Language
    {
        public override string[] VanillaSpeakers => new string[] 
        { 
        };
        public override string[] LanguageNames => new string[] { Language.English };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Speaks_English>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This is a back-end trait. You shouldn't see it."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = PlayerName(typeof(Speaks_English)),
                })
                .WithUnlock(new TraitUnlock_CCU
                {
                    Cancellations = { VanillaTraits.VocallyChallenged },
                    IsAvailable = false,
                    IsAvailableInCC = false,
                    IsPlayerTrait = false,
                    UnlockCost = 0,
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