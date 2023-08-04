using RogueLibsCore;
using System;

namespace CCU.Traits.Combat
{
    public class Fearless : T_Combat, ISetupAgentStats
    {
        [RLSetup]
        public static void Setup()
        {
            PostProcess = RogueLibs.CreateCustomTrait<Fearless>()
                .WithDescription(new CustomNameInfo
                {
                    [LanguageCode.English] = String.Format("This character will never flee from combat."),
                    [LanguageCode.Spanish] = "Este NPC nunca intentara escapar del combate.",

                })
                .WithName(new CustomNameInfo
                {
                    [LanguageCode.English] = DesignerName(typeof(Fearless)),
                    [LanguageCode.Spanish] = "Agalludo",

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
            agent.mustFlee = false;
            agent.wontFlee = true;
        }
	}
}
