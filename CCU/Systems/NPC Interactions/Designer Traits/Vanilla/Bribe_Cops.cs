using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Bribe_Cops : T_InteractionNPC
	{
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& !interactingAgent.aboveTheLaw && !interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.AbovetheLaw)
			&& !interactingAgent.enforcer
			&& !interactingAgent.upperCrusty;

		public override string ButtonTextName => VButtonText.BribeCops;
		public override string MoneyCostName => VDetermineMoneyCost.BribeCops;

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;

			if (interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.CopDebt1))
				h.AddButton(VButtonText.PayCops, agent.determineMoneyCost(VDetermineMoneyCost.PayCops1), m =>
				{
					if (agent.moneySuccess(agent.determineMoneyCost(VDetermineMoneyCost.PayCops1)))
						agent.agentInteractions.PayCops(agent, interactingAgent);

					agent.StopInteraction();
				});
			else if (interactingAgent.statusEffects.hasStatusEffect(VStatusEffect.CopDebt2))
				h.AddButton(VButtonText.PayCops, agent.determineMoneyCost(VDetermineMoneyCost.PayCops2), m =>
				{
					if (agent.moneySuccess(agent.determineMoneyCost(VDetermineMoneyCost.PayCops2)))
						agent.agentInteractions.PayCops(agent, interactingAgent);

					agent.StopInteraction();
				});
			else if (!interactingAgent.statusEffects.hasTrait(VanillaTraits.CorruptionCosts))
				h.AddButton(VButtonText.BribeCops, agent.determineMoneyCost(VDetermineMoneyCost.BribeCops), m =>
				{
					if (interactingAgent.aboveTheLaw || interactingAgent.upperCrusty)
						agent.SayDialogue(VDialogue.Cop2_DontNeedMoney);
					else if (agent.moneySuccess(agent.determineMoneyCost(VDetermineMoneyCost.BribeCops)))
						agent.agentInteractions.BribeCops(agent, interactingAgent);

					agent.StopInteraction();
				});
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Bribe_Cops>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character will accept cash to bribe law enforcement."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Bribe_Cops)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}