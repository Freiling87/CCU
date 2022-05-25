using RogueLibsCore;

namespace CCU.Traits.Hire
{
    public class Decoy : T_Hire
    {
        public override string ButtonText => "CauseRuckus";

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Decoy>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to cause a distraction, or a Ruckus, if you will.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Decoy)),
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
