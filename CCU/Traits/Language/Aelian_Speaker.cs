using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class Aelian_Speaker : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { VanillaAgents.Alien };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Aelian_Speaker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character speaks Aelian, the lingua franca of... aliens (No etymological relation). A bizarre language that sounds like a keyboard being mashed. Very concise.\n\n" + 
                    "Agent can bypass Vocally Challenged when speaking to vanilla Aliens, and anyone else with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Aelian_Speaker)),
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