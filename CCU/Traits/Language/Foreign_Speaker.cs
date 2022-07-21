using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class Foreign_Speaker : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { VanillaAgents.Zombie };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Foreign_Speaker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Americans used to think that all foreigners spoke the same language, Foreign. In a sick twist of fate, that's now the truth. That's just how linguistics works, man.\n\n" +
                    "Agent can bypass Vocally Challenged when speaking to anyone else with this trait. This doesn't apply to any vanilla NPCs."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Foreign_Speaker)),
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