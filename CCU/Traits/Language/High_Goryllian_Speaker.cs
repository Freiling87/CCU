using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class High_Goryllian_Speaker : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { VanillaAgents.Gorilla };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<High_Goryllian_Speaker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character speaks High Goryllian, colloquially called Monkese. This is the lingua franca of hyperintelligent non-human primates. A lilting, delicate language with a heavy emphasis on respect and etiquette.\n\n" +
                    "Agent can bypass Vocally Challenged when speaking to vanilla Gorillas, and anyone else with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(High_Goryllian_Speaker)),
                    
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