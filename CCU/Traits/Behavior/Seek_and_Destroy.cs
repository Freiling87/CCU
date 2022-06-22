using RogueLibsCore;

namespace CCU.Traits.Behavior
{
    public class Seek_and_Destroy : T_Behavior
    {
        public override bool LosCheck => false;
        public override string[] GrabItemCategories => null;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Seek_and_Destroy>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will follow and attack the player like the Killer Robot."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Seek_and_Destroy), "Seek & Destroy"),
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
