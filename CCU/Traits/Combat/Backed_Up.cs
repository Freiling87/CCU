using CCU.Hooks;
using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
	public class Backed_Up : T_Combat, ISetupAgentStats
    {
        //[RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Backed_Up>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character has a Walkie Talkie, and will call for Police backup when they enter combat. They should probably eat more fiber."),
                    [LanguageCode.Spanish] = "Este NPC tiene un Walkie Talkie que se activa al entrar en combate llamando a los polis cercanos. Como tipica chusma.",
                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Backed_Up)),
                    [LanguageCode.Spanish] = "Llama-refuerzos",
                })
                .WithUnlock(new TraitUnlock_CCU
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

        public void SetupAgentStats(Agent agent)
        {
            agent.GetOrAddHook<H_Agent>().WalkieTalkieUsed = false;
        }
    }
}