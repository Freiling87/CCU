using RogueLibsCore;
using CCU.Localization;

namespace CCU.Traits.Hire_Type
{
    public class Decoy : T_HireType
    {
        public override string HiredActionButtonText => VButtonText.Hired_CauseRuckus;
        public override string ButtonText => VButtonText.Hire_Expert;
        public override object HireCost => VDetermineMoneyCost.Hire_Thief;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Decoy>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to cause a distraction, or a Ruckus, if you will.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Decoy)),
                    
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
