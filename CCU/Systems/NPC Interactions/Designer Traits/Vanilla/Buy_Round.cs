using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Interactions
{
	public class Buy_Round : T_InteractionNPC
	{
		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			// TODO: Allow use only once per NPC offering it

			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;
			string relationship = agent.relationships.GetRel(interactingAgent);
			int relationshipLevel = VRelationship.GetRelationshipLevel(relationship);
			List<Agent> drinkers = GC.agentList.Where(a =>
					a != agent
					&& !a.dead && !a.zombified && !a.oma.mindControlled
					&& a.startingChunk == agent.startingChunk && a.gc.tileInfo.GetTileData(a.tr.position).chunkID == a.startingChunk
					&& a.prisoner == 0
					&& relationshipLevel > 2 // Neutral or better
					&& relationship != VRelationship.Submissive // Vanilla choice
					&& !a.statusEffects.hasTrait(VanillaTraits.Jugularious) && !a.statusEffects.hasTrait(VanillaTraits.OilReliant))
			.ToList();

			if (drinkers.Any())
				h.AddButton(VButtonText.BuyRound, agent.determineMoneyCost(drinkers.Count, VDetermineMoneyCost.BuyRound), m =>
				{
					if (m.Object.moneySuccess(m.Object.determineMoneyCost(drinkers.Count, VDetermineMoneyCost.BuyRound)))
						m.Object.agentInteractions.BuyRound(m.Object, interactingAgent);

					m.Object.StopInteraction();
				});
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Buy_Round>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be paid to serve a round of drinks to everyone in the chunk."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Buy_Round)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}