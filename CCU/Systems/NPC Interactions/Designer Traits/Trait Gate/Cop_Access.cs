using RogueLibsCore;
using System;

namespace CCU.Traits.Trait_Gate
{
	public class Cop_Access : T_TraitGate
	{
		public static bool CountsAsPolice(Agent interactingAgent) =>
			interactingAgent.HasTrait(VanillaTraits.TheLaw) 
			|| interactingAgent.HasTrait<Cop_Access>()
			|| interactingAgent.agentName == VanillaAgents.Cop 
			|| interactingAgent.agentName == VanillaAgents.CopBot 
			|| interactingAgent.agentName == VanillaAgents.SuperCop;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Cop_Access>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("Will not sell to the player if they aren't a law enforcer."),
					[LanguageCode.Spanish] = "Este NPC no le vendera al jugador al menos que tengan el rasgo La Ley",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Cop_Access)),
					[LanguageCode.Spanish] = "Acceso Policial",
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}