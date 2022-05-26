using RogueLibsCore;

namespace CCU.Traits.Hire_Type
{
    public class Hacker : T_Hire
    {
        public override string ButtonText => "HackSomething";

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Hacker>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to hack something remotely.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Hacker)),
                    [LanguageCode.Russian] = "",
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
