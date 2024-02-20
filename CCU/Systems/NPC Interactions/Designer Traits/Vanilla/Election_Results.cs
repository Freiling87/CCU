using CCU.Traits.Loadout_Chunk_Items;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Interactions
{
	public class Election_Results : T_InteractionNPC
	{
		private static readonly List<string> ValidElectionStatuses = new List<string>()
		{
			"RunningElection",
			"ReadyToAnnounceElectionWinner",
			"AnnouncingElectionWinner",
			"ElectionWinnerAnnouncedMayor",
			"ElectionWinnerAnnouncedPlayer",
		};

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Object.interactingAgent;
			string ElectionStatus = agent.agentInteractions.GetEventTrigger("MayorElectionResultsPosition").ToString(); 
			// ToString needs testing here

			if (ElectionStatus is null
				|| !ValidElectionStatuses.Contains(ElectionStatus))
				return;

			if (agent.assignedEventTrigger2 == null)
			{
				agent.assignedEventTrigger = agent.agentInteractions.GetEventTrigger("ClerkElectionResultsPosition");
				agent.assignedEventTrigger2 = agent.agentInteractions.GetEventTrigger("MayorElectionResultsPosition");
			}

			if (agent.assignedEventTrigger2.triggerState == "ReadyToAnnounceElectionWinner")
			{
				agent.SayDialogue("ReadyForElectionWinner");
				
				//agent.agentInteractions.AddButton("GetElectionResults");
				h.AddButton(VButtonText.GetElectionResults, m =>
				{
					agent.agentInteractions.GetElectionResults(m.Object, interactingAgent);
				});
			}
			else if (agent.assignedEventTrigger2.triggerState == "RunningElection")
			{
				if (agent.assignedEventTrigger.triggerCount == 1)
					agent.SayDialogue("MayorNotReadyForResults");
				else
					agent.SayDialogue("GetResultsAtPark");
			}

			return;
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Election_Results>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will deliver Election results."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Election_Results)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}