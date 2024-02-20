using RogueLibsCore;
using System;
using static CCU.Traits.Rel_Faction.T_Rel_Faction;

namespace CCU.Interactions
{
	public class Pay_Entrance_Fee : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.PayEntranceFee;
		public override string MoneyCostName => VDetermineMoneyCost.Bribe;
		public override bool RequireTrust => false;

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			H_AgentInteractions agentInteractionsHook = agent.GetHook<H_AgentInteractions>();
			Agent interactingAgent = h.Agent;
			bool hasOtherDoorman = false;
			string relationship = agent.relationships.GetRel(interactingAgent);
			int relationshipLevel = VRelationship.GetRelationshipLevel(relationship);

			foreach (Agent otherAgent in GC.agentList)
			{
				if (otherAgent.startingChunk == agent.startingChunk
					&& otherAgent.ownerID == agent.ownerID
					&& otherAgent != agent
					&& (otherAgent.oma.modProtectsProperty != 0 || AlignmentUtils.CountsAsDoorman(otherAgent)))
				{
					relStatus relCode = otherAgent.relationships.GetRelCode(interactingAgent);

					if (relationshipLevel < 3 && relCode != relStatus.Submissive)
					{
						hasOtherDoorman = true;
						break;
					}
				}
			}

			if (agent.agentInteractions.HasMetalDetector(agent, interactingAgent) &&
				(relationshipLevel > 2 || relationship == VRelationship.Submissive || interactingAgent.agentName == VanillaAgents.SuperCop))
			{
				agent.SayDialogue("DontNeedMoney");
				agent.agentInteractions.BouncerTurnOffLaserEmitter(agent, interactingAgent, false);
			}
			else
			{
				if ((relationshipLevel > 2 || relationship == VRelationship.Submissive) &&
					(!hasOtherDoorman || relationship == VRelationship.Submissive))
				{
					agent.SayDialogue("DontNeedMoney");
					return;
				}
				else if (interactingAgent.statusEffects.hasTrait(VanillaTraits.Malodorous)
					|| interactingAgent.statusEffects.hasTrait(VanillaTraits.Wanted)
					|| interactingAgent.statusEffects.hasTrait(VanillaTraits.Wanted))
				{
					agent.SayDialogue("WontJoinA");
					return;
				}

				string buttonText =
					(agent.startingChunkRealDescription == VChunkType.Bar ||
					agent.startingChunkRealDescription == VChunkType.DanceClub ||
					agent.startingChunkRealDescription == VChunkType.Arena ||
					agent.startingChunkRealDescription == VChunkType.MusicHall)
						? VButtonText.PayEntranceFee
						: VButtonText.Bribe;

				h.AddButton(buttonText, agent.determineMoneyCost(VDetermineMoneyCost.Bribe), m =>
				{
					m.Object.agentInteractions.Bribe(m.Object, interactingAgent, VItemName.Money, m.Object.determineMoneyCost(VDetermineMoneyCost.Bribe));
				});
			}
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pay_Entrance_Fee>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character, if serving as Doorman, will allow access if paid with cash.\n\nBypasses Untrusting traits."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pay_Entrance_Fee)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}