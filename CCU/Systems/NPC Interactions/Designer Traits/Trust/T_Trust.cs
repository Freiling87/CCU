using RogueLibsCore;
using System.Linq;

namespace CCU.Interactions.Interaction_Gate
{
	public abstract class T_Trust : T_DesignerTrait
	{
		public T_Trust() : base() { }

		public abstract int MinimumRelationship { get; }
	}

	public static class InteractionTrustHelper
	{
		public static bool TrustsAgent(Agent agent, Agent interactingAgent)
		{
			T_Trust trustTrait = agent.GetTraits<T_Trust>().FirstOrDefault();
			string relationship = agent.relationships.GetRel(interactingAgent);
			int relationshipLevel = VRelationship.GetRelationshipLevel(relationship);
			int minTrust = trustTrait?.MinimumRelationship ?? 2;

			return relationshipLevel >= minTrust
				|| (relationship == VRelationship.Annoyed && CanInteractWithAnnoyed(interactingAgent));
		}

		public static bool CanInteractWithAnnoyed(Agent interactingAgent) =>
			interactingAgent.HasTrait(VanillaTraits.Mugger)
			|| interactingAgent.HasTrait(VanillaTraits.Mugger + "2");
	}
}