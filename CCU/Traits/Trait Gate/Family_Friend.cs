using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
    public class Family_Friend : T_TraitGate
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Family_Friend>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This NPC will be Aligned to all players with Friend of the Family, as well as all Mobsters."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Family_Friend)),
                    
                })
                .WithUnlock(new TraitUnlock_CCU
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
