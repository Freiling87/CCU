using RogueLibsCore;
using SORCE.Localization;

namespace CCU.Traits.Behavior
{
    public class Grab_Food : T_Behavior
    {
        public override bool LosCheck => true;
        public override string[] GrabItemCategories => new string[] { VItemCategory.Food };

        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Grab_Food>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = string.Format("This character will grab food if they see it."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Grab_Food)),
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
