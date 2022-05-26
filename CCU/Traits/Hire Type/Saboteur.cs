using RogueLibsCore;

namespace CCU.Traits.Hire_Type
{
    public class Saboteur : T_Hire
    {
        public override string ButtonText => CJob.TamperSomething;

        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Saboteur>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to tamper with machinery and electronics.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Saboteur)),
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
