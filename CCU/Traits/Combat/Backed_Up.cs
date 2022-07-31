using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
    public class Backed_Up : T_Combat
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Backed_Up>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character has a Walkie Talkie, and will call for Police backup when they enter combat. They should probably eat more fiber."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Backed_Up)),
                    
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
