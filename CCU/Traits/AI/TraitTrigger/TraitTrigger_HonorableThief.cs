using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Traits.AI.TraitTrigger
{
    public class TraitTrigger_HonorableThief : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<TraitTrigger_HonorableThief>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This NPC's behaviors will react to the player's Honor Among Thieves trait.\n\n<color=green>{0}</color>: Will not pickpocket the player if they have Honor Among Thieves.\n\n<color=green>{1}</color>: Will not sell wares unless player has Honor Among Thieves.", CTrait.AI_Behavior_Pickpocket, CTrait.AI_Vendor_Thief),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.AI_TraitTrigger_HonorableThief,
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
