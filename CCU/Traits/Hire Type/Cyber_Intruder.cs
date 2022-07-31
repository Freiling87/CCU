using RogueLibsCore;
using CCU.Localization;

namespace CCU.Traits.Hire_Type
{
    public class Cyber_Intruder : T_HireType
    {
        public override string HiredActionButtonText => VButtonText.Hired_HackSomething;
        public override string ButtonText => VButtonText.Hire_Expert;
        public override object HireCost => VDetermineMoneyCost.Hire_Hacker;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Cyber_Intruder>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to hack something remotely.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Cyber_Intruder), "Cyber-Intruder"),
                    
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
