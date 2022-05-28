using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
    public class More : T_CostScale
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<More>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are increased by 50%.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(More)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Less)), DisplayName(typeof(Zero)) },
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
