using RogueLibsCore;
using System;

namespace CCU.Traits.Language
{
    public class Lang_Zonbi_Speaker : T_Language
    {
        public override string[] VanillaSpeakers => new string[] { VanillaAgents.Zombie };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Lang_Zonbi_Speaker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character speaks Lang Zonbi, basically Haitian Creole spoken by a vodou spirit through a rotting trachea. Much in common with Whalesong, but nowhere near as relaxing.\n\n" +
                    "Agent can bypass Vocally Challenged when speaking to vanilla Zombies, and anyone else with this trait."),
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Lang_Zonbi_Speaker)),
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