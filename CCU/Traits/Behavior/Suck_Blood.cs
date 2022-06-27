using RogueLibsCore;

namespace CCU.Traits.Behavior
{
    public class Suck_Blood : T_Behavior
    {
        public override bool LosCheck => true;
        public override string[] GrabItemCategories => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Suck_Blood>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will suck blood like the Vampire.\n\n<color=red>Requires:</color> {0}", vSpecialAbility.Bite),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Suck_Blood)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Eat_Corpses)), DisplayName(typeof(Pick_Pockets)) },
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
