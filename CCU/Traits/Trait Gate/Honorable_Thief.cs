using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
    public class Honorable_Thief : T_TraitGate
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Honorable_Thief>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This Agent's behaviors will react to the player's Honor Among Thieves trait.\n\n" + 
                    "<color=green>Interactions</color>\n" +
                    "- Will not target the player with Pickpocketing if they have Honor Among Thieves.\n" +
                    "- Will not sell items unless player has Honor Among Thieves."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Honorable_Thief)),
                    
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
