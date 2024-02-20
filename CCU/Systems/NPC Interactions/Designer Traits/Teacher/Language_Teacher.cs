using BunnyLibs;
using CCU.Systems.Language;
using RogueLibsCore;
using System.Collections.Generic;
using System.Linq;

namespace CCU.Interactions
{
	public class Teach_Languages : T_InteractionNPC
	{
		public override string ButtonTextName => "Learn_Language";
		public override bool NonMoneyCost => true;
		public override bool InteractionAllowed(Agent interactingAgent) =>
			base.InteractionAllowed(interactingAgent)
			&& TeachableLanguages(Owner, interactingAgent).Any();
		public override InteractionState? StateApplied => InteractionState.LearnTraits_Language;
		public override InteractionState? StateRequired => null; // Complex, see below

		private static List<string> TeachableLanguages(Agent teacher, Agent student) =>
			LanguageSystem.KnownLanguages(teacher).Where(lang =>
				!LanguageSystem.KnownLanguages(student).Contains(lang))
			.ToList();

		public override void AddInteraction(SimpleInteractionProvider<Agent> h)
		{
			Agent agent = h.Object;
			Agent interactingAgent = h.Agent;
			H_AgentInteractions agentInteractionsHook = agent.GetOrAddHook<H_AgentInteractions>();

			switch (agentInteractionsHook.interactionState)
			{
				case InteractionState.Default:
					h.AddButton(ButtonTextName, m =>
					{
						ApplyInteractionState();
						agent.RefreshButtons();
					});
					break;

				case InteractionState.LearnTraits_Language:
					foreach (string language in TeachableLanguages(agent, interactingAgent))
					{
						string determineMoneyCostText = language is LanguageSystem.English
							? CDetermineMoneyCost.LearnLanguageEnglish
							: CDetermineMoneyCost.LearnLanguageOther;
						int cost = agent.determineMoneyCost(determineMoneyCostText);

						h.AddButton("Learn_" + language, cost, m =>
						{
							if (agent.moneySuccess(cost))
							{
								if (language == LanguageSystem.English)
									interactingAgent.statusEffects.RemoveTrait(VanillaTraits.VocallyChallenged);
								else
								{
									T_Language trait = CoreTools.AllClassesOfType<T_Language>()
										.Where(t =>
											!(t is Polyglot)
											&& t.LanguageNames.Contains(language))
										.FirstOrDefault();

									interactingAgent.AddTrait(trait.GetType());
								}

								if (!TeachableLanguages(agent, interactingAgent).Any())
								{
									logger.LogDebug("No Teachable Langs");
									agentInteractionsHook.interactionState = InteractionState.Default;
								}

								agent.RefreshButtons();
							}
						});
					}

					break;
			}
		}

		[RLSetup]
		public static void Setup()
		{
			PostProcess_DesignerTrait = RogueLibs.CreateCustomTrait<Teach_Languages>()
				.WithDescription(new CustomNameInfo
				{
					[LanguageCode.English] = "Teaches any language they know, for a price. A price of dollars. But those dollars come at a price. In dollars." +
						"\n- English (removes Vocally Challenged) - $600" +
						"\n- Other languages - $200",
				})
				.WithName(new CustomNameInfo
				{
					[LanguageCode.English] = DesignerName(typeof(Teach_Languages)),
				})
				.WithUnlock(new TU_DesignerUnlock());

			RogueLibs.CreateCustomName("Learn_Language", NameTypes.Interface, new CustomNameInfo
			{
				[LanguageCode.English] = "Learn Language",
			});

			foreach (string language in LanguageSystem.AllLanguages)
				RogueLibs.CreateCustomName($"Learn_{language}", NameTypes.Interface, new CustomNameInfo
				{
					[LanguageCode.English] = $"Learn {language}",
				});
		}
	}
}