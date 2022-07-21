using RogueLibsCore;
using CCU.Localization;

namespace CCU.Traits.Hire_Type
{
    public class Muscle : T_HireType
    {
        public override string HiredActionButtonText => null;
        public override string ButtonText => VButtonText.Hire_Muscle;
        public override object HireCost => VDetermineMoneyCost.Hire_Soldier;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Muscle>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired as a bodyguard.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Muscle)),
                    
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
