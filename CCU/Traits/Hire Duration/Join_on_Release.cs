using RogueLibsCore;

namespace CCU.Traits.Hire_Duration
{
    public class Join_on_Release : T_HireDuration
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Join_on_Release>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "Agent will join the player who frees them from Prison.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Join_on_Release)),
                    
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
