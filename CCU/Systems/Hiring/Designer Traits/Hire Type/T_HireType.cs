using CCU.Interactions;
using CCU.Systems.PermanentHire;
using CCU.Traits.Hire_Duration;
using RogueLibsCore;
using System.Linq;
using static CCU.Traits.Rel_Faction.T_Rel_Faction;

namespace CCU.Traits.Hire_Type
{
	public abstract class T_HireType : T_InteractionNPC
	{
		public T_HireType() : base() { }

		public abstract string HiredActionButtonText { get; }
		public override string SharedButtonGroup => 
			Owner.employer is null 
			? "CCU_HireType"
			: null;

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;

			if (agent.employer == interactingAgent && !agent.oma.cantDoMoreTasks && HiredActionButtonText != null)	//	Ordering already-hired agent
			{
				h.AddButton(HiredActionButtonText, m =>
				{
					m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, HiredActionButtonText, 0);
				});

				return;
			}

			if (!(agent.employer is null) || agent.relationships.GetRelCode(interactingAgent) == relStatus.Annoyed)
				return;

			if (interactingAgent.oma.superSpecialAbility
					&& ((AlignmentUtils.CountsAsBlahd(agent) && interactingAgent.agentName == VanillaAgents.GangsterBlahd)
					|| (AlignmentUtils.CountsAsCrepe(agent) && interactingAgent.agentName == VanillaAgents.GangsterCrepe)))
				h.AddButton(VButtonText.JoinMe, m =>
				{
					m.Object.agentInteractions.QualifyHireAsProtection(m.Object, interactingAgent, 0);
				});
			else
			{
				string hireButtonText =
					agent.GetTraits<T_HireType>().Where(t => t.ButtonTextName == VButtonText.Hire_Muscle).Any()
						? VButtonText.Hire_Muscle
						: VButtonText.Hire_Expert;

				string costString =
					agent.GetTraits<T_HireType>().Where(t => t.ButtonTextName == VButtonText.Hire_Muscle).Any()
						? VDetermineMoneyCost.Hire_Soldier
						: VDetermineMoneyCost.Hire_Hacker;

				if (!agent.HasTrait<Permanent_Hire_Only>()) // Normal Hire
				{
					int normalHireCost = agent.determineMoneyCost(costString);

					if (interactingAgent.inventory.HasItem(VItemName.HiringVoucher))
						h.AddButton(hireButtonText + "_Voucher", 6666, m =>
						{
							interactingAgent.agentInteractions.QualifyHireAsProtection(agent, interactingAgent, 6666);
						});

					h.AddButton(hireButtonText, normalHireCost, m =>
					{
						agent.agentInteractions.PressedButton(m.Object, interactingAgent, hireButtonText, normalHireCost);
					});
				}

				if (agent.HasTrait<Permanent_Hire_Only>() || agent.HasTrait<Permanent_Hire>())
				{
					int permanentHireCost = agent.determineMoneyCost(costString + "_Permanent");

					//if (interactingAgent.inventory.HasItem(VItemName.HiringVoucher /*Add Gold Version*/))
					//	h.AddButton(hireButtonText + "_Permanent_Voucher", 6667, m =>
					//	{
					//		//m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, hireButtonText, 6667);
					//		m.Agent.agentInteractions.QualifyHireAsProtection(agent, interactingAgent, 6667);
					//	});

					h.AddButton(hireButtonText + "_Permanent", permanentHireCost, m =>
					{
						// Testing if this can replace the below. Might need to throw the other stuff into that method too.
						PermanentHire.HirePermanently(agent, interactingAgent, permanentHireCost);

						//agent.agentInteractions.QualifyHireAsProtection(agent, interactingAgent, permanentHireCost);

						if (agent.followingNum == interactingAgent.agentID)
						{
							agent.GetHook<H_AgentInteractions>().HiredPermanently = true;
							agent.canGoBetweenLevels = true;
						}
					});
				}
			}
		}
	}
}