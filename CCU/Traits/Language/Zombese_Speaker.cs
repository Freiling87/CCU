using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class Zombese_Speaker : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { VanillaAgents.Zombie };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Zombese_Speaker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character speaks Zombese, the lingua franca of persons of additional lifespan. Speech requires no articulation of the mouth or tongue. Just a stream of raw brain-data oozing out from Broca's Area. Not as relaxing as whalesong.\n\n" +
                    "Agent can bypass Vocally Challenged when speaking to vanilla Zombies, and anyone else with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Zombese_Speaker)),
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