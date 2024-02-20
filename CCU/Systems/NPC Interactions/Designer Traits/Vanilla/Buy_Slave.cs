using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Agent_Interactions
{
	public class Buy_Slave : T_Interaction
	{
		public override string ButtonTextName => VButtonText.PurchaseSlave;

		private static bool CountsAsSlave(Agent agent) =>
			agent.agentName == VanillaAgents.Slave;
		// TODO: Helmet

		//[RLSetup]
		// Doesn't work yet
		public static void Setup()
		{
			RogueInteractions.CreateProvider<Agent>(h =>
			{
				Agent agent = h.Object;
				Agent interactingAgent = h.Agent;
				List<Agent> slaves = GC.agentList.Where(a => a.slaveOwners.Contains(agent)).ToList();
				bool questSlave = slaves.Any(s => s.rescueForQuest != null || s.oma.rescuingForQuest);

				// TODO: MoneySuccess
				if (questSlave)
				{
					int price = agent.determineMoneyCost(VDetermineMoneyCost.QuestSlavePurchase);

					h.AddButton(VButtonText.PurchaseSlave, price, m =>
					{
						agent.agentInteractions.GiveSlave(m.Object, interactingAgent, true, 0, price, false, false);
					});
				}
				else if (slaves.Any())
				{
					int price = agent.determineMoneyCost(VDetermineMoneyCost.SlavePurchase);

					h.AddButton(VButtonText.PurchaseSlave, price, m =>
					{
						agent.agentInteractions.GiveSlave(m.Object, interactingAgent, true, 0, price, false, false);
					});
				}

			});

			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Buy_Slave>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("If this character owns any Slaves, they will sell them."),

				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Buy_Slave)),

				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}