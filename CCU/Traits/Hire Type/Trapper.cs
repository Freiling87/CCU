using RogueLibsCore;

namespace CCU.Traits.Hire_Type
{
    public class Trapper : T_Hire
    {
        public override string ButtonText => CJob.DisarmTrap;

        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Trapper>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to disarm a trap.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Trapper)),
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
