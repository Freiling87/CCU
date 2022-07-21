using RogueLibsCore;
using CCU.Localization;

namespace CCU.Traits.Behavior
{
    public class Grab_Drugs : T_Behavior
    {
        public override bool LosCheck => true;
        public override string[] GrabItemCategories => new string[] { VItemCategory.Drugs };

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Grab_Drugs>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will grab drugs if they see any."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Grab_Drugs)),
                    
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
