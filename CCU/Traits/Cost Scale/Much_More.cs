using RogueLibsCore;

namespace CCU.Traits.Cost_Scale
{
    public class Much_More : T_CostScale
    {
        public override float CostScale => 2.00f;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Much_More>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character's costs are increased by 100%.",
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Much_More)),
                    
                })
                .WithUnlock(new TraitUnlock
                {
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