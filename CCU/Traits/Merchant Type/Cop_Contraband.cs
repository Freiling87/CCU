using CCU.Traits.Trait_Gate;
using RogueLibsCore;
using System;

namespace CCU.Traits.Merchant_Type
{
    public class Cop_Contraband : T_MerchantType
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Cop_Contraband>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Stuff confiscated from the City's Ne'er- and/or Rarely-Do-Wells.\n\n<color=green>{0}</color> = Player needs The Law to access shop", LongishDocumentationName(typeof(Cop_Access))),
                    
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Cop_Contraband), "Cop (Contraband)"),
                    
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
