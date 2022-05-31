using RogueLibsCore;

namespace CCU.Traits.Behavior
{
    public class Grab_Alcohol : T_Behavior
    {
        public override bool LosCheck => true;

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Grab_Alcohol>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will grab alcohol if they see it."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Grab_Alcohol)),
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
