using RogueLibsCore;

namespace CCU.Traits.Active
{
    public class Behavior_Enforcer : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Behavior_Enforcer>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will enforce the law, like Cops."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.Active_EnforceLaws,
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
