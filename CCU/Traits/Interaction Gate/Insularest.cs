using RogueLibsCore;
using System;

namespace CCU.Traits.Interaction_Gate
{
    public class Insularest : T_InteractionGate
    {
        public override int MinimumRelationship => 5;

        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Insularest>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will not interact with anyone who has a relationship with their faction at Loyal or worse."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Insularest)),
                    
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
