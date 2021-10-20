using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Traits.AI.TraitTrigger
{
    public class TraitTrigger_Scumbag : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<TraitTrigger_Scumbag>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This NPC will be a valid target for Scumbag Slaughterer."),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.AI_TraitTrigger_Scumbag,
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
