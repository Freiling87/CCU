using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Traits.Behaviors
{
    public class Thief_Pickpocket : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<Thief_Pickpocket>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("If spawned as an NPC, this character will pick pockets if set to wander the city.\n\n{1} = Will not pickpocket from player if they have Honor Among Thieves", CTrait.AI_Thief_HonestThief),
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.AI_Thief_Pickpocket,
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
