using RogueLibsCore;
using System;

namespace CCU.Traits.TraitGate
{
    public class HonorableThief : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<HonorableThief>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This Agent's behaviors will react to the player's Honor Among Thieves trait.\n\n" + 
                    "<color=green>Interactions</color>\n" +
                    CTrait.Pickpocket + ": Will not target the player if they have Honor Among Thieves.\n" +
                    CTrait.MerchantType_Thief+ ": Will not sell items unless player has Honor Among Thieves."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.TraitGate_HonorableThief,
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
