using HarmonyLib;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU
{
    public class AI_Pickpocket : CustomTrait
    {
        [RLSetup]
        public static void Setup()
        {
            RogueLibs.CreateCustomTrait<AI_Pickpocket>()
                .WithDescription(new CustomNameInfo("This character will pick pockets if set to wander the city.\n\nWarning: This is for use by content creators only. Use by players, unless instructed by campaign author, may cause unintended consequences."))
                .WithName(new CustomNameInfo("AI: Pickpocket"))
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

    [HarmonyPatch(declaringType: typeof(BrainUpdate))]
    public class BrainUpdate_Patches
	{
        [HarmonyPrefix, HarmonyPatch(methodName: nameof(BrainUpdate.MyUpdate), argumentTypes: new Type[0] { })]
        public static bool MyUpdate_Prefix(BrainUpdate __instance)
		{
            // This has the wandering NPC code like pickpocketing, Hobo grabbing, etc.

            return true;
		}
    }
}
