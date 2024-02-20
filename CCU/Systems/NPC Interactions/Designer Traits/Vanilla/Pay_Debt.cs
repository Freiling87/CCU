using CCU.Traits.Cost_Scale;
using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Pay_Debt : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.PayDebt;
		public override bool RequireTrust => false;
		public override bool InteractionAllowed(Agent interactingAgent)
		{
			logger.LogDebug("InteractionAllowed: " + GetType());

			return
			base.InteractionAllowed(interactingAgent)
			&& (interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt1)
				|| interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt2)
				|| interactingAgent.statusEffects.hasStatusEffect(VanillaEffects.InDebt3));
		}

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;

			// Cost scaling is done slightly differently for this interaction, since it's not subject to level scaling.
			float costScale = agent.GetTrait<T_CostScale>()?.CostScale ?? 1f;
			int totalCost = (int)(interactingAgent.CalculateDebt() * costScale);

			h.AddButton(VButtonText.PayDebt, totalCost, m =>
			{
				m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, VButtonText.PayDebt, totalCost);
			});
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pay_Debt>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can accept debt payments.\n\n" +
					"Note: If you want them to lend money as well, use {0} too.\n\nBypasses Untrusting traits.", DocumentationName(typeof(Borrow_Money))),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pay_Debt)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}