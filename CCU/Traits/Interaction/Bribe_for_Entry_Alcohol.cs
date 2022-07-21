using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Bribe_for_Entry_Alcohol : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => null;
        public override bool ExtraTextCostOnly => false;
        public override string InteractionCost => null;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Bribe_for_Entry_Alcohol>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character, if serving as Doorman, will allow access if bribed with alcohol."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DisplayName(typeof(Bribe_for_Entry_Alcohol),("Bribe for Entry (Alcohol)")),
                    
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
