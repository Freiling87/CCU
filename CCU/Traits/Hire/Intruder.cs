using RogueLibsCore;

namespace CCU.Traits.Hire
{
    public class Intruder : T_Hire
    {
        public override string ButtonText => "LockpickDoor";

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Intruder>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to break into windows or doors.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Intruder)),
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
