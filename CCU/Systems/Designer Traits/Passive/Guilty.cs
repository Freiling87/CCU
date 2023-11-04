using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Passive
{
	public class Guilty : T_DesignerTrait, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Guilty>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character is designated Guilty for The Law."),
					[LanguageCode.Spanish] = "Este NPC siempre sera culpable",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Guilty)),
					[LanguageCode.Spanish] = "Culpable",
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.oma.mustBeGuilty = true;
		}
	}
}
