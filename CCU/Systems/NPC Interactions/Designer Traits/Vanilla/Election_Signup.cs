using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Interactions
{
	public class Election_Signup : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.RunForOffice;

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Election_Signup>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format($"This character will accept applications for the election."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Election_Signup)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			EventTriggerFloor electionStatus = agent.agentInteractions.GetEventTrigger("MayorElectionResultsPosition");

			if (!(electionStatus is null)
				&& electionStatus.triggerState != "ElectionDonePlayer" 
				&& electionStatus.triggerState != "ElectionDoneMayor")
			{
				h.AddButton(VButtonText.RunForOffice, m =>
				{
					QualifyForElection(agent, agent.interactingAgent);
				});
			}
		}

		private static void QualifyForElection(Agent clerk, Agent candidate)
		{
			Agent mayor = GC.agentList.FirstOrDefault(a => a.isMayor);

			if (!GC.objectRealList.Any(or =>
				or is Computer computer
				&& !computer.destroyed
				&& computer.functional // TEST
				&& computer.startingChunk == clerk.startingChunk)) // TEST, vanilla uses assumed chunk type check
				clerk.SayDialogue(VDialogue.Clerk_MayorComputerBroken);
			else if (mayor.dead && !mayor.teleporting)
				clerk.SayDialogue(VDialogue.Clerk_MayorDeadNoElection);
			else if (mayor.inventory.equippedArmorHead != null && mayor.inventory.equippedArmorHead.invItemName != VanillaItems.MayorHat)
				clerk.SayDialogue(VDialogue.Clerk_MayorLostHat);
			else if (mayor.relationships.GetRelCode(candidate) == relStatus.Hostile)
				clerk.SayDialogue(VDialogue.Clerk_MayorHatesPlayer);
			else
				RunForOffice(clerk, candidate, mayor, GC.sessionData.electionScore[candidate.isPlayer]);
			
			clerk.StopInteraction();
		}

		private static void RunForOffice(Agent clerk, Agent candidate, Agent mayor, int electionScore)
		{
			if (GC.serverPlayer)
			{
				Agent.gangCount++;

				mayor.brainUpdate.activeNextUpdate = true;
				mayor.SetPreviousDefaultGoal(mayor.defaultGoal);
				mayor.SetDefaultGoal(VAgentGoal.GetElectionResults);
				mayor.startingChunk = 0;
				mayor.cantBump = false;
				mayor.assignedEventTrigger = clerk.agentInteractions.GetEventTrigger("MayorElectionResultsPosition");
				mayor.gang = Agent.gangCount;
				mayor.modLeashes = 0;

				clerk.SayDialogue(VDialogue.Clerk_ElectionResultsIn);
				clerk.brainUpdate.activeNextUpdate = true;
				clerk.SetPreviousDefaultGoal(clerk.defaultGoal);
				clerk.SetDefaultGoal(VAgentGoal.GetElectionResults);
				clerk.assignedEventTrigger = clerk.agentInteractions.GetEventTrigger("ClerkElectionResultsPosition");
				clerk.assignedEventTrigger2 = clerk.agentInteractions.GetEventTrigger("MayorElectionResultsPosition");

				List<Agent> bodyguards = GC.agentList.Where(a => a.guardingMayor).ToList();

				foreach (Agent bodyguard in bodyguards)
				{
					bodyguard.brainUpdate.activeNextUpdate = true;
					bodyguard.SetPreviousDefaultGoal(bodyguard.defaultGoal);
					bodyguard.SetDefaultGoal(VAgentGoal.GetElectionResults);
					bodyguard.startingChunk = 0;
					bodyguard.SetFollowing(null);
					bodyguard.assignedEventTrigger = clerk.agentInteractions.GetEventTrigger("MayorElectionResultsPosition");
					bodyguard.gang = Agent.gangCount;
					bodyguard.modLeashes = 0;
					bodyguard.modVigilant = 0;
					bodyguard.guardingMayor = true;
					bodyguard.specialWalkSpeed = mayor.speedMax;
					bodyguard.oma.modProtectsProperty = 0;

					foreach (Agent gangmember in bodyguards)
					{
						if (!gangmember.gangMembers.Contains(bodyguard))
							gangmember.gangMembers.Add(bodyguard);

						if (!bodyguard.gangMembers.Contains(gangmember))
							bodyguard.gangMembers.Add(gangmember);
					}
				}

				EventTriggerFloor eventTrigger = clerk.agentInteractions.GetEventTrigger("MayorElectionResultsPosition");
				eventTrigger.triggerState = "RunningElection";
				eventTrigger.triggerAgent = candidate;
				eventTrigger.triggerInt = candidate.playerColor;

				if (electionScore > 0)
					eventTrigger.triggerBool = true;
			}
			else
			{
				clerk.agentInteractions.GetEventTrigger("MayorElectionResultsPosition").triggerState = "RunningElection";
				candidate.objectMult.ObjectAction(clerk.objectNetID, "RunForOffice", (float)GC.sessionData.electionScore[candidate.isPlayer]);
			}
		}
	}
}