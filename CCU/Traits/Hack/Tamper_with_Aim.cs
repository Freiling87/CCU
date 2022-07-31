using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Hack
{
    public class Tamper_with_Aim : T_Hack
    {
        public override string ButtonText => VButtonText.Hack_TamperAim;

        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Tamper_with_Aim>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can be hacked to tamper with their ranged weapon aim.\n\n" +
                    "<color=red>Requires:</color> Electronic"),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Tamper_with_Aim)),
                    
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