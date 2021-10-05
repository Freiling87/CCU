using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using RogueLibsCore;
using CCU.Traits.AI;
using Random = UnityEngine.Random;
using System.Reflection;
using CCU.Traits;
using CCU.Traits.AI.Hire;
using CCU.Traits.AI.Vendor;
using CCU.Traits.AI.TraitTrigger;
using Google2u;
using CCU.Traits.AI.Interaction;

namespace CCU.Patches.Behaviors
{
	[HarmonyPatch(declaringType: typeof(AgentInteractions))]
	public class P_AgentInteractions
	{
		private static readonly ManualLogSource logger = CCULogger.GetLogger();
		public static GameController GC => GameController.gameController;

		[HarmonyPrefix, HarmonyPatch(methodName: nameof(AgentInteractions.DetermineButtons), argumentTypes: new[] { typeof(Agent), typeof(Agent), typeof(List<string>), typeof(List<string>), typeof(List<int>) })]
		private static bool DetermineButtons_Prefix(Agent agent, Agent interactingAgent, List<string> buttons1, List<string> buttonsExtra1, List<int> buttonPrices1, AgentInteractions __instance, ref List<string> ___buttons, ref List<string> ___buttonsExtra, ref List<int> ___buttonPrices, ref Agent ___mostRecentAgent, ref Agent ___mostRecentInteractingAgent)
		{
			Core.LogMethodCall();
			TraitManager.LogTraitList(agent);

			if (agent != agent.gc.playerAgent && (TraitManager.HasTraitFromList(agent, TraitManager.HireTraits) || TraitManager.HasTraitFromList(agent, TraitManager.InteractionTraits) || TraitManager.HasTraitFromList(agent, TraitManager.VendorTypes)))
			{
				___buttons = buttons1;
				___buttonsExtra = buttonsExtra1;
				___buttonPrices = buttonPrices1;
				___mostRecentAgent = agent;
				___mostRecentInteractingAgent = interactingAgent;

				// agent.SayDialogue("InteractB"); // No custom dialogue
				agent.gc.audioHandler.Play(agent, "AgentTalk");
				logger.LogDebug("hasSpecialInvDatabase: " + agent.hasSpecialInvDatabase);
				Type vendorTrait = TraitManager.GetOnlyTraitFromList(agent, TraitManager.VendorTypes);
				
				if (agent.HasTrait<Interaction_Moochable>() && interactingAgent.statusEffects.hasTrait("CanBorrowMoney"))
					__instance.AddButton("BorrowMoney"); 

				// TODO: Verify if Vendor is compatible with Hireable. If not, separate them.

				// Interaction 

				if (TraitManager.HasTraitFromList(agent, TraitManager.InteractionTraits))
				{
					Core.LogCheckpoint("Interaction");

				}

				// Vendor 
				if (!(vendorTrait is null) && agent.hasSpecialInvDatabase) // All Vendor Traits
				{
					Core.LogCheckpoint("Vendor");

					bool canBuy = true;

					if (agent.HasTrait<TraitTrigger_CopAccess>())
						canBuy = interactingAgent.HasTrait("TheLaw");
					if (agent.HasTrait<TraitTrigger_HonorableThief>())
						canBuy = interactingAgent.statusEffects.hasTrait("HonorAmongThieves") || interactingAgent.statusEffects.hasTrait("HonorAmongThieves2");
					if (agent.HasTrait<TraitTrigger_CoolCannibal>())
						canBuy = interactingAgent.statusEffects.hasTrait("CannibalsNeutral");

					if (canBuy)
					{
						__instance.AddButton("Buy");

						if (interactingAgent.inventory.HasItem("FreeItemVoucher"))
							__instance.AddButton("UseVoucher");
					}
				}

				// Hire 
				if (TraitManager.HasTraitFromList(agent, TraitManager.HireTraits))
				{
					Core.LogCheckpoint("Hire");

					if (agent.employer == null && agent.relationships.GetRelCode(interactingAgent) != relStatus.Annoyed)
					{
						Core.LogCheckpoint("Hire Initial");

						float hireCostFactor = 1.0f;
						if (agent.HasTrait<Hire_CostMore>())
							hireCostFactor = 1.5f;
						else if (agent.HasTrait<Hire_CostLess>())
							hireCostFactor = 0.5f;

						bool bananaCost = agent.HasTrait<Hire_CostBanana>();

						if (agent.HasTrait<Hire_Bodyguard>())
						{
							if (interactingAgent.inventory.HasItem("HiringVoucher"))
								__instance.AddButton("AssistMe", 6666);
							else
								__instance.AddButton("HireAsProtection", bananaCost ? 6789 : (int)(agent.determineMoneyCost("SoldierHire") * hireCostFactor));
						}
						else if (
							agent.HasTrait<Hire_BreakIn>() ||
							agent.HasTrait<Hire_CauseRuckus>() ||
							agent.HasTrait<Hire_Hack>() ||
							agent.HasTrait<Hire_Safecrack>() ||
							agent.HasTrait<Hire_Tamper>())
						{
							if (interactingAgent.inventory.HasItem("HiringVoucher"))
								__instance.AddButton("AssistMe", 6666);
							else
								__instance.AddButton("AssistMe", bananaCost ? 6789 : (int)(agent.determineMoneyCost("ThiefAssist") * hireCostFactor));
						}
					}
					else if (!agent.oma.cantDoMoreTasks)
					{
						Core.LogCheckpoint("Hire Order");

						if (agent.HasTrait<Hire_BreakIn>())
							__instance.AddButton("LockpickDoor");
						if (agent.HasTrait<Hire_CauseRuckus>())
							__instance.AddButton("CauseRuckus");
						if (agent.HasTrait<Hire_Hack>())
							__instance.AddButton("HackSomething");
						if (agent.HasTrait<Hire_Safecrack>())
							__instance.AddButton(CJob.SafecrackSafe);
						if (agent.HasTrait<Hire_Tamper>())
							__instance.AddButton(CJob.TamperSomething);
					}
				}

				logger.LogDebug("Count: " + ___buttons.Count);

				return false;
			}

			return true;
		}

		[HarmonyPrefix, HarmonyPatch(methodName:nameof(AgentInteractions.PressedButton), argumentTypes:new[] { typeof(Agent), typeof(Agent), typeof(string), typeof(int) })]
		public static bool PressedButton_Prefix(Agent agent, Agent interactingAgent, string buttonText, int buttonPrice, AgentInteractions __instance)
		{
			/* ToDo:
			 * 
			 * Add custom names for nameDB.GetName(namehere, "Interface");
			 * 
			 * Prefix InvInterface.ShowTarget2
			 * Prefix InvInterface.TargetAnywhere
			 * Prefix PFOInteractions.TargetObject
			 */
			Core.LogMethodCall();
			logger.LogDebug("\tbuttonText: " + buttonText);

			__instance.interactor = null;
			__instance.allAttack = false;
			__instance.allGoHere = false;

			if (buttonText == CJob.SafecrackSafe)
			{
				__instance.interactor = interactingAgent;
				agent.commander = interactingAgent;
				interactingAgent.mainGUI.invInterface.ShowTarget(agent, "CauseRuckus"); // Todo: Replace with real name, but use this now in case it breaks it
				agent.StopInteraction();

				return false;
			}
			else if (buttonText == CJob.TamperSomething)
			{
				__instance.interactor = interactingAgent;
				agent.commander = interactingAgent;
				interactingAgent.mainGUI.invInterface.ShowTarget(agent, "CauseRuckus"); // Todo: Replace with real name, but use this now in case it breaks it
				agent.StopInteraction();

				return false;
			}

			return true;
		}

		// Non-Patch
		public static void SafecrackSafe(Agent agent, Agent interactingAgent, PlayfieldObject mySafe)
		{
			if (interactingAgent.gc.serverPlayer)
			{
				agent.job = CJob.SafecrackSafe;
				agent.jobCode = jobType.GetSupplies; // TODO
				agent.StartCoroutine(agent.ChangeJobBig(""));
				agent.assignedPos = mySafe.GetComponent<ObjectReal>().FindDoorObjectAgentPos();
				agent.assignedObject = mySafe.playfieldObjectReal;
				agent.assignedAgent = null;
				agent.gc.audioHandler.Play(agent, "AgentOK");

				return;
			}

			interactingAgent.objectMult.CallCmdObjectActionExtraObjectID(agent.objectNetID, CJob.SafecrackSafe, mySafe.objectNetID);
		}


	}
}
