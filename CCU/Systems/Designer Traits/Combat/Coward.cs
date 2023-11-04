using BunnyLibs;
using RogueLibsCore;
using System;

namespace CCU.Traits.Combat_
{
	public class Coward : T_Combat, ISetupAgentStats
	{
		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Coward>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will always flee from combat."),
					[LanguageCode.Spanish] = "Este NPC le tiene miedo al conflicto y siempre saldra corriendo al volverse hostil.",

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Coward)),
					[LanguageCode.Spanish] = "Cobarde",

				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Cancellations = { },
					CharacterCreationCost = 0,
					IsAvailable = false,
					IsAvailableInCC = Core.designerEdition,
					UnlockCost = 0,
				});
		}
		
		

		public bool BypassUnlockChecks => false;
		public void SetupAgent(Agent agent)
		{
			agent.mustFlee = true;
			agent.wontFlee = false;
		}
	}
}
