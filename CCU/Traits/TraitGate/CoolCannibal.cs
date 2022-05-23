using RogueLibsCore;
using System;

namespace CCU.Traits.TraitGate
{
    public class CoolCannibal : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<CoolCannibal>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This NPC's behaviors will react to the player's Cool with Cannibals trait.\n\n" + 
                    "<color=green>Interactions</color>\n" +
                    CTrait.HideInObject + ", " + CTrait.Relationships_AggressiveCannibal + ": Will not target the player if they have Cool with Cannibals.\n" +
                    CTrait.MerchantType_Anthropophagie + ": Will not sell wares unless player has Cool With Cannibals"),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.TraitGate_CoolCannibal,
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
