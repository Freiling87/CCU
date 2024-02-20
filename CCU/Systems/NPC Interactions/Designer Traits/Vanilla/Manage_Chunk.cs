using CCU.Traits.Loadout_Chunk_Items;
using RogueLibsCore;
using System;
using System.Linq;

namespace CCU.Interactions
{
	public class Manage_Chunk : T_InteractionNPC
	{
		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			H_AgentInteractions agentInteractionsHook = agent.GetOrAddHook<H_AgentInteractions>();
			Agent interactingAgent = h.Agent;

			switch (agent.startingChunkRealDescription)
			{
				case VChunkType.Arena:
					for (int i = 0; i < agent.gc.objectRealList.Count; i++)
					{
						ObjectReal objectReal = agent.gc.objectRealList[i];

						if (objectReal.startingChunk == agent.startingChunk && objectReal.objectName == VObjectReal.EventTriggerFloor)
						{
							EventTriggerFloor eventTriggerFloor = (EventTriggerFloor)objectReal;

							if (!eventTriggerFloor.functional &&
								eventTriggerFloor.triggerState != "NeedToPayOut" &&
								eventTriggerFloor.triggerState != "NoPayout" &&
								eventTriggerFloor.triggerState != "Cheated")
								agent.SayDialogue("NoMoreFights");
							else if (eventTriggerFloor.triggerState == "Initial")
							{
								if (GC.agentList.Any(a => a != interactingAgent && (a.isPlayer > 0 || a.employer != null)))
									agent.SayDialogue("AskToStartFightMultiple");
								else
									agent.SayDialogue("AskToStartFight");

								h.AddButton(VButtonText.Arena_SignUpToFight, m =>
								{
									m.Object.agentInteractions.SignUpToFight(m.Object, interactingAgent);
									m.Object.StopInteraction();
								});
							}
							else if (eventTriggerFloor.triggerState == "FightSignedUp")
								agent.SayDialogue("SignedUpToFight");
							else if (eventTriggerFloor.triggerState == "FightStarted")
								agent.SayDialogue("FightCheer");
							else if (eventTriggerFloor.triggerState == "NeedToPayOut")
							{
								agent.SayDialogue("NoMoreFights");
								h.AddButton(VButtonText.PayOutFight, m =>
								{
									m.Object.agentInteractions.PayOutFight(m.Object, interactingAgent);
									m.Object.StopInteraction();
								});
							}
							else if (eventTriggerFloor.triggerState == "NoPayout")
								agent.SayDialogue("LostFightNoPayout");
							else if (eventTriggerFloor.triggerState == "Cheated")
								agent.SayDialogue("CheatedNoPayout");
							else if (eventTriggerFloor.triggerState == "FightsOver")
								agent.SayDialogue("NoMoreFights");
						}
					}
					break;

				case VChunkType.DeportationCenter:
					string relationship = agent.relationships.GetRel(interactingAgent);
					int relationshipLevel = VRelationship.GetRelationshipLevel(relationship);

					if (interactingAgent.upperCrusty)
						agent.SayDialogue("NoDeportationUpperCrusty");
					else if (relationshipLevel > 2 || relationship == VRelationship.Submissive)
						agent.SayDialogue("DontNeedMoney");
					else
					{
						h.AddButton(VButtonText.BribeDeportation, agent.determineMoneyCost(VDetermineMoneyCost.BribeDeportation), m =>
						{
							m.Object.agentInteractions.Bribe(m.Object, interactingAgent, VItemName.Money, agent.determineMoneyCost(VDetermineMoneyCost.BribeDeportation));
						});
						h.AddButton(VButtonText.BribeDeportationItem, m =>
						{
							m.Object.ShowUseOn("BribeDeportationItem");
						});
					}
					break;

				case VChunkType.Hotel:
					if (agent.inventory.HasItem(VItemName.Key))
						h.AddButton(VButtonText.BuyKeyHotel, agent.determineMoneyCost(VDetermineMoneyCost.BuyKey), m =>
						{
							if (!interactingAgent.inventory.hasEmptySlot())
							{
								interactingAgent.inventory.PlayerFullResponse(interactingAgent);
								m.Object.StopInteraction();
								return;
							}

							if (!m.Object.moneySuccess(agent.determineMoneyCost(VDetermineMoneyCost.BuyKey)))
							{
								m.Object.StopInteraction();
								return;
							}

							m.Object.agentInteractions.BuyKey(m.Object, interactingAgent);
							m.Object.SayDialogue(VDialogue.Clerk_BoughtHotelKey);
							m.Object.SetChangeElectionPoints(interactingAgent);
							m.Object.StopInteraction();
						});
					break;
			}
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Manage_Chunk>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will do Clerk/Jock behaviors if they're placed in certain chunks:\n" +
					"- Arena\n" +
					"- Deportation Center\n" +
					"- Hotel *\n\n" +
					"*<color=red>Requires</color>: {0}", DocumentationName(typeof(Chunk_Key))),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Manage_Chunk)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}