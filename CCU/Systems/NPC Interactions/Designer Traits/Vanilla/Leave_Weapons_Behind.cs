using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCU.Interactions
{
	public class Leave_Weapons_Behind : T_InteractionNPC
	{
		public override string ButtonTextName => VButtonText.LeaveWeaponsBehind;
		public override bool RequireTrust => false;
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& (interactingAgent.inventory.HasWeapons() || ArmedFollowers(interactingAgent).Any());

		private static List<Agent> ArmedFollowers(Agent leader) =>
			GC.agentList.Where(a =>
				a.employer == leader
				&& a.inventory.HasWeapons()
				&& !a.inCombat
				&& a.jobCode != jobType.GoHere
				&& Vector2.Distance(leader.tr.position, a.tr.position) < 13.44f)
			.ToList();

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			H_AgentInteractions agentInteractionsHook = agent.GetOrAddHook<H_AgentInteractions>();
			Agent interactingAgent = h.Agent;

			if (interactingAgent.inventory.HasWeapons())
				h.AddButton(VButtonText.LeaveWeaponsBehind, m =>
				{
					interactingAgent.inventory.DropWeapons();
					agent.RefreshButtons();
				});

			if (ArmedFollowers(interactingAgent).Any())
				h.AddButton(VButtonText.FollowersLeaveWeaponsBehind, m =>
				{
					foreach (Agent follower in ArmedFollowers(interactingAgent))
						follower.inventory.DropWeapons();

					agent.RefreshButtons();
				});
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Leave_Weapons_Behind>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be interacted with to drop all weapons in the Player's inventory.\n\nBypasses Untrusting traits."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Leave_Weapons_Behind)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}