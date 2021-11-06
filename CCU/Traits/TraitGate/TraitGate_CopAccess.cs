using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Traits.TraitGate
{
    public class TraitGate_CopAccess : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<TraitGate_CopAccess>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("Certain NPC behaviors will only be accessible if the player has The Law.\n\n<color=green>{0}</color>, <color=green>{1}</color>, <color=green>{2}:</color> Will not sell to the player if they don't have The Law.", CTrait.MerchantType_Contraband, CTrait.MerchantType_CopStandard, CTrait.MerchantType_CopSWAT),
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
