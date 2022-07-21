using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Hack
{
    public class Explode : T_Hack
    {
        public override string ButtonText => VButtonText.Hack_Haywire;

        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Explode>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be hacked to Explode.\n\n" + 
                    "<color=red>Requires:</color> Electronic"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Explode)),
                    
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