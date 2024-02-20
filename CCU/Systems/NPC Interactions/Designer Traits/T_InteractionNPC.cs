using CCU.Interactions.Interaction_Gate;
using RogueLibsCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace CCU.Interactions
{
	public abstract class T_InteractionNPC : T_DesignerTrait, IAgentInteraction
	{
		public T_InteractionNPC() : base() { }

		public virtual string ButtonTextName { get; } // Make nullable since some are complex
		/// <summary>
		/// null == Free interaction
		/// </summary>
		public virtual string MoneyCostName => null;
		public virtual bool NonMoneyCost => false;
		public virtual bool RequireCommunication => true;
		public virtual bool RequireTrust => true;
		/// <summary>
		/// Use this to have interactions share a button. E.g., various hire types don't need a button for each.
		/// </summary>
		public virtual string SharedButtonGroup { get; }
		public virtual InteractionState? StateApplied => null;
		public virtual InteractionState? StateRequired => InteractionState.Default;

		public virtual bool InteractionAllowed(Agent interactingAgent) =>
			(StateRequired is null || StateRequired == Owner.GetHook<H_AgentInteractions>().interactionState)
			&& (!RequireCommunication || Owner.CanUnderstandEachOther(interactingAgent, true, false))
			&& (!RequireTrust || InteractionTrustHelper.TrustsAgent(Owner, interactingAgent));

		public virtual void ApplyInteractionState()
		{
			if (StateApplied is null)
				return;

			Owner.GetHook<H_AgentInteractions>().interactionState = (InteractionState)StateApplied;
			Owner.RefreshButtons();
		}
		 
		[RLSetup]
		private static void InteractionSetup()
		{
			RogueInteractions.CreateProvider<Agent>(h =>
			{
				Agent agent = h.Object;
				Agent interactingAgent = h.Agent;

				//	Interactions are not accessible through RL here.
				List<string> sharedButtonsAdded = new List<string>();

				foreach (T_InteractionNPC obj in agent.GetTraits<T_InteractionNPC>())
				{
					if (!(obj.SharedButtonGroup is null) && sharedButtonsAdded.Any(i => i == obj.SharedButtonGroup))
						continue;

					var method = obj.GetType().GetMethod(nameof(InteractionAllowed));
					bool? allowed;

					if (method.DeclaringType == typeof(T_InteractionNPC))
						allowed = obj.InteractionAllowed(interactingAgent);
					else
						allowed = method.Invoke(obj, new[] {interactingAgent}) as bool?;

					if ((bool)allowed)
					{
						obj.AddInteraction(h);
						sharedButtonsAdded.Add(obj.SharedButtonGroup);
					}
				}

				////	TODO: Differentiate between traits that focus on NPC or PC actions.
				//foreach (var trait in agent.GetTraits<T_InteractionNPC>())
				//{
				//	T_InteractionNPC specificTrait = trait; // Use a specific type variable
				//	if (specificTrait.InteractionAllowed(interactingAgent))
				//		specificTrait.AddInteraction(h);
				//}
			});
		}

		public virtual void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;

			if (MoneyCostName is null)
				h.AddButton(ButtonTextName, m =>
				{
					m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, ButtonTextName, 0);
					ApplyInteractionState();
				});
			else if (NonMoneyCost)
				h.AddButton(ButtonTextName, MoneyCostName, m =>
				{
					m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, ButtonTextName, 0);
					ApplyInteractionState();
				});
			else
				h.AddButton(ButtonTextName, agent.determineMoneyCost(MoneyCostName), m =>
				{
					// TODO: Verify MoneySuccess is called somewhere
					m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, ButtonTextName, m.Object.determineMoneyCost(MoneyCostName));
					ApplyInteractionState();
				});
		}
	}
}