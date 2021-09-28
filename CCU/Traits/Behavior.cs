using CCU.Traits.Behaviors;
using RogueLibsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCU.Traits
{
	public static class Behavior
	{
		public static List<Type> BehaviorTraits = new List<Type>()
		{
			typeof(Thief_Hire),
			typeof(Thief_HonestThief),
			typeof(Thief_Pickpocket),
			typeof(Thief_Vendor),
		};
		public static List<Type> HireTraits = new List<Type>()
		{
			typeof(Thief_Hire),
		};
		public static List<Type> InteractionTraits = new List<Type>()
		{
			typeof(Thief_Hire),
			typeof(Thief_Vendor),
		};
		public static List<Type> NoninteractionTraits = new List<Type>()
		{
			typeof(Thief_HonestThief),
			typeof(Thief_Pickpocket),
		};

		public static bool HasBehaviorTrait(Agent agent)
		{
			foreach (Type trait in BehaviorTraits)
				if (agent.HasTrait<Trait>())
					return true;

			return false;
		}
		public static bool HasHireTrait(Agent agent)
		{
			foreach (Type trait in HireTraits)
				if (agent.HasTrait<Trait>())
					return true;

			return false;
		}
		public static bool HasInteractionTrait(Agent agent)
		{
			foreach (Type trait in InteractionTraits)
				if (agent.HasTrait<Trait>())
					return true;

			return false;
		}
		public static bool HasNoninteractionTrait(Agent agent)
		{
			foreach (Type trait in NoninteractionTraits)
				if (agent.HasTrait<Trait>())
					return true;

			return false;
		}
	}
}
