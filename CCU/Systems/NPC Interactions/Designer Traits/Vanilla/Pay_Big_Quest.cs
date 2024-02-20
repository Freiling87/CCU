using RogueLibsCore;
using System;

namespace CCU.Interactions
{
	public class Pay_Big_Quest : T_InteractionNPC
	{
		public override string ButtonTextName => PutMoneyTowardBigQuest;
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& interactingAgent.bigQuest == VanillaAgents.SlumDweller 
			&& !GC.loadLevel.levelContainsMayor;
		public override bool RequireTrust => false;

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;

			h.AddButton(ButtonTextName, 50, m =>
			{
				m.Object.agentInteractions.PressedButton(m.Object, interactingAgent, VButtonText.PutMoneyTowardHome, 50);
				//	The button text name here is supposed to be different, since we're just repackaging a vanilla feature.
			});
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Pay_Big_Quest>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format($"This character can accept payments for the Slum Dweller Big Quest."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Pay_Big_Quest)),
				})
				.WithUnlock(new TU_DesignerUnlock());

			RogueLibs.CreateCustomName(PutMoneyTowardBigQuest, NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = String.Format($"Contribute Money Toward Big Quest"),

			});
		}

		public const string PutMoneyTowardBigQuest = "PutMoneyTowardBigQuest";
	}
}