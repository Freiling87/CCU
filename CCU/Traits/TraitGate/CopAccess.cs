using RogueLibsCore;
using System;

namespace CCU.Traits.TraitGate
{
    public class CopAccess : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<CopAccess>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Certain NPC behaviors will only be accessible if the player has The Law.\n\n" + 
                    "<color=green>Interactions</color>\n" +
                    CTrait.MerchantType_Contraband + ", " + CTrait.MerchantType_CopStandard + ", " + CTrait.MerchantType_CopSWAT + ": Will not sell to the player if they don't have The Law."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.TraitGate_CopAccess,
                    [LanguageCode.Russian] = "",
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
