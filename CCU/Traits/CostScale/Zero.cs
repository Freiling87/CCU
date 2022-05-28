using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
    public class Zero : T_CostScale
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Zero>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are reduced to zero.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Zero)),
                    [LanguageCode.Russian] = "",
                })
                .WithUnlock(new TraitUnlock
                {
                    Cancellations = { DisplayName(typeof(Less)), DisplayName(typeof(More)) },
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
