using RogueLibsCore;
using CCU.Localization;
using System;

namespace CCU.Traits.Interaction
{
    public class Administer_Blood_Bag : T_Interaction
    {
        public override bool AllowUntrusted => false;
        public override string ButtonText => VButtonText.AdministerBloodBag;
        public override bool ExtraTextCostOnly => true;
        public override string DetermineMoneyCost => VDetermineMoneyCost.HP_20;

        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Administer_Blood_Bag>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character can give you a blood bag, at the cost of 20 HP."),
                     
                })
                .WithName(new CustomNameInfo 
                {
                    [LanguageCode.English] = DesignerName(typeof(Administer_Blood_Bag)),
                    
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
