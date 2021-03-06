using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Outdoor_Outfitter : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Outdoor_Outfitter>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character sells traps & wilderness gear."),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Outdoor_Outfitter)),
                    
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
