using RogueLibsCore;
using System;
using static CCU.Traits.Rel_Faction.T_Rel_Faction;

namespace CCU.Interactions
{
	public class Bribe_for_Entry_Alcohol : T_InteractionNPC
	{
		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;
			string relationship = agent.relationships.GetRel(interactingAgent);
			int relationshipLevel = VRelationship.GetRelationshipLevel(relationship);
			bool hasOtherDoorman = false;

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
				agent.SayDialogue(VDialogue.Bouncer_DontNeedMoney);
				agent.agentInteractions.BouncerTurnOffLaserEmitter(agent, interactingAgent, false);
			}
			else
			{
				if (!hasOtherDoorman
					&& (relationshipLevel > 2 || relationship == VRelationship.Submissive))
				{
					agent.SayDialogue(VDialogue.Bouncer_DontNeedMoney);
					return;
				}
				else if (interactingAgent.statusEffects.hasTrait(VanillaTraits.Malodorous) ||
						interactingAgent.statusEffects.hasTrait(VanillaTraits.Wanted) ||
						interactingAgent.statusEffects.hasTrait(VanillaTraits.Suspicious))
				{
					agent.SayDialogue(VDialogue.NA_WontJoinA);
					return;
				}

				if (interactingAgent.inventory.HasItem(VItemName.Beer))
					h.AddButton(VButtonText.BribeForEntryBeer, m =>
					{
						agent.agentInteractions.Bribe(agent, interactingAgent, VItemName.Beer, 0);
					});
				else if (interactingAgent.inventory.HasItem(VItemName.Whiskey))
					h.AddButton(VButtonText.BribeForEntryWhiskey, m =>
					{
						agent.agentInteractions.Bribe(agent, interactingAgent, VItemName.Whiskey, 0);
					});
			}
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bribe_for_Entry_Alcohol>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character, if serving as Doorman, will allow access if bribed with alcohol."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bribe_for_Entry_Alcohol), ("Bribe for Entry (Alcohol)")),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}