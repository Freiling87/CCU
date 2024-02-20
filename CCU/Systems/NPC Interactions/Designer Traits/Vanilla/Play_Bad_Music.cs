using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CCU.Interactions
{
	public class Play_Bad_Music : T_InteractionNPC
	{
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& Turntableses().Any();

		private List<Turntables> Turntableses()
		{
			return GC.objectRealList.Where(or =>
					or.objectName == VObjectReal.Turntables && or is Turntables turntables
					&& turntables.startingChunk == Owner.startingChunk
					&& !turntables.destroyed && turntables.functional
					&& turntables.SpeakersFunctional() && !turntables.badMusicPlaying
					&& Vector2.Distance(turntables.tr.position, Owner.tr.position) < 1.28f
				).Cast<Turntables>()
				.ToList();
		}

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;

			if (interactingAgent.inventory.HasItem(VItemName.RecordofEvidence))
				h.AddButton(VButtonText.PlayMayorEvidence, agent.determineMoneyCost(VDetermineMoneyCost.PlayMayorEvidence), m =>
				{
					if (m.Object.moneySuccess(m.Object.determineMoneyCost(VDetermineMoneyCost.PlayMayorEvidence)))
						m.Object.agentInteractions.PlayMayorEvidence(m.Object, interactingAgent);

					m.Object.StopInteraction();
				});

			h.AddButton(VButtonText.PlayBadMusic, agent.determineMoneyCost(VDetermineMoneyCost.PlayBadMusic), m =>
			{
				if (m.Object.moneySuccess(m.Object.determineMoneyCost(VDetermineMoneyCost.PlayBadMusic)))
				{
					m.Object.agentInteractions.PlayBadMusic(m.Object, interactingAgent);
					m.Object.SetChangeElectionPoints(interactingAgent);
				}

				m.Object.StopInteraction();
			});
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Play_Bad_Music>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = String.Format("This character can be paid to play a bad song, clearing the chunk out. They can also play Mayor Evidence on Turntables."),
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Play_Bad_Music)),
				})
				.WithUnlock(new TU_DesignerUnlock());
		}
	}
}