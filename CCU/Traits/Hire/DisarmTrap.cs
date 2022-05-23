using BepInEx.Logging;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Traits.Hire
{
    public class DisarmTrap : CustomTrait
    {
        //[RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<DisarmTrap>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = "This character can be hired to disarm a trap.",
                    [LanguageCode.Russian] = "",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = CTrait.DisarmTrap,
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
