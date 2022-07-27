using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class Polyglot : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { VanillaAgents.Zombie };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Polyglot>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("When the moon is full, something awakens in this character. Something that knows no human scruple, nor vowel. An ancient curse that howls for blood and just so happens to speak Welsh. Weirder things have happened.\n\n" +
                    "Agent can bypass Vocally Challenged when speaking to (transformed) vanilla Werewolves, and anyone else with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Polyglot)),
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