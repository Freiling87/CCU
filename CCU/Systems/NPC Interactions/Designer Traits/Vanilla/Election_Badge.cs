using CCU.Traits.Loadout_Chunk_Items;
using RogueLibsCore;
using System;
using System.Collections.Generic;

namespace CCU.Interactions
{
	public class Election_Badge : T_InteractionNPC
	{
		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			H_AgentInteractions agentInteractionsHook = agent.GetOrAddHook<H_AgentInteractions>();
			Agent interactingAgent = h.Agent;

			// UNREVIEWED!
			bool hasBadge = agent.inventory.HasItem(VanillaItems.MayorsMansionGuestBadge)
				|| (agent.gc.multiplayerMode && !agent.gc.serverPlayer && agent.oma.hasMayorBadge);

			if (!interactingAgent.inventory.HasItem(VanillaItems.MayorsMansionGuestBadge) && hasBadge)
			{
				if (agent.relationships.GetRel(interactingAgent) == VRelationship.Aligned 
					|| agent.relationships.GetRel(interactingAgent) == VRelationship.Loyal 
					|| agent.relationships.GetRel(interactingAgent) == VRelationship.Submissive)
					agent.agentInteractions.AddButton(VButtonText.MayorBadge_GiveAlly); // Should work vanilla
				else
				{
					agent.agentInteractions.AddButton(VButtonText.MayorBadge_BribeItem); // Should work vanilla
					agent.agentInteractions.AddButton(VButtonText.MayorBadge_BribeMoney, agent.determineMoneyCost(VDetermineMoneyCost.BribeMayorBadge)); // Should work vanilla
					agent.agentInteractions.AddButton(VButtonText.MayorBadge_Threaten, " (" + agent.relationships.FindThreat(interactingAgent, false).ToString() + "%)"); // ←
				}
			}
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Election_Badge>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("If this character has the Mayor Badge, they can be asked, threatened or bribed to hand it over."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Election_Badge)),
				})
				.WithUnlock(new TU_DesignerUnlock
				{
					Recommendations = new List<string> { nameof(Chunk_Mayor_Badge)},
				});
		}
	}
}